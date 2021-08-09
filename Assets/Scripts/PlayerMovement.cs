using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public BattleManager Battle;
    public BattleEffects ScreenEffects;
    public Transform CurrentInteractive;
    public GameObject InteractIcon;
    public Transform PlayerMesh;
    public FloatingJoystick Joystick;
    public float MoveSpeed;
    public int StepCount = 0;
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
        var tarDist = Vector3.Distance(targetPos, transform.position);
        transform.position = Vector3.Lerp(transform.position, targetPos, MoveSpeed * Time.deltaTime / tarDist);
        if(direction == 0)
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
        
        if (tarDist < 0.01f && !GameManager.Paused)
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
                              

            }
            else
            {
                //Debug.Log(Joystick.Horizontal + " - " + Joystick.Vertical);
                if (Input.GetAxis("Horizontal") > 0 || Joystick.Horizontal > 0.5f)
                {
                    moveVector = new Ray(transform.position + Vector3.right + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position + Vector3.right + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 90, 0);
                    direction = 1;
                    Walking = true;
                    idleTimer = 0;

                }
                if (Input.GetAxis("Horizontal") < 0 || Joystick.Horizontal < -0.5f)
                {
                    moveVector = new Ray(transform.position - Vector3.right + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position - Vector3.right + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, -90, 0);
                    direction = 3;
                    Walking = true;
                    idleTimer = 0;

                }
                if (Input.GetAxis("Vertical") > 0 || Joystick.Vertical > 0.5f)
                {
                    moveVector = new Ray(transform.position + Vector3.forward + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position + Vector3.forward + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 0, 0);
                    direction = 0;
                    Walking = true;
                    idleTimer = 0;
                }
                if (Input.GetAxis("Vertical") < 0 || Joystick.Vertical < -0.5f)
                {
                    moveVector = new Ray(transform.position - Vector3.forward + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position - Vector3.forward + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 180, 0);
                    direction = 2;
                    Walking = true;
                    idleTimer = 0;

                }


            }
            if (Physics.Raycast(moveVector, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    targetPos = new Vector3(Mathf.RoundToInt(hit.point.x), 0, Mathf.RoundToInt(hit.point.z));
                    OverEnemy = false;
                    CurrentInteractive = null;

                }
                else if (hit.transform.tag == "Interactive")
                {
                    if(hit.transform.GetComponent<BossFight>() != null)
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
            if (Physics.Raycast(enemyVector, out hit))
            {
                if(hit.transform.tag == "Enemy")
                {
                    OverEnemy = true;
                    CurrentInteractive = null;
                }
                
            }
        }
        else
        {
            StepCompleted = false;
            SpriteAnimator.SetBool("Walking", true);
            idleTimer = 0;

        }
        Ray ray = new Ray(PlayerMesh.transform.position + PlayerMesh.transform.forward + (Vector3.up * 2), Vector3.down);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Interactive")
            {
                CurrentInteractive = hit.transform;
            }
            else
            {
                CurrentInteractive = null;
            }
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
}
