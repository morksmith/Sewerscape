﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool Active = false;
    public bool PauseMenu = true;
    public bool BattleMenu = false;
    public Vector3 InactivePosition;
    public Vector3 ActivePosition;
    public float MoveSpeed = 5;

    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, ActivePosition, MoveSpeed * Time.deltaTime);
        }
        else
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, InactivePosition, MoveSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        var menus = FindObjectsOfType<Menu>();
        foreach(Menu m in menus)
        {
            if (!m.BattleMenu)
            {
                m.DeActivate();
            }

        }
        if (PauseMenu)
        {
            GameManager.Paused = true;
        }
        else
        {
            GameManager.Paused = false;
        }
        Active = true;
    }

    public void DeActivate()
    {
        if (!GameManager.InBattle)
        {
            if (PauseMenu)
            {
                GameManager.Paused = false;
            }
            else
            {
                GameManager.Paused = true;
            }
        }
       
        Active = false;


    }

    public void Toggle()
    {
        if (Active)
        {
            DeActivate();
        }
        else
        {
            Activate();
        }
    }
}
