using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string WeaponName;
    public string WeaponDescription;
    public bool Equipped;
    public Sprite UiSprite;

    public enum WeaponType
    {
        Melee,
        Ranged,
        Magic
    }
    public WeaponType Type;
    public float MinDamage;
    public float MaxDamage;
    public float Price;
    public float ManaCost;
    public void SelectWeapon()
    {
        var iM = GameObject.FindObjectOfType<ItemManager>();
        iM.SelectWeapon(this);
    }


}
