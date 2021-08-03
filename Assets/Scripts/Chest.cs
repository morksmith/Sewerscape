using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string UniqueID;
    public bool Open = false;
    public SpriteRenderer Sprite;
    public Sprite ClosedSprite;
    public Sprite OpenSprite;
    public Dialogue OpenText;
    public enum LootType
    {
        Money,
        Item,
        Armour,
        Weapon,
        Spell
    }

    public GameObject LootItem;


    private void Start()
    {
        OpenText.Sentences.Clear();
        string playerName = GameObject.FindObjectOfType<Stats>().PlayerName;
        OpenText.Sentences.Add(playerName + " opened a chest! \nIt contained a " + LootItem.name + "!");

        //if (PlayerPrefs.GetString(UniqueID) == null)
        //{
        //    CloseChest();
        //}
        //else
        //{
        //    if (PlayerPrefs.GetString(UniqueID) == UniqueID + " Open")
        //    {
        //        OpenChest();
        //    }
        //}
    }
    public void OpenChest()
    {
        Debug.Log(UniqueID + " was opened!");
        PlayerPrefs.SetString(UniqueID, UniqueID + " Open");
        Open = true;
        Sprite.sprite = OpenSprite;
    }
    public void CloseChest()
    {
        Debug.Log(UniqueID + " starts closed");
        Open = false;
        Sprite.sprite = ClosedSprite;
    }
}
