using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public enum SpellType
    {
        Heal,
        FullHeal,
        Freeze,
        Defense,
        Reflect,
        Fireball,
        FireBlast,
        Explosion,
        Curse,
        Meteor
    }
    public SpellType Type;
    public string SpellName = "Spell Name";
    public string SpellDescription = "This is the spell description";
    public float MPCost;
    public Sprite UiSprite;
    public float MinDamage;
    public float MaxDamage;


    public void SelectSpell()
    {
        var sM = GameObject.FindObjectOfType<SpellManager>();
        sM.SelectSpell(this);
    }
}
