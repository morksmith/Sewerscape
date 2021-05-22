using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string EnemyName = "Enemy";
    public Sprite BattleSprite;
    public float Level = 1;
    public float Speed = 1;
    public float Damage = 1;
    public float MaxHP = 5;
    public float HP = 5;
    public float XP;
    public bool Dead = false;
    

    private void Start()
    {
        Speed *= Level;
        Damage *= Level;
        MaxHP *= Level;
        HP = MaxHP;
        XP = MaxHP;
    }

    public void AttackPlayer(PlayerControl p)
    {
        var dmg = Random.Range(Damage - (Damage/4), Damage + (Damage/4));
        dmg -= p.Stats.Strength;
        dmg = Mathf.RoundToInt(Mathf.Clamp(dmg, 1, 999));
        p.TakeDamage(dmg, this);
    }
    public void TakeDamage(float dmg, DialogueBox db, PlayerControl p)
    {
        if(dmg < HP)
        {
            HP -= dmg;
            
        }
        else
        {
            HP = 0;
            db.SendText(p.Stats.PlayerName + " deals " + dmg + " damage to " + EnemyName + "!" + "\n" + p.Stats.PlayerName + " killed " + EnemyName + "!" + "\n" + p.Stats.PlayerName +" gains " + XP + "XP!");
            p.PlusXP(XP);
            Dead = true;
            Destroy(gameObject);
        }
    }

    
}
