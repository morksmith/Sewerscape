using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string EnemyName = "Enemy";
    public Sprite BattleSprite;
    public float Speed = 1;
    public float MinDamage = 1;
    public float MaxDamage = 1;
    public float MaxHP = 5;
    public float HP = 5;
    public float XP;
    public float Gold;
    public bool Dead = false;
    public bool DropItem = false;
    public bool Boss = false;
    

    private void Start()
    {
        HP = MaxHP;
    }

    public void AttackPlayer(PlayerControl p)
    {
        var dmg = Random.Range(MinDamage, MaxDamage);
        dmg -= p.Stats.Defence;
        dmg = Mathf.RoundToInt(Mathf.Clamp(dmg, 1, 999));
        
        p.TakeDamage(dmg, this);
    }
    public void TakeDamage(float dmg, DialogueBox db, PlayerControl p, bool Crit)
    {
        if(dmg < HP)
        {
            HP -= dmg;
            
        }
        else
        {
            HP = 0;
            var goldAmount = Mathf.CeilToInt(Random.Range(Gold * 0.75f, Gold * 1.25f)) * p.Stats.GoldBonus;
            var xpAmount = XP * p.Stats.XPBonus;
            if (Crit)
            {
                db.SendText("Critical Hit! \n" + p.Stats.PlayerName + " dealt " + dmg + " damage to " + EnemyName + "!" + "\n" + p.Stats.PlayerName + " killed " + EnemyName + "!" + "\n" + p.Stats.PlayerName + " gained " + xpAmount + "XP!" + "\n" + goldAmount + " dollars found!");
            }
            else
            {
                db.SendText(p.Stats.PlayerName + " dealt " + dmg + " damage to " + EnemyName + "!" + "\n" + p.Stats.PlayerName + " killed " + EnemyName + "!" + "\n" + p.Stats.PlayerName + " gained " + xpAmount + "XP!" + "\n" + goldAmount + " dollars found!");
            }
            p.PlusXP(xpAmount);
            p.Stats.Gold += goldAmount;
            Dead = true;
            //if (Boss)
            //{
            //    var b = gameObject.GetComponentInParent<BossFight>();
            //    b.KillBoss();
            //}
            Destroy(gameObject);
        }
    }

    
}
