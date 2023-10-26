using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float MoveSpeed = 2;
    public float LevelBottom = -30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > LevelBottom)
        {
            transform.position += new Vector3(0, -1 * Time.deltaTime * MoveSpeed, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, (LevelBottom * -1) * 2, transform.position.z);
        }
    }
}
