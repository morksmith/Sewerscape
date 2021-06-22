using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Item SelectedItem;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemDescriptionText;
    public Button UseButton;
    public Menu GameCanvas;
    public Dialogue Messages;
    public DialogueBox GameText;
    public Image ItemImage;
    public Stats PlayerStats;
    public PlayerMovement PlayerMovement;
    public BattleManager BattleManager;

    // Start is called before the first frame update
    public void Start()
    {
        if(SelectedItem == null)
        {
            ItemNameText.text = " ";
            ItemDescriptionText.text = " ";
            UseButton.interactable = false;
            ItemImage.sprite = null;
        }
    }

    public void SelectItem(Item i)
    {
        SelectedItem = i;
        ItemNameText.text = i.ItemName;
        ItemDescriptionText.text = i.ItemDescription;
        UseButton.interactable = true;
        ItemImage.sprite = i.UiSprite;
    }

    public void UseItem()
    {
        if(SelectedItem.Type == Item.ItemType.Candy)
        {
            UseCandyBar();
        }
        else if(SelectedItem.Type == Item.ItemType.Protein)
        {
            UseProteinBar();
        }
        Destroy(SelectedItem.gameObject);
        SelectedItem = null;
        UseButton.interactable = false;
        ItemNameText.text = " ";
        ItemDescriptionText.text = " ";
        ItemImage.sprite = null;
        if (!GameManager.InBattle)
        {
            GameCanvas.Activate();
            GameManager.Paused = true;

        }
    }
    public void CloseMenu()
    {
        if (!GameManager.InBattle)
        {
            GameCanvas.Activate();
        }
    }

    public void UseCandyBar()
    {
        if (!GameManager.InBattle)
        {
            Messages.Sentences.Clear();
            Messages.Sentences.Add(PlayerStats.PlayerName + " ate a candy bar.");
            var healthBoost = PlayerStats.MaxHP / 3;
            healthBoost = Mathf.CeilToInt(healthBoost);
            healthBoost = Mathf.Clamp(healthBoost, 0, PlayerStats.MaxHP - PlayerStats.HP);
            Messages.Sentences.Add(PlayerStats.PlayerName + " recovered " + healthBoost + " HP!");
            PlayerStats.HP += healthBoost;
            PlayerStats.HP = Mathf.Clamp(PlayerStats.HP, 0, PlayerStats.MaxHP);
            PlayerStats.UpdateStats();
            GameText.StartDialogue(Messages);
        }
        else
        {
            var healthBoost = PlayerStats.MaxHP / 3;
            healthBoost = Mathf.CeilToInt(healthBoost);
            healthBoost = Mathf.Clamp(healthBoost, 0, PlayerStats.MaxHP - PlayerStats.HP);
            PlayerStats.HP += healthBoost;
            PlayerStats.HP = Mathf.Clamp(PlayerStats.HP, 0, PlayerStats.MaxHP);
            PlayerStats.UpdateStats();
            BattleManager.BattleText.SendText(PlayerStats.PlayerName + " ate a candy bar." + "\n" + PlayerStats.PlayerName + " recovered " + healthBoost + " HP!");
            BattleManager.PlayerUseItem();
        }
        
    }

    public void UseProteinBar()
    {
        if (!GameManager.InBattle)
        {
            Messages.Sentences.Clear();
            Messages.Sentences.Add(PlayerStats.PlayerName + " ate a protein bar.");            
            Messages.Sentences.Add(PlayerStats.PlayerName + " feels stronger!");
            PlayerMovement.StrengthBonus = 1;
            GameText.StartDialogue(Messages);
        }
        else
        {
            PlayerStats.StrengthBonus = 1.5f;
            PlayerStats.UpdateStats();
            BattleManager.BattleText.SendText(PlayerStats.PlayerName + " ate a protein bar." + "\n" + PlayerStats.PlayerName + " feels stronger!");
            BattleManager.PlayerUseItem();
        }

    }

}
