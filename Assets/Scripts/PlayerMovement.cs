using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public BattleManager Battle;
    public Stats Stats;
    public BattleEffects ScreenEffects;
    public Transform CurrentInteractive;
    public GameObject InteractIcon;
    public FloatingJoystick Joystick;
    public float MoveSpeed;
    public int StepCount = 0;
    public bool IsMoving = false;
    public bool StepCompleted = false;
    public bool OverEnemy = false;
    public bool Walking = false;
    public Vector3 targetPos;
    private Vector3 startPos;
    public EnemyManager EnemyManager;
    private Ray moveVector;
    private Ray enemyVector;
    private RaycastHit hit;
    public Animator SpriteAnimator;
    public float StrengthBonus = 0;
    private float idleTimer;
    private int direction;
    public UnityEvent RestEvent;
    public LayerMask Fog;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Paused)
        {
            return;
        }
        
        if (direction == 0)
        {
            if(Walking)
            {
                SpriteAnimator.Play("Walk Up");
            }
            else
            {
                SpriteAnimator.Play("Idle Up");
            }
        }
        else if(direction == 1)
        {
            if (Walking)
            {
                SpriteAnimator.Play("Walk Right");
            }
            else
            {
                SpriteAnimator.Play("Idle Right");
            }
        }
        else if (direction == 2)
        {
            if (Walking)
            {
                SpriteAnimator.Play("Walk Down");
            }
            else
            {
                SpriteAnimator.Play("Idle Down");
            }
        }
        else if (direction == 3)
        {
            if (Walking)
            {
                SpriteAnimator.Play("Walk Left");
            }
            else
            {
                SpriteAnimator.Play("Idle Left");
            }
        }

        if (CurrentInteractive != null)
        {
            InteractIcon.SetActive(true);
        }
        else
        {
            InteractIcon.SetActive(false);
        }
        
        if (StepCompleted)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer > 0.1f)
            {
                Walking = false;
            }
        }
        var tarDist = Vector3.Distance(targetPos, transform.position);
        if (tarDist > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, MoveSpeed * Time.deltaTime / tarDist);
            StepCompleted = false;
            SpriteAnimator.SetBool("Walking", true);
            idleTimer = 0;
        }
        else
        {
            if (!StepCompleted)
            {

                StepCount++;
                if (OverEnemy)
                {
                    EnemyCheck();
                    targetPos = transform.position;
                }
                
                StepCompleted = true;
                CheckAhead();
                              

            }
            else
            {
                RaycastHit2D hitI = Physics2D.Raycast(moveVector.origin, moveVector.direction, 1, ~Fog);
                if (hitI.collider != null)
                {
                    if (hitI.transform.tag == "Interactive")
                    {
                        if (hitI.transform.GetComponent<BossFight>() != null)
                        {
                            OverEnemy = true;
                        }
                        CurrentInteractive = hitI.transform;
                    }
                    else
                    {
                        targetPos = transform.position;
                        CurrentInteractive = null;
                    }
                }
                if (Input.GetAxis("Horizontal") > 0 || Joystick.Horizontal > 0.5f)
                {
                    moveVector = new Ray(transform.position + new Vector3(1, 0, 0), Vector3.forward);
                    direction = 1;
                    Walking = true;
                    idleTimer = 0;
                    CheckAhead();

                }
                if (Input.GetAxis("Horizontal") < 0 || Joystick.Horizontal < -0.5f)
                {
                    moveVector = new Ray(transform.position + new Vector3(-1, 0, 0), Vector3.forward);
                    direction = 3;
                    Walking = true;
                    idleTimer = 0;
                    CheckAhead();


                }
                if (Input.GetAxis("Vertical") > 0 || Joystick.Vertical > 0.5f)
                {
                    moveVector = new Ray(transform.position + new Vector3(0, 1, 0), Vector3.forward);
                    direction = 0;
                    Walking = true;
                    idleTimer = 0;
                    CheckAhead();

                }
                if (Input.GetAxis("Vertical") < 0 || Joystick.Vertical < -0.5f)
                {
                    moveVector = new Ray(transform.position + new Vector3(0, -1, 0), Vector3.forward);
                    direction = 2;
                    Walking = true;
                    idleTimer = 0;
                    CheckAhead();


                }
                

            }

            


        }
        

    }

    public void CheckAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(moveVector.origin, moveVector.direction, 1, ~Fog);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            if (hit.transform.tag == "Floor")
            {
                targetPos = new Vector3(Mathf.RoundToInt(moveVector.origin.x), Mathf.RoundToInt(moveVector.origin.y), 0);
                OverEnemy = false;
                CurrentInteractive = null;
            }
            else if (hit.transform.tag == "Interactive")
            {
                if (hit.transform.GetComponent<BossFight>() != null)
                {
                    OverEnemy = true;
                }
                CurrentInteractive = hit.transform;
            }
            else
            {
                targetPos = transform.position;
                CurrentInteractive = null;
            }
        }
        RaycastHit2D eHit = Physics2D.Raycast(enemyVector.origin, enemyVector.direction);
        if (eHit.transform.tag == "Fog")
        {
            OverEnemy = true;
            CurrentInteractive = null;
        }
    }

    
    public void EnemyCheck()
    {
        if(StepCount < 4)
        {
            return;
        }
        var enemyChance = Random.Range(0, 6);
        if(enemyChance == 1)
        {
            EnemyManager.SpawnEnemy();
            ScreenEffects.Flash(Color.white);
            StepCount = 0;
            StrengthBonus = 0;
        }
    }
    public void SendToStart()
    {
        transform.position = startPos;
        targetPos = transform.position;
        StepCompleted = true;
        OverEnemy = false;
        Debug.Log(targetPos);
        CurrentInteractive = null;
        StepCount = 0;
        moveVector = new Ray(transform.position + transform.up * 2, -transform.up);

    }
    public void StartRest()
    {
        SpriteAnimator.Play("Lie Down");
        ScreenEffects.FadeToColour(Color.black, RestEvent);
    }

    public void Rest()
    {
        SpriteAnimator.Play("Stand Up");
        Walking = true;
        Stats.HP = Stats.MaxHP;
        Stats.MP = Stats.MaxMP;
        direction = 2;
    }
}
