using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string WeaponName;
    public enum WeaponType
    {
        Melee,
        Ranged,
        Magic
    }
    public WeaponType Type;
    public float Level;
    public float Damage;

}
