using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3M : Enemy
{
    float x;
    public float attackDelayTime;
    public float moveSpeed = 3.0f;
    public float trackMax;
    public float trackMin;
    public BoxCollider2D attackCollider;
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
        Destroy(moveLimitMin);
        Destroy(moveLimitMax);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(playerTransform != null)
        {  
            distance = (transform.position - playerTransform.position).sqrMagnitude; // 計算兩點距離
            Over();
            if(!animInfo.IsName("Attack") && timer > attackDelayTime)
            {
                if(distance <= attackDistance)
                {
                    rb.velocity = Vector2.zero;    
                    anim.SetTrigger("Attack");
                    timer = 0;         
                }
                else
                {
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
                                    transform.localScale = new Vector3(1, 1, 1);
                                }               
                            }  
                            else if(left)
                            {
                                count = 0;
                                if(playerTransform.position.x < transform.position.x)
                                {
                                    rb.velocity = new Vector2(-moveSpeed, 0);
                                    transform.localScale = new Vector3(-1, 1, 1);
                                }
                            } 
                        }
                    }
                }
            } 
        }  
    }
    void Track()
    {
        if(distance <= trackMax && distance >= trackMin)
        {        
            if(playerTransform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }
            if(playerTransform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
                transform.localScale = new Vector3(-1, 1, 1);
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

    public void AttackEventOn()
    {       
        attackCollider.enabled = true;    
    }
    public void AttackEventOff()
    {
        attackCollider.enabled = false;
    }
}
