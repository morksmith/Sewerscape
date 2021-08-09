using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public float TextSpeed = 0.1f;
    public TextMeshProUGUI TMP;
    public bool DialogueStarted = false;
    public GameObject ActionMenu;
    public PlayerControl PlayerControl;
    public Dialogue CurrentDialogue;
    private int charCount = 0;
    private float charTimer = 0;
    public GameObject NextIcon;
    public float FlashSpeed = 0.2f;
    private float flashTimer = 0;
    
    void Update()
    {
        if (DialogueStarted)
        {
            if(charCount >= TMP.text.Length)
            {
                if(NextIcon != null)
                {
                    flashTimer += Time.deltaTime;
                    if (flashTimer > FlashSpeed)
                    {
                        if (NextIcon.activeSelf)
                        {
                            NextIcon.SetActive(false);
                        }
                        else
                        {
                            NextIcon.SetActive(true);
                        }
                        flashTimer = 0;
                    }
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    NextSentence();
                }
            }
            else
            {
                if(NextIcon != null)
                {
                    NextIcon.SetActive(false);
                }
            }
            
        }
        charTimer += Time.deltaTime;
        if(charTimer > TextSpeed)
        {
            if(charCount > TMP.text.Length)
            {
                return;
            }
            charCount++;
            TMP.maxVisibleCharacters = charCount;
            charTimer = 0;
        }

        
    }

    public void SendText(string txt)
    {

        TMP.text = " ";
        charCount = 0;
        charTimer = 0;
        TMP.text = txt;
    }

    public void StartDialogue(Dialogue d)
    {
        ActionMenu.SetActive(false);
        CurrentDialogue = d;
        DialogueStarted = true;
        TMP.text = " ";
        charCount = 0;
        charTimer = 0;
        TMP.text = d.Sentences[d.CurrentSentence];

    }
    public void NextSentence()
    {
        if(CurrentDialogue.CurrentSentence < CurrentDialogue.Sentences.Count - 1)
        {
            CurrentDialogue.CurrentSentence++;
            TMP.text = " ";
            charCount = 0;
            charTimer = 0;
            TMP.text = CurrentDialogue.Sentences[CurrentDialogue.CurrentSentence];
        }
        else
        {

            ActionMenu.SetActive(true);
            DialogueStarted = false;
            TMP.text = " ";
            charCount = 0;
            charTimer = 0;
            CurrentDialogue.CurrentSentence = 0;
            if (!GameManager.InBattle)
            {
                GameManager.Paused = false;
            }

        }
    }
}
