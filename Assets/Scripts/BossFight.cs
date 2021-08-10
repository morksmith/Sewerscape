using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public string BossName;
    public Enemy BossEnemy;
    public bool Dead = false;
    public string UniqueID;
    public Dialogue VictoryText;
    public DialogueBox GameText;
    private Stats stats;
    private ItemManager items;
    public enum LootType
    {
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
        VictoryText.Sentences.Clear();
        string playerName = GameObject.FindObjectOfType<Stats>().PlayerName;
        GameText = GameObject.FindObjectOfType<DialogueBox>();


        VictoryText.Sentences.Add(playerName + " defeated " + BossName + "! \n" + playerName + " acquired " + LootItem.name + "!");

        if (PlayerPrefs.GetString(UniqueID) == null)
        {
            SpawnBoss();
        }
        else
        {
            if (PlayerPrefs.GetString(UniqueID) == UniqueID + " Dead")
            {
                Dead = true;
                Destroy(this.gameObject);
            }
        }
    }
    public void KillBoss()
    {
        Debug.Log(UniqueID + " was killed!");
        PlayerPrefs.SetString(UniqueID, UniqueID + " Dead");
        GameText.CurrentDialogue = VictoryText;
        GameText.StartDialogue(VictoryText);
        Dead = true;
        if (Type == LootType.Item)
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
        
        this.enabled = false;
        Destroy(this.gameObject);

    }

    public void SpawnBoss()
    {
        Debug.Log(UniqueID + " is alive!");
        Dead = false;
    }
}
