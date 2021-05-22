using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Paused = false;
    public PlayerControl Player;
    public Stats PlayerStats;
    public Dialogue LevelUpMessage;
    public DialogueBox GameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            if(PlayerStats.XP >= PlayerStats.MaxXP)
            {
                PlayerStats.LevelUp();
                GameManager.Paused = true;
                LevelUpMessage.Sentences.Clear();
                LevelUpMessage.Sentences.Add(PlayerStats.PlayerName + " reached level " + PlayerStats.Level + "!");
                LevelUpMessage.Sentences.Add("Health restored! Skill point awarded!");
                LevelUpMessage.Sentences.Add(PlayerStats.MaxXP.ToString() + "XP points until next level");
                GameText.StartDialogue(LevelUpMessage);
            }
        }
    }
}
