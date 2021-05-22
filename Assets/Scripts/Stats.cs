using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public string PlayerName = "Player";
    public float Level = 1;
    public float Speed = 1;
    public float Strength = 1;
    public float Will = 1;
    public float Skill = 1;
    public float Accuracy = 1;
    public float Vitality = 1;
    public float MaxHP = 10;
    public float HP;
    public float MaxMP = 10;
    public float MP;
    public float MaxXP = 10;
    public float XP = 0;
    public float SkillPoints = 0;
    public float Gold = 0;
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
        HitChance = 70 + Accuracy;
        DodgeChance = 10 + Speed;
        RunChance = 50 + Speed;
        MaxHP = 10 * Vitality;
        MaxMP = 10 * Will;
       
    }
    public void Update()
    {
        HPText.text = "HP : " + HP + "-" + MaxHP;
        MPText.text = "MP : " + MP + "-" + MaxMP;
        XPText.text = "XP : " + XP + "-" + MaxXP;
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
        MaxXP *= 1.5f;
        MaxXP = Mathf.CeilToInt(MaxXP);
        XP = 0;
        HP = MaxHP;
        UpdateStats();
        Debug.Log("Player levelled up!");
    }

}
