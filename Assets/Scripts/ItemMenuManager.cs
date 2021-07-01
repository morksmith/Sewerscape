using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuManager : MonoBehaviour
{
    public Color SelectColour;
    public Color DisabledColour;
    public GameObject ItemMenu;
    public GameObject ArmourMenu;
    public GameObject WeaponMenu;
    public Button ItemsButton;
    public Button ArmourButton;
    public Button WeaponButton;
    // Start is called before the first frame update
    void Start()
    {
        ItemsButton.image.color = SelectColour;
        ArmourButton.image.color = DisabledColour;
        WeaponButton.image.color = DisabledColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if (GameManager.InBattle)
        {
            ItemsButton.gameObject.SetActive(false);
            ArmourButton.gameObject.SetActive(false);
            WeaponButton.gameObject.SetActive(false);
            ItemsSelected();
            
           // Debug.Log("In Battle");
        }
        else
        {
            //Debug.Log("Not in Battle");
            ItemsButton.gameObject.SetActive(true);
            ArmourButton.gameObject.SetActive(true);
            WeaponButton.gameObject.SetActive(true);
        }
    }

    public void ItemsSelected()
    {
        ItemsButton.image.color = SelectColour;
        ArmourButton.image.color = DisabledColour;
        WeaponButton.image.color = DisabledColour;
        
    }
    public void ArmourSelected()
    {
        ItemsButton.image.color = DisabledColour;
        ArmourButton.image.color = SelectColour;
        WeaponButton.image.color = DisabledColour;
        ItemMenu.SetActive(false);
        ArmourMenu.SetActive(true);
        WeaponMenu.SetActive(false);
    }
    public void WeaponsSelected()
    {
        ItemsButton.image.color = DisabledColour;
        ArmourButton.image.color = DisabledColour;
        WeaponButton.image.color = SelectColour;
        ItemMenu.SetActive(false);
        ArmourMenu.SetActive(false);
        WeaponMenu.SetActive(true);
    }
}
