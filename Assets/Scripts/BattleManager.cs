using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public PlayerControl Player;
    public Stats Stats;
    public Image EnemySprite;
    public BattleEffects EnemyEffects;
    public BattleEffects ScreenEffects;
    public bool BattleStarted = false;
    public bool BattleOver = false;
    public Stats PlayerStats;
    public bool PlayerTurn = false;
    public Menu BattleCanvas;
    public Menu GameCanvas;
    public GameObject ActionMenu;
    public Enemy TargetEnemy;
    public float TurnDelay = 2;
    public DialogueBox BattleText;
    public Dialogue BattleDialogue;

    private float turnTimer = 0;


    private void Start()
    {
       // StartBattle();
    }
    private void Update()
    {
        if (!BattleCanvas.Active)
        {
            return;
        }
        turnTimer += Time.deltaTime;
        if (TargetEnemy.Dead)
        {
            EnemySprite.enabled = false;
        }
        if(turnTimer > TurnDelay)
        {
            if (!BattleStarted)
            {
                StartTurn();
                BattleStarted = true;
                turnTimer = 0;
            }
            if (PlayerTurn)
            {
                if (!Player.Dead)
                {
                    ActionMenu.SetActive(true);
                }
                else
                {
                    if (!BattleOver)
                    {
                        turnTimer = TurnDelay / 2;
                        BattleOver = true;
                    }
                    else
                    {
                        PlayerTurn = true;
                        ActionMenu.SetActive(false);
                        BattleCanvas.Active = false;
                        GameCanvas.Active = false;
                        GameManager.Paused = true;
                    }
                }
            }
            else
            {
                if (!TargetEnemy.Dead)
                {
                    TargetEnemy.AttackPlayer(Player);
                    PlayerTurn = true;
                    turnTimer = 0;
                    EnemyEffects.Attack();
                }
                else
                {
                    if (!BattleOver)
                    {
                        EnemySprite.enabled = false;
                        turnTimer = TurnDelay/2;
                        BattleOver = true;
                    }
                    else
                    {
                        PlayerTurn = false;
                        ActionMenu.SetActive(false);
                        BattleCanvas.Active = false;
                        GameCanvas.Active = true;
                        GameManager.Paused = false;
                    }
                    
                }
            }
            
            
        }
        
    }

    public void StartBattle(Enemy e)
    {
        if (e.Speed > PlayerStats.TurnOrder)
        {
            PlayerTurn = false;
        }
        else
        {
            PlayerTurn = true;
        }
        TargetEnemy = e;
        EnemySprite.enabled = true;
        BattleText.SendText(TargetEnemy.EnemyName + " attacked " + PlayerStats.PlayerName + "!");
        EnemySprite.sprite = TargetEnemy.BattleSprite;
        turnTimer = 0;
        ActionMenu.SetActive(false);
        BattleCanvas.Active = true;
        GameCanvas.Active = false;


    }
    public void PlayerAttacks()
    {
        Attack(TargetEnemy);
        PlayerTurn = false;
        ActionMenu.SetActive(false);
        turnTimer = 0;
    }
    public void StartTurn()
    {
        if (TargetEnemy.Speed > PlayerStats.TurnOrder)
        {
            PlayerTurn = false;
            turnTimer = 0;
        }
        else
        {
            PlayerTurn = true;
            ActionMenu.SetActive(true);
        }
    }
    public void Attack(Enemy e)
    {
        var EquippedWeapon = Player.EquippedWeapon;
        float dmg = 1;
        // Roll for miss
        var hitChance = Random.Range(0, 100);
        if (hitChance > Stats.HitChance)
        {
            BattleText.SendText(PlayerStats.PlayerName + " missed!");
            turnTimer = 0;
            EnemyEffects.Dodge();
            return;
        }
        else
        {
            //Roll attack numbers
            if (EquippedWeapon == null)
            {
                dmg = Random.Range(1, 3);
                dmg = Mathf.CeilToInt(dmg) * Player.Stats.MeleeDamage;
                e.TakeDamage(dmg, BattleText, Player);
            }
            else
            {
                dmg = Random.Range(EquippedWeapon.Damage + 2, (EquippedWeapon.Damage - 2));
                dmg = Mathf.Clamp(dmg, 1, 999);
                if (EquippedWeapon.Type == Weapon.WeaponType.Magic)
                {
                    dmg *= Stats.MagicDamage;
                }
                else if (EquippedWeapon.Type == Weapon.WeaponType.Melee)
                {
                    dmg *= Stats.MeleeDamage;
                }
                else if (EquippedWeapon.Type == Weapon.WeaponType.Ranged)
                {
                    dmg *= Stats.RangeDamage;
                }
            }
        }
        float critChance = Random.Range(0, 20);
        if (critChance < 1 + Stats.CritBonus)
        {
            dmg *= 2.5f;
            dmg = Mathf.CeilToInt(dmg);
            BattleText.SendText("Critical Hit!" + "\n" + PlayerStats.PlayerName + " dealt " + dmg + " damage to " + e.EnemyName + "!");
            e.TakeDamage(dmg, BattleText, Player);
            EnemyEffects.Flash(Color.red);
            turnTimer = 0;
        }
        else
        {
            dmg = Mathf.CeilToInt(dmg);
            BattleText.SendText(PlayerStats.PlayerName + " dealt " + dmg + " damage to " + e.EnemyName + "!");
            e.TakeDamage(dmg, BattleText, Player);
            EnemyEffects.Flash(Color.red);
            turnTimer = 0;
        }




    }
    
}
