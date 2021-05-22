using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public float MoveSpeed = 0.5f;
    public float LevelHeight = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Paused)
        {
            if(transform.position.z < LevelHeight)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -LevelHeight);
            }
        }
    }
}
