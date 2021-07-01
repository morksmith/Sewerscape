using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Candy,
        Protein,
        Mana,
        Bomb,
        Meat,
        Spray,
        Fan
    }
    public ItemType Type;
    public string ItemName = "Item Name";
    public string ItemDescription = "This is the item description";
    public Sprite UiSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectItem()
    {
        var iM = GameObject.FindObjectOfType<ItemManager>();
        iM.SelectItem(this);
    }
}
