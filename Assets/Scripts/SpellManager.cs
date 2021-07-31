using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellManager : MonoBehaviour
{
    public Image SelectedSpellIcon;
    public Spell SelectedSpell;
    public TextMeshProUGUI SpellNameText;
    public TextMeshProUGUI SpellDescriptionText;
    public PlayerControl Player;
    public Button UseButton;
    public Menu GameCanvas;
    public Dialogue Messages;
    public DialogueBox GameText;
    public Image SpellImage;
    public Stats PlayerStats;
    public PlayerMovement PlayerMovement;
    public BattleManager BattleManager;

    // Start is called before the first frame update
    public void Start()
    {
        SelectedSpellIcon.gameObject.SetActive(false);
        if (SelectedSpell == null)
        {
            SpellNameText.text = " ";
            SpellDescriptionText.text = "No spell selected";
            UseButton.interactable = false;
            SpellImage.enabled = false;


        }
        
    }

    public void SelectSpell(Spell i)
    {
        SelectedSpellIcon.gameObject.SetActive(true);
        SelectedSpellIcon.rectTransform.position = i.transform.position;
        SelectedSpell = i;
        SpellNameText.text = (i.SpellName + ":" + i.MPCost + "MP");
        SpellDescriptionText.text = i.SpellDescription;
        if(i.MPCost <= PlayerStats.MP)
        {
            UseButton.interactable = true;
        }
        else
        {
            UseButton.interactable = false;
        }
        SpellImage.enabled = true;
        SpellImage.sprite = i.UiSprite;
    }
    public void UseSpell()
    {
        PlayerStats.MP -= SelectedSpell.MPCost;
        PlayerStats.MP = Mathf.Clamp(PlayerStats.MP, 0, PlayerStats.MaxMP);
        PlayerStats.UpdateStats();
        if (SelectedSpell.Type == Spell.SpellType.Heal)
        {
            UseHeal();
        }
        else if (SelectedSpell.Type == Spell.SpellType.FullHeal)
        {
            UseFullHeal();
        }
        else if (SelectedSpell.Type == Spell.SpellType.Fireball)
        {
            UseFireball();
        }

        SpellNameText.text = " ";
        SpellDescriptionText.text = "No spell selected";
        SpellImage.enabled = false;
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
    public void OpenMenu()
    {
        SelectedSpell = null;
        SelectedSpellIcon.gameObject.SetActive(false);
        UseButton.interactable = false;
    }

    public void UseHeal()
    {
        if (!GameManager.InBattle)
        {
            Messages.Sentences.Clear();
            Messages.Sentences.Add(PlayerStats.PlayerName + " cast Heal!.");
            var healthBoost = PlayerStats.MaxHP / 4;
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
            var healthBoost = PlayerStats.MaxHP / 4;
            healthBoost = Mathf.CeilToInt(healthBoost);
            healthBoost = Mathf.Clamp(healthBoost, 0, PlayerStats.MaxHP - PlayerStats.HP);
            PlayerStats.HP += healthBoost;
            PlayerStats.HP = Mathf.Clamp(PlayerStats.HP, 0, PlayerStats.MaxHP);
            PlayerStats.UpdateStats();
            BattleManager.BattleText.SendText(PlayerStats.PlayerName + " cast heal!" + "\n" + PlayerStats.PlayerName + " recovered " + healthBoost + " HP!");
            BattleManager.PlayerUseItem();
        }
    }
    public void UseFullHeal()
    {

    }

    public void UseFireball()
    {
        if (!GameManager.InBattle)
        {
            Messages.Sentences.Clear();
            Messages.Sentences.Add(PlayerStats.PlayerName + " cast Fireball!.");
            GameText.StartDialogue(Messages);
        }
        else
        {
            BattleManager.AttackSpell(SelectedSpell);
        }
    }


}
