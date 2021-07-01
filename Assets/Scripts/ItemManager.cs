using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Image SelectedItemIcon;
    public Image EquippedArmourIcon;
    public Image EquippedWeaponIcon;
    public Item SelectedItem;
    public Armour SelectedArmour;
    public Armour EquippedArmour;
    public Weapon SelectedWeapon;
    public Weapon EquippedWeapon;
    public Color PlusStatsColour;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ArmourNameText;
    public TextMeshProUGUI WeaponNameText;
    public TextMeshProUGUI ItemDescriptionText;
    public TextMeshProUGUI ArmourDefenceText;
    public TextMeshProUGUI ArmourDescriptionText;
    public TextMeshProUGUI WeaponDescriptionText;
    public TextMeshProUGUI WeaponDamageText;
    public PlayerControl Player;
    public enum ItemMenu { Items, Armour, Weapons}
    public ItemMenu Tab = ItemMenu.Items;
    public Button UseButton;
    public Button EquipArmourButton;
    public Button EquipWeaponButton;
    public Menu GameCanvas;
    public Dialogue Messages;
    public DialogueBox GameText;
    public Image ItemImage;
    public Image ArmourImage;
    public Image WeaponImage;
    public Stats PlayerStats;
    public PlayerMovement PlayerMovement;
    public BattleManager BattleManager;

    // Start is called before the first frame update
    public void Start()
    {
        SelectedItemIcon.gameObject.SetActive(false);
        EquippedArmourIcon.gameObject.SetActive(false);
        EquippedWeaponIcon.gameObject.SetActive(false);
        if(SelectedItem == null)
        {
            ItemNameText.text = " ";
            ItemDescriptionText.text = "No item selected";          
            UseButton.interactable = false;
            ItemImage.enabled = false;
            
            
        }
        if(SelectedArmour == null)
        {
            ArmourNameText.text = " ";
            ArmourDefenceText.text = " ";
            ArmourDescriptionText.text = "No armour selected";
            ArmourImage.enabled = false;
            EquipArmourButton.interactable = false;


        }
        if (SelectedWeapon == null)
        {
            WeaponNameText.text = " ";
            WeaponDescriptionText.text = "No weapon selected";
            WeaponImage.enabled = false;
            EquipWeaponButton.interactable = false;
        }
    }

    public void SelectItem(Item i)
    {
        SelectedItemIcon.gameObject.SetActive(true);
        SelectedItemIcon.rectTransform.position = i.transform.position;
        SelectedItem = i;
        ItemNameText.text = i.ItemName;
        ItemDescriptionText.text = i.ItemDescription;
        UseButton.interactable = true;
        ItemImage.enabled = true;
        ItemImage.sprite = i.UiSprite;
    }
    public void SelectArmour(Armour a)
    {
        SelectedItemIcon.gameObject.SetActive(true);
        SelectedItemIcon.rectTransform.position = a.transform.position;
        SelectedArmour = a;
        ArmourNameText.text = a.ArmourName;
        if(a.Defence < PlayerStats.Defence)
        {
            ArmourDefenceText.color = Color.red;
            ArmourDefenceText.text = "DEF: -" + (PlayerStats.Defence - a.Defence).ToString();
        }
        else if(a.Defence > PlayerStats.Defence)
        {
            ArmourDefenceText.color = PlusStatsColour;
            ArmourDefenceText.text = "DEF: +" + (a.Defence - PlayerStats.Defence).ToString();
        }
        else if(a.Defence == PlayerStats.Defence)
        {
            ArmourDefenceText.color = Color.white;
            ArmourDefenceText.text = "DEF: =" + a.Defence;
        }
        ArmourDescriptionText.text = a.ArmourDescription;
        EquipArmourButton.interactable = true;
        
        //UseButton.interactable = true;
        ArmourImage.enabled = true;
        ArmourImage.sprite = a.UiSprite;
    }
    public void SelectWeapon(Weapon w)
    {
        SelectedItemIcon.gameObject.SetActive(true);
        SelectedItemIcon.rectTransform.position = w.transform.position;
        SelectedWeapon = w;
        WeaponNameText.text = w.WeaponName;
        if (w.MaxDamage < PlayerStats.MaxDamage)
        {
            WeaponDamageText.color = Color.red;            
        }
        else if (w.MaxDamage > PlayerStats.MaxDamage)
        {
            WeaponDamageText.color = PlusStatsColour;
        }
        else if (w.MaxDamage == PlayerStats.MaxDamage)
        {
            WeaponDamageText.color = Color.white;
        }
        WeaponDamageText.text = "DMG: " + w.MinDamage + " - " + w.MaxDamage;
        WeaponDescriptionText.text = w.WeaponDescription;
        EquipWeaponButton.interactable = true;

        //UseButton.interactable = true;
        WeaponImage.enabled = true;
        WeaponImage.sprite = w.UiSprite;
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
        ItemDescriptionText.text = "No item selected";
        ItemImage.enabled = false;
        if (!GameManager.InBattle)
        {
            GameCanvas.Activate();
            GameManager.Paused = true;

        }
    }
    public void EquipArmour()
    {
        if(EquippedArmour == SelectedArmour)
        {
            EquippedArmour = null;
            PlayerStats.Defence = 0;
            EquippedArmourIcon.gameObject.SetActive(false);
            SelectArmour(SelectedArmour);

        }
        else
        {
            EquippedArmour = SelectedArmour;
            PlayerStats.Defence = SelectedArmour.Defence;
            EquippedArmourIcon.gameObject.SetActive(true);
            EquippedArmourIcon.rectTransform.position = EquippedArmour.transform.position;
            SelectArmour(SelectedArmour);


        }


    }
    public void EquipWeapon()
    {
        if (EquippedWeapon == SelectedWeapon)
        {
            EquippedWeapon = null;
            PlayerStats.MaxDamage = 3;
            EquippedWeaponIcon.gameObject.SetActive(false);
            SelectWeapon(SelectedWeapon);
            Player.EquippedWeapon = null;

        }
        else
        {
            EquippedWeapon = SelectedWeapon;
            PlayerStats.MaxDamage = SelectedWeapon.MaxDamage;
            EquippedWeaponIcon.gameObject.SetActive(true);
            EquippedWeaponIcon.rectTransform.position = EquippedWeapon.transform.position;
            SelectWeapon(SelectedWeapon);
            Player.EquippedWeapon = SelectedWeapon;
            Debug.Log("Player Equipped " + SelectedWeapon.WeaponName);

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

    public void UpdateUI()
    {
        if (SelectedItem == null)
        {
            ItemNameText.text = " ";
            ItemDescriptionText.text = "No item selected";
            UseButton.interactable = false;
            ItemImage.enabled = false;
        }
        if (SelectedArmour == null)
        {
            ArmourNameText.text = " ";
            ArmourDefenceText.text = " ";
            ArmourDescriptionText.text = "No armour selected";
            ArmourImage.enabled = false;
            EquipArmourButton.interactable = false;
        }
        if (SelectedWeapon == null)
        {
            WeaponNameText.text = " ";
            WeaponDescriptionText.text = "No weapon selected";
            WeaponImage.enabled = false;
            EquipWeaponButton.interactable = false;
        }
        SelectedItemIcon.gameObject.SetActive(false);
        

    }

}
