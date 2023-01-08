using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyM : Enemy
{
    public BoxCollider2D originalColliderPostion;
    public BoxCollider2D attackCollider;
    public float attackDelayTime;
    public float moveSpeed = 3.0f;
    public float trackMax;
    public float trackMin;
    float distance;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        originalColliderPostion = GetComponent<BoxCollider2D>();
        attackCollider = GameObject.FindGameObjectWithTag("Mattack").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(hp <= 0)
        {
            Player.enemyKilled = true;
        }
        if(!animInfo.IsName("Attack"))
        {
            timer += Time.deltaTime;
            if(playerTransform != null)
            {
                distance = (transform.position - playerTransform.position).sqrMagnitude; // 計算兩點距離
                if(distance <= trackMax && distance >= trackMin)
                {
                    anim.SetBool("Move", true);            
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
                    anim.SetBool("Move", false);
                    rb.velocity = new Vector2(0, 0);
                }
            }
            if(distance <= attackDistance)
            {                            
                anim.SetBool("Move", false);      
                if(timer >= attackDelayTime)
                {
                    anim.SetTrigger("Attack");
                    timer = 0;
                }    
                if(animInfo.IsName("Attack"))
                    rb.velocity = new Vector2(0, 0);    
            } 
        }
    }
    public void MAttackEventOn()
    {       
        attackCollider.enabled = true;
        
    }
    public void MAttackEventOff()
    {
        attackCollider.enabled = false;
    }
    
}
