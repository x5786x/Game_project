using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : Enemy
{
    float x;
    public float attackDelayTime;
    public float moveSpeed = 3.0f;
    public float trackMax;
    public float trackMin;
    float min;
    float max;
    Vector3 playerPosition;
    bool right = false;
    bool left = false;
    bool both = true;
    bool done = false;
    int count = 0;
    public GameObject attack;
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
                    done = false;
                    anim.SetBool("Move", false);
                    rb.velocity = Vector2.zero;    
                    anim.SetTrigger("Attack");
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
                                anim.SetBool("Move", true);
                                count = 0;
                                if(playerTransform.position.x > transform.position.x)
                                {
                                    rb.velocity = new Vector2(moveSpeed, 0);
                                    transform.localScale = new Vector3(1, 1, 1);
                                }               
                            }  
                            else if(left)
                            {
                                anim.SetBool("Move", true);
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
            if(animInfo.IsName("Attack") && done == false)
            {
                playerPosition = new Vector3(playerTransform.position.x + 1.67f , playerTransform.position.y + 1.96f, -5);
                if(animInfo.normalizedTime >= 0.8f)
                {
                    Instantiate(attack, playerPosition, new Quaternion(0, 0, 0, 0));
                    timer = 0;
                    done = true;
                }
            }
        }  
    }
    void Track()
    {
        if(distance <= trackMax && distance >= trackMin)
        {        
            
            anim.SetBool("Move", true);
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
                left = true;
            else  
                right = true;
        }
        else
        {
            both = true;
            right = left = false;
        }
    }
}
