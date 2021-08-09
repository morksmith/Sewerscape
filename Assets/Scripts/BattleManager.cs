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
                if (!Player.Dead && !BattleOver)
                {
                    ActionMenu.SetActive(true);
                }
                else
                {
                    if (!BattleOver)
                    {
                        turnTimer = TurnDelay;
                        BattleOver = true;
                    }
                    else
                    {
                        PlayerTurn = true;
                        ActionMenu.SetActive(false);
                        BattleCanvas.Active = false;
                        GameCanvas.Active = true;
                        GameManager.Paused = false;
                        Debug.Log("Battle Over");
                        BattleOver = false;
                        BattleStarted = false;
                    
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
                        turnTimer = -1;
                        BattleOver = true;
                    }
                    else
                    {
                        PlayerTurn = false;
                        ActionMenu.SetActive(false);
                        BattleCanvas.Active = false;
                        GameCanvas.Active = true;
                        GameManager.Paused = false;
                        GameManager.InBattle = false;
                        BattleOver = false;
                        BattleStarted = false;
                        Player.Stats.StrengthBonus = 1;
                    }
                    
                }
            }
            
            
        }
        
    }

    public void StartBattle(Enemy e)
    {
        GameManager.InBattle = true;
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
        //GameManager.Paused = true;
        Attack(TargetEnemy);
        Debug.Log(TargetEnemy.name);
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
                Debug.Log("Base Damage = " + dmg);
            }
            else
            {
                dmg = Random.Range(EquippedWeapon.MinDamage, (EquippedWeapon.MaxDamage));
                dmg = Mathf.Clamp(dmg, 1, 999);
                Debug.Log("Weapon Damage = " + dmg);

                if (EquippedWeapon.Type == Weapon.WeaponType.Magic)
                {
                    dmg *= Stats.MagicDamage;
                    Debug.Log("Weapon Multiply Damage = " + dmg);

                }
                else if (EquippedWeapon.Type == Weapon.WeaponType.Melee)
                {
                    dmg *= Stats.MeleeDamage;
                    Debug.Log("Weapon Multiply Damage = " + dmg);

                }
                else if (EquippedWeapon.Type == Weapon.WeaponType.Ranged)
                {
                    dmg *= Stats.RangeDamage;
                    Debug.Log("Weapon Multiply Damage = " + dmg);

                }
            }
        }
        dmg *= PlayerStats.StrengthBonus;
        Debug.Log(PlayerStats.StrengthBonus);
        Debug.Log("Strength Bonus Damage = " + dmg);

        float critChance = Random.Range(0, 20);
        if (critChance < 1 + Stats.CritBonus)
        {
            dmg *= 2.5f;
            dmg = Mathf.CeilToInt(dmg);
            Debug.Log("Crit Damage = " + dmg);

            BattleText.SendText("Critical Hit!" + "\n" + PlayerStats.PlayerName + " dealt " + dmg + " damage to " + e.EnemyName + "!");
            if(e.HP > dmg)
            {
                turnTimer = 0;
            }
            else
            {
                turnTimer = -1;
            }
            e.TakeDamage(dmg, BattleText, Player, true);
            
            //turnTimer = 0;
        }
        else
        {
            dmg = Mathf.CeilToInt(dmg);
            Debug.Log("Rounded Up Damage = " + dmg);

            if (e.HP > dmg)
            {
                turnTimer = 0;
            }
            else
            {
                turnTimer = -1;
            }
            BattleText.SendText(PlayerStats.PlayerName + " dealt " + dmg + " damage to " + e.EnemyName + "!");
            e.TakeDamage(dmg, BattleText, Player, false);
            EnemyEffects.Flash(Color.red);
            //turnTimer = 0;
        }

        




    }
    public void AttackSpell(Spell s)
    {
        PlayerTurn = false;
        ActionMenu.SetActive(false);
        turnTimer = -1;
        var e = TargetEnemy;
        float dmg = 1;
        dmg = Random.Range(s.MinDamage, (s.MaxDamage));
        dmg = Mathf.Clamp(dmg, 1, 999);
        dmg *= Stats.MagicDamage;
        dmg = Mathf.CeilToInt(dmg);
        Debug.Log("Spell Damage = " + dmg);

            if (e.HP > dmg)
            {
                turnTimer = 0;
            }
            else
            {
                turnTimer = -1;
            }
            BattleText.SendText(PlayerStats.PlayerName + " cast " + s.SpellName + "!" + "\n" + PlayerStats.PlayerName + " dealt " + dmg + " damage to " + e.EnemyName + "!");
            e.TakeDamage(dmg, BattleText, Player, false);
            EnemyEffects.Flash(Color.red);
        
    }
    public void PlayerUseItem()
    {
        PlayerTurn = false;
        ActionMenu.SetActive(false);
        turnTimer = 0;
        Debug.Log("Player used Item");
    }

    public void AttemptRun()
    {
        var runChance = Random.Range(0, 100);
        ActionMenu.SetActive(false);

        if (runChance < PlayerStats.RunChance)
        {
            BattleText.SendText(PlayerStats.PlayerName + " tried to run away!" + "\n" + PlayerStats.PlayerName + " got away safely!");
            turnTimer = -1;            
            BattleOver = true;
            PlayerTurn = true;
        }
        else
        {
            BattleText.SendText(PlayerStats.PlayerName + " tried to run away!" + "\n" + PlayerStats.PlayerName + " couldn't escape!");
            turnTimer = -1;
            PlayerTurn = false;
        }
    }

    


}
