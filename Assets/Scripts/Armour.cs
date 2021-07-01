using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{
    public string ArmourName;
    public string ArmourDescription;
    public bool Equipped;
    public float Defence;
    public float Price;
    public float SkillBonus;
    public float WillBonus;
    public float SpeedBonus;
    public float StrengthBonus;
    public float VitalityBonus;
    public Sprite UiSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectArmour()
    {
        var iM = GameObject.FindObjectOfType<ItemManager>();
        iM.SelectArmour(this);
    }
}
