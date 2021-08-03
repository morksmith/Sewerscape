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


    private void Update()
    {
        
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
                BattleText.SendText(e.EnemyName + " dealt " + dmg + " damage to " + Stats.PlayerName + "\n" + Stats.PlayerName + " passed out!");
                Dead = true;
                GameManager.InBattle = false;
                //GameManager.Paused = false;
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
            else if (Movement.CurrentInteractive.GetComponent<Switch>() != null)
            {
                var d = Movement.CurrentInteractive.GetComponent<Dialogue>();
                var s = Movement.CurrentInteractive.GetComponent<Switch>();
                s.ActivateSwitch();
                GameText.CurrentDialogue = d;
                GameText.StartDialogue(d);
                GameManager.Paused = true;
            }
            else if (Movement.CurrentInteractive.GetComponent<Chest>() != null)
            {
                var c = Movement.CurrentInteractive.GetComponent<Chest>();
                c.OpenChest();
                var d = Movement.CurrentInteractive.GetComponent<Dialogue>();
                GameText.CurrentDialogue = d;
                GameText.StartDialogue(d);
                GameManager.Paused = true;
            }
        }
    }
    

}
