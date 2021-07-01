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
    public GameObject EquippedArmourIcon;
    public GameObject EquippedWeaponIcon;
    public Button ItemsButton;
    public Button ArmourButton;
    public Button WeaponButton;
    public Button UseItemButton;
    public Button EquipArmourButton;
    public Button EquipWeaponButton;
    public ItemManager ItemManager;
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
        ItemMenu.gameObject.SetActive(true);
        ArmourMenu.gameObject.SetActive(false);
        WeaponMenu.gameObject.SetActive(false);
        ItemManager.SelectedItem = null;
        ItemManager.SelectedArmour = null;
        ItemManager.SelectedWeapon = null;
        UseItemButton.gameObject.SetActive(true);
        EquipArmourButton.gameObject.SetActive(false);
        EquipWeaponButton.gameObject.SetActive(false);
        ItemManager.UpdateUI();
        EquippedArmourIcon.gameObject.SetActive(false);
        EquippedWeaponIcon.gameObject.SetActive(false);



    }
    public void ArmourSelected()
    {
        ItemsButton.image.color = DisabledColour;
        ArmourButton.image.color = SelectColour;
        WeaponButton.image.color = DisabledColour;
        ItemMenu.gameObject.SetActive(false);
        ArmourMenu.gameObject.SetActive(true);
        WeaponMenu.gameObject.SetActive(false);
        ItemManager.SelectedItem = null;
        ItemManager.SelectedArmour = null;
        ItemManager.SelectedWeapon = null;
        UseItemButton.gameObject.SetActive(false);
        EquipArmourButton.gameObject.SetActive(true);
        EquipWeaponButton.gameObject.SetActive(false);
        ItemManager.UpdateUI();
        EquippedWeaponIcon.gameObject.SetActive(false);

        if (ItemManager.EquippedArmour != null)
        {
            EquippedArmourIcon.gameObject.SetActive(true);
            EquippedArmourIcon.transform.position = ItemManager.EquippedArmour.transform.position;
        }
        

    }
    public void WeaponsSelected()
    {
        ItemsButton.image.color = DisabledColour;
        ArmourButton.image.color = DisabledColour;
        WeaponButton.image.color = SelectColour;
        ItemMenu.gameObject.SetActive(false);
        ArmourMenu.gameObject.SetActive(false);
        WeaponMenu.gameObject.SetActive(true);
        ItemManager.SelectedItem = null;
        ItemManager.SelectedArmour = null;
        ItemManager.SelectedWeapon = null;
        UseItemButton.gameObject.SetActive(false);
        EquipArmourButton.gameObject.SetActive(false);
        EquipWeaponButton.gameObject.SetActive(true);
        ItemManager.UpdateUI();
        EquippedArmourIcon.gameObject.SetActive(false);
        if (ItemManager.EquippedWeapon != null)
        {
            EquippedWeaponIcon.gameObject.SetActive(true);
            EquippedWeaponIcon.transform.position = ItemManager.EquippedWeapon.transform.position;
        }


    }
}
