using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragon : Enemy
{
    public PolygonCollider2D attackCollider; 
    public float waitTime;
    void Start()
    {
        base.Start();
    }

    void Update()
    {     
        base.Update();
        timer += Time.deltaTime;
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        distance = (transform.position - playerTransform.position).sqrMagnitude;
        if(distance <= attackDistance && timer >= waitTime)
        {
            anim.SetTrigger("Attack");
            timer = 0;
        }
        if(animInfo.IsName("Fire"))
            attackCollider.enabled = true;
        else
            attackCollider.enabled = false;
    }

}
