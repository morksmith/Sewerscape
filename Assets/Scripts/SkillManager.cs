using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public Menu StatsMenu;
    public PlayerControl Player;
    public Stats Stats;
    public TextMeshProUGUI StatsText;
    public TextMeshProUGUI SkillsText;
    public float VitPoints;
    public float StrPoints;
    public float SkiPoints;
    public float WilPoints;
    public float AccPoints;
    public float SpePoints;
    private float StartPoints;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateStats()
    {
        StatsText.text = Stats.PlayerName + "\n Level : " + Stats.Level + "\n HP : " + Stats.HP + "/" + Stats.MaxHP + "\n MP : " + Stats.MP + "/" + Stats.MaxMP + "\n Melee DMG : " + Stats.MeleeDamage.ToString("F1") + "\n Ranged DMG : " + Stats.RangeDamage.ToString("F1") + "\n Magic DMG : " + Stats.MagicDamage.ToString("F1") + "\n Hit Chance : " + Stats.HitChance + "% \n Dodge Chance : " + Stats.DodgeChance + "%";
        SkillsText.text = "XP : " + Stats.XP + "/" + Stats.MaxXP + "\n Skill Points : " + Stats.SkillPoints + "\n Vitality : " + Stats.Vitality + "\n Strength : " + Stats.Strength + "\n Skill : " + Stats.Skill + "\n Will : " + Stats.Will + "\n Accuracy : " + Stats.Accuracy + "\n Speed : " + Stats.Speed;
        Stats.UpdateStats();
    }
    public void OpenMenu()
    {
        Stats.UpdateStats();
        StartPoints = Stats.SkillPoints;
        UpdateStats();
    }
    public void CancelMenu()
    {
        Stats.SkillPoints = StartPoints;
        Stats.Vitality -= VitPoints;
        VitPoints = 0;
        Stats.Strength -= StrPoints;
        StrPoints = 0;
        Stats.Skill -= SkiPoints;
        SkiPoints = 0;
        Stats.Will -= WilPoints;
        WilPoints = 0;
        Stats.Accuracy -= AccPoints;
        AccPoints = 0;
        Stats.Speed -= SpePoints;
        SpePoints = 0;
    }
    public void AcceptMenu()
    {
        VitPoints = 0;
        StrPoints = 0;
        SkiPoints = 0;
        WilPoints = 0;
        AccPoints = 0;
        SpePoints = 0;
    }
    public void PlusVit()
    {
        if(Stats.SkillPoints > 0)
        {
            var hpPercent = (Stats.HP / Stats.MaxHP);
            Stats.Vitality++;
            VitPoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            Stats.HP = Mathf.CeilToInt(hpPercent * Stats.MaxHP);
            Stats.HP = Mathf.Clamp(Stats.HP, 1, Stats.MaxHP);
            UpdateStats();


        }

    }
    public void MinusVit()
    {
        if(VitPoints > 0)
        {
            var hpPercent = (Stats.HP / Stats.MaxHP);
            Stats.Vitality--;
            VitPoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            Stats.HP = Mathf.CeilToInt(hpPercent * Stats.MaxHP);
            Stats.HP = Mathf.Clamp(Stats.HP, 1, Stats.MaxHP);
            UpdateStats();


        }

    }
    public void PlusWill()
    {
        if (Stats.SkillPoints > 0)
        {
            var mpPercent = (Stats.MP / Stats.MaxMP);
            Stats.Will++;
            WilPoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            Stats.MP = Mathf.CeilToInt(mpPercent * Stats.MaxMP);
            Stats.MP = Mathf.Clamp(Stats.MP, 1, Stats.MaxMP);
            UpdateStats();


        }

    }
    public void MinusWill()
    {
        if (WilPoints > 0)
        {
            var mpPercent = (Stats.MP / Stats.MaxMP);
            Stats.Will--;
            WilPoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            Stats.MP = Mathf.CeilToInt(mpPercent * Stats.MaxMP);
            Stats.MP = Mathf.Clamp(Stats.MP, 1, Stats.MaxMP);
            UpdateStats();


        }

    }
    public void PlusSpeed()
    {
        if(Stats.SkillPoints > 0)
        {
            Stats.Speed++;
            SpePoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void MinusSpeed()
    {
        if (SpePoints > 0)
        {
            Stats.Speed--;
            SpePoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void PlusStrength()
    {
        if (Stats.SkillPoints > 0)
        {
            Stats.Strength++;
            StrPoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void MinusStrength()
    {
        if (StrPoints > 0)
        {
            Stats.Strength--;
            StrPoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void PlusSkill()
    {
        if (Stats.SkillPoints > 0)
        {
            Stats.Skill++;
            SkiPoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void MinusSkill()
    {
        if (SkiPoints > 0)
        {
            Stats.Skill--;
            SkiPoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void PlusAccuracy()
    {
        if (Stats.SkillPoints > 0)
        {
            Stats.Accuracy++;
            AccPoints++;
            Stats.SkillPoints--;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
    public void MinusAccuracy()
    {
        if (AccPoints > 0)
        {
            Stats.Accuracy--;
            AccPoints--;
            Stats.SkillPoints++;
            Stats.UpdateStats();
            UpdateStats();
        }
    }
}
