using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleEffects : MonoBehaviour
{
    private Image img;
    public float FlashTime = 1;
    public bool Flashing;
    public bool Fading;
    public bool Attacking;
    public bool Dodging;
    private Color startColor;
    public Color FlashColour = Color.red;
    public float FlashSpeed = 0.025f;
    private float step;
    public float MoveAmount = 10;
    public bool Returning = false;
    public float MoveSpeed = 5;
    private Vector2 targetPos;
    private Vector2 startPos;

    public UnityEvent StoredEvent;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        startPos = img.rectTransform.anchoredPosition;
        startColor = img.color;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Flash(Color.red);
        }
        if (Flashing)
        {
            if (step < 1)
            {
                
                step += Time.deltaTime / FlashTime;
                img.color = Color.Lerp(FlashColour, startColor, step);
                
            }
            else
            {
                step = 0;
                img.enabled = true;
                Flashing = false;
            }
        }
        if (Fading)
        {
            if (step < 1)
            {

                step += Time.deltaTime / FlashTime / 2;
                img.color = Color.Lerp(new Color(0,0,0,0), FlashColour, step);

            }
            else
            {
                if(StoredEvent!= null)
                {
                    StoredEvent.Invoke();
                }
                step = 0;
                img.enabled = true;
                Fading = false;
                Flash(Color.black);
            }
        }
        if (Attacking)
        {
            var moveDist = Vector2.Distance(img.rectTransform.anchoredPosition, targetPos);
            if (!Returning)
            {
                if (!Dodging)
                {
                    if (moveDist < 20f)
                    {
                        img.rectTransform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                
                img.rectTransform.anchoredPosition = Vector2.Lerp(img.rectTransform.anchoredPosition, targetPos, Time.deltaTime * MoveSpeed);
                if (moveDist < 1)
                {
                    Returning = true;
                    img.rectTransform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                img.rectTransform.anchoredPosition = Vector2.Lerp(img.rectTransform.anchoredPosition, startPos, Time.deltaTime * MoveSpeed);
            }
            
        }
        if (Dodging)
        {
            var moveDist = Vector2.Distance(img.rectTransform.anchoredPosition, targetPos);
            //Debug.Log(moveDist);
            if (!Returning)
            {
                img.rectTransform.anchoredPosition = Vector2.Lerp(img.rectTransform.anchoredPosition, targetPos, Time.deltaTime * MoveSpeed);
                if (moveDist < 1)
                {
                    Returning = true;
                }
            }
            else
            {
                img.rectTransform.anchoredPosition = Vector2.Lerp(img.rectTransform.anchoredPosition, startPos, Time.deltaTime * MoveSpeed);
            }

        }
    }
    public void Flash(Color c)
    {
        img.color = c;
        FlashColour = c;
        Flashing = true;
        step = 0;
        
    }
    public void FadeToColour(Color c, UnityEvent ue)
    {
        img.color = c;
        FlashColour = c;
        Fading = true;
        step = 0;
        StoredEvent = ue;

    }
    public void Attack()
    {
        Dodging = false;
        Attacking = true;
        step = 0;
        targetPos = img.rectTransform.anchoredPosition - new Vector2(0, MoveAmount);
        startPos = img.rectTransform.anchoredPosition;
        Returning = false;
    }
    public void Dodge()
    {
        Attacking = false;
        Dodging = true;
        step = 0;
        targetPos = img.rectTransform.anchoredPosition - new Vector2(MoveAmount, 0);
        startPos = img.rectTransform.anchoredPosition;
        Returning = false;
    }
    public void Dead()
    {
        img.enabled = false;
    }
}
