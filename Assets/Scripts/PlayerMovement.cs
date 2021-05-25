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
    public Vector3 targetPos;
    public EnemyManager EnemyManager;
    private Ray moveVector;
    private Ray enemyVector;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var tarDist = Vector3.Distance(targetPos, transform.position);
        transform.position = Vector3.Lerp(transform.position, targetPos, MoveSpeed * Time.deltaTime / tarDist);
        if(CurrentInteractive != null)
        {
            InteractIcon.SetActive(true);
        }
        else
        {
            InteractIcon.SetActive(false);
        }
        if (GameManager.Paused)
        {
            return;
        }

        
        if(tarDist < 0.01f && !GameManager.Paused)
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
                Debug.Log(Joystick.Horizontal + " - " + Joystick.Vertical);
                if (Input.GetAxis("Horizontal") > 0 || Joystick.Horizontal > 0.5f)
                {
                    moveVector = new Ray(transform.position + Vector3.right + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position + Vector3.right + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 90, 0);
                }
                if (Input.GetAxis("Horizontal") < 0 || Joystick.Horizontal < -0.5f)
                {
                    moveVector = new Ray(transform.position - Vector3.right + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position - Vector3.right + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, -90, 0);
                }
                if (Input.GetAxis("Vertical") > 0 || Joystick.Vertical > 0.5f)
                {
                    moveVector = new Ray(transform.position + Vector3.forward + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position + Vector3.forward + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 0, 0);
                }
                if (Input.GetAxis("Vertical") < 0 || Joystick.Vertical < -0.5f)
                {
                    moveVector = new Ray(transform.position - Vector3.forward + (Vector3.up * 2), Vector3.down);
                    enemyVector = new Ray(transform.position - Vector3.forward + (Vector3.up * -2), Vector3.up);
                    PlayerMesh.eulerAngles = new Vector3(0, 180, 0);
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
        }
    }
}
