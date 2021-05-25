using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Stats Stats;
    public Weapon EquippedWeapon;
    public PlayerMovement Movement;
    public DialogueBox BattleText;
    public DialogueBox GameText;
    public bool Dead = false;
    public BattleManager Battle;
    private Vector3 startPosition;
    private float deathTimer;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (Dead)
        {
            
            deathTimer += Time.deltaTime;
            if(deathTimer >= 5)
            {

                PlayerDead();
            }
        }
    }
    
    public void TakeDamage(float dmg, Enemy e)
    {
        float evade = Random.Range(0, 100);
        if(evade < Stats.DodgeChance)
        {
            BattleText.SendText(e.EnemyName + " missed!");
            Debug.Log(e.EnemyName + " missed!");
        }
        else
        {
            if (dmg < Stats.HP)
            {
                Stats.HP -= dmg;
                BattleText.SendText(e.EnemyName + " dealt " + dmg + " damage to " + Stats.PlayerName + "!");
            }
            else
            {
                Stats.HP = 0;
                var goldLost = Mathf.RoundToInt(Stats.Gold / 2);
                Stats.Gold -= goldLost;
                BattleText.SendText(e.EnemyName + " dealt " + dmg + " damage to " + Stats.PlayerName + "\n" +  Stats.PlayerName + " passed out!" + "\n" + Stats.PlayerName + "Dropped " + goldLost + " gold.");
                Dead = true;
            }
        }
        
    }
    public void PlusXP(float x)
    {
        Stats.XP += x;
    }

    public void InteractWith()
    {
        if(Movement.CurrentInteractive != null)
        {
            if(Movement.CurrentInteractive.GetComponent<Dialogue>() != null)
            {
                var d = Movement.CurrentInteractive.GetComponent<Dialogue>();
                GameText.CurrentDialogue = d;
                GameText.StartDialogue(d);
                GameManager.Paused = true;
            }
        }
    }
    public void PlayerDead()
    {
        deathTimer = 0;
        
        transform.position = startPosition;
        Movement.targetPos = startPosition;
        Dead = false;
        Stats.HP = Stats.MaxHP;
        Movement.OverEnemy = false;
        GameManager.Paused = false;

    }

}
