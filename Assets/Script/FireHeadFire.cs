using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeadFire : Enemy
{

    public float attackDelayTime;
    public float moveSpeed = 3.0f;
    public float trackMax;
    public float trackMin;
    private float distance;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(playerTransform != null)
        {  
            distance = (transform.position - playerTransform.position).sqrMagnitude; // 計算兩點距離
            if(distance <= trackMax && distance >= trackMin && timer >= attackDelayTime)
            {          
                
                if(playerTransform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (playerTransform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(moveSpeed, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }                 
                
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

}
