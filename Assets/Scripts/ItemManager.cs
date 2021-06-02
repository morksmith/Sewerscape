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
    // Start is called before the first frame update
    public void Start()
    {
        if(SelectedItem == null)
        {
            ItemNameText.text = " ";
            ItemDescriptionText.text = " ";
            UseButton.interactable = false;
        }
    }

    public void SelectItem(Item i)
    {
        SelectedItem = i;
        ItemNameText.text = i.ItemName;
        ItemDescriptionText.text = i.ItemDescription;
        UseButton.interactable = true;
    }

    public void UseItem()
    {
        Destroy(SelectedItem.gameObject);
        SelectedItem = null;
        UseButton.interactable = false;
        ItemNameText.text = " ";
        ItemDescriptionText.text = " ";
        if (!GameManager.InBattle)
        {
            GameCanvas.Activate();
        }
    }
    public void CloseMenu()
    {
        if (!GameManager.InBattle)
        {
            GameCanvas.Activate();
        }
    }

}
