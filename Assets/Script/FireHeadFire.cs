using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeadFire : Enemy
{
    float x;
    public float attackDelayTime;
    public float moveSpeed = 3.0f;
    public float trackMax;
    public float trackMin;
    float min;
    float max;
    bool right = false;
    bool left = false;
    bool both = true;
    int count = 0;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        min = moveLimitMin.transform.position.x;
        max = moveLimitMax.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(playerTransform != null)
        {  
            distance = (transform.position - playerTransform.position).sqrMagnitude; // 計算兩點距離
            Over();
            if(both)
                Track();
            else if(!both && count == 0)
            {
                rb.velocity = Vector2.zero;
                count++;
            }
            else
            {
                if(distance <= trackMax && distance >= trackMin && timer >= attackDelayTime)
                {      
                    if(right)
                    {
                        count = 0;
                        if(playerTransform.position.x > transform.position.x)
                        {
                            rb.velocity = new Vector2(moveSpeed, 0);
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }               
                    }  
                    else if(left)
                    {
                        count = 0;
                        if(playerTransform.position.x < transform.position.x)
                        {
                            rb.velocity = new Vector2(-moveSpeed, 0);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    } 
                }
            }
        }  
    }
    void Track()
    {
        if(distance <= trackMax && distance >= trackMin && timer >= attackDelayTime)
        {        
            if(playerTransform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if(playerTransform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    void Over()
    {
        if(transform.position.x < min || transform.position.x > max)
        {
            both = false;
            if(transform.position.x < min)
                right = true;
            else
                left = true;
        }
        else
        {
            both = true;
            right = left = false;
        }
    }
}
