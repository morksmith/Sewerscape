using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillManager Manager;
    public Button PlusButton;
    public Button MinusButton;
    public enum SkillStat
    {
        Vitality,
        Strength,
        Skill,
        Will,
        Accuracy,
        Speed
    }
    public SkillStat Stat;

    private float statPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Stat == SkillStat.Accuracy)
        {
            statPoints = Manager.AccPoints;
        }
        else if(Stat == SkillStat.Skill)
        {
            statPoints = Manager.SkiPoints;
        }
        else if (Stat == SkillStat.Speed)
        {
            statPoints = Manager.SpePoints;
        }
        else if (Stat == SkillStat.Strength)
        {
            statPoints = Manager.StrPoints;
        }
        else if (Stat == SkillStat.Vitality)
        {
            statPoints = Manager.VitPoints;
        }
        else if (Stat == SkillStat.Will)
        {
            statPoints = Manager.WilPoints;
        }

        if(statPoints > 0)
        {
            MinusButton.interactable = true;
        }
        else
        {
            MinusButton.interactable = false;
        }

        if (Manager.Stats.SkillPoints > 0)
        {
            PlusButton.interactable = true;
        }
        else
        {
            PlusButton.interactable = false;
        }

    }
}
