using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Paused = false;
    public PlayerControl Player;
    public PlayerMovement Movement;
    public Stats PlayerStats;
    public Dialogue Messages;
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
                GameManager.Paused = true;
                PlayerStats.LevelUp();
                Messages.Sentences.Clear();
                Messages.Sentences.Add(PlayerStats.PlayerName + " reached level " + PlayerStats.Level + "!");
                Messages.Sentences.Add("3 skill points gained!");
                GameText.StartDialogue(Messages);
            }
            if (Player.Dead)
            {
                GameManager.Paused = true;
                Debug.Log("Player Dead");
                Messages.Sentences.Clear();
                Messages.Sentences.Add(PlayerStats.PlayerName + " wakes up at the bottom of the sewer.");
                var goldLost = Mathf.RoundToInt(PlayerStats.Gold / 2);
                Messages.Sentences.Add(PlayerStats.PlayerName + " lost " + goldLost + " gold!");
                PlayerStats.Gold -= goldLost;
                GameText.StartDialogue(Messages);
                Movement.SendToStart();
                Player.Dead = false;
            }
        }
    }
}
