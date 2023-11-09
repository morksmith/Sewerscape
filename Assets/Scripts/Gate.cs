using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public string UniqueID;
    public bool Open = false;
    public BoxCollider2D Collider;
    public SpriteRenderer Sprite;
    public Sprite ClosedSprite;
    public Sprite OpenSprite;


    private void Start()
    {
        if(PlayerPrefs.GetString(UniqueID) == null)
        {
            CloseGate();
        }
        else
        {
            if(PlayerPrefs.GetString(UniqueID) == UniqueID + " Open")
            {
                OpenGate();
            }
        }
    }
    public void OpenGate()
    {
        PlayerPrefs.SetString(UniqueID, UniqueID + " Open");
        Open = true;
        Sprite.sprite = OpenSprite;
        Collider.enabled = false;
    }
    public void CloseGate()
    {
        Debug.Log(UniqueID + " starts closed");
        Open = false;
        Sprite.sprite = ClosedSprite;
        Collider.enabled = true;
    }
}


