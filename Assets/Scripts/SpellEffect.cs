using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public float EffectTime;
    public Animator Animator;
    public string AffectedType;
    private float timer;

    private void Start()
    {
        timer = 0;
    }
    private void Update()
    {
        if (GameManager.Paused)
        {
            Animator.enabled = false;
            return;
        }
        else
        {
            timer += Time.deltaTime;
            if(timer < EffectTime)
            {
                Animator.enabled = true;
            }
            else
            {
                Ray ray = new Ray(transform.position + transform.up * 2, Vector3.down);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.GetComponent<Obstacle>())
                    {
                        if(hit.transform.GetComponent<Obstacle>().Type == AffectedType)
                        Destroy(hit.transform.gameObject);
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }
}
