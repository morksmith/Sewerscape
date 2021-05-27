﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public string PlayerName = "Player";
    public float LevelCurve = 20;
    public float Defence = 1;
    public float Level = 1;
    public float Speed = 1;
    public float TurnOrder;
    public float Strength = 1;
    public float MeleeDamage;
    public float Will = 1;
    public float MagicDamage;
    public float Skill = 1;
    public float RangeDamage;
    public float Accuracy = 1;
    public float CritChance = 1;
    public float Vitality = 1;
    public float MaxHP = 10;
    public float HP;
    public float MaxMP = 10;
    public float MP;
    public float MaxXP = 10;
    public float XP = 0;
    public float SkillPoints = 0;
    public float Gold = 0;
    public float RunBonus;
    public float XPBonus = 1;
    public float CritBonus;
    public float EncounterBonus;
    public float GoldBonus;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI MPText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI GoldText;

    public float HitChance;
    public float RunChance;
    public float DodgeChance;

    void Start()
    {
        UpdateStats();
        HP = MaxHP;
        MP = MaxMP;
    }

    public void UpdateStats()
    {
        HitChance = Mathf.Ceil(Accuracy / (Accuracy + 20) * 35) + 70;
        DodgeChance = Mathf.Ceil(Speed / (Speed + LevelCurve) * 100);
        RunChance = 50 + RunBonus;
        MaxHP = Mathf.Ceil(Vitality / (Vitality + LevelCurve * 5) * 2000);
        MaxMP = Mathf.Ceil(Will / (Will + LevelCurve * 5) * 2000);
        MeleeDamage = (Strength / (Strength + LevelCurve) * 5) + 1;
        RangeDamage = (Skill / (Skill + LevelCurve) * 5) + 1;
        MagicDamage = (Will / (Will + LevelCurve) * 5) + 1;
        TurnOrder = Mathf.Ceil(Speed / (Speed + LevelCurve) * 30) - 1;
        MaxXP = Mathf.Ceil(Level / (Level + LevelCurve) * 200 * Level);
        CritChance = 1 + CritBonus;



    }
    public void Update()
    {
        HPText.text = "HP : " + HP + "/" + MaxHP;
        MPText.text = "MP : " + MP + "/" + MaxMP;
        XPText.text = "XP : " + XP + "/" + MaxXP;
        GoldText.text = Gold + "G";
        if(HP < MaxHP / 4)
        {
            HPText.color = Color.yellow;
        }
        else
        {
            HPText.color = Color.white;
        }
    }

    public void LevelUp()
    {
        Level++;
        SkillPoints +=3;
        XP -= MaxXP;
        UpdateStats();
        Debug.Log("Player levelled up!");
    }

    public void GainXP(float xp)
    {
        
    }

}
