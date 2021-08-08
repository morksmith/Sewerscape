using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string UniqueID;
    public bool Open = false;
    public float MoneyAmount;
    public SpriteRenderer Sprite;
    public Sprite ClosedSprite;
    public Sprite OpenSprite;
    public Dialogue OpenText;
    private Stats stats;
    private ItemManager items;
    public enum LootType
    {
        Money,
        Item,
        Armour,
        Weapon,
        Spell
    }
    public LootType Type;
    public GameObject LootItem;


    private void Start()
    {
        stats = GameObject.FindObjectOfType<Stats>();
        items = GameObject.FindObjectOfType<ItemManager>();
        OpenText.Sentences.Clear();
        string playerName = GameObject.FindObjectOfType<Stats>().PlayerName;
        if(Type != LootType.Money)
        {
            OpenText.Sentences.Add(playerName + " opened a chest! \n" + playerName + " acquired " + LootItem.name + "!");
        }
        else
        {
            OpenText.Sentences.Add(playerName + " opened a chest! \nIt contained " + MoneyAmount + " dollars!");
        }

        if (PlayerPrefs.GetString(UniqueID) == null)
        {
            CloseChest();
        }
        else
        {
            if (PlayerPrefs.GetString(UniqueID) == UniqueID + " Open")
            {
                Open = true;
                Sprite.sprite = OpenSprite;
            }
        }
    }
    public void OpenChest()
    {
        Debug.Log(UniqueID + " was opened!");
        PlayerPrefs.SetString(UniqueID, UniqueID + " Open");
        Open = true;
        Sprite.sprite = OpenSprite;
        if(Type == LootType.Item)
        {
            Instantiate(LootItem, items.ItemList);
        }
        else if (Type == LootType.Armour)
        {
            Instantiate(LootItem, items.ArmourList);
        }
        else if (Type == LootType.Weapon)
        {
            Instantiate(LootItem, items.WeaponList);
        }
        else if (Type == LootType.Spell)
        {
            Instantiate(LootItem, items.SpellList);
        }
        else if (Type == LootType.Money)
        {
            stats.Gold += MoneyAmount;
            stats.UpdateStats();
        }
        OpenText.Sentences.Clear();
        OpenText.Sentences.Add("Empty...");
        this.enabled = false;

    }
    public void CloseChest()
    {
        Debug.Log(UniqueID + " starts closed");
        Open = false;
        Sprite.sprite = ClosedSprite;
    }
}
