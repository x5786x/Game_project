using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss1 : Enemy
{
    float distance;
    public float attackDelayTime;
    AnimatorStateInfo attacking;
    public CapsuleCollider2D original;
    public GameObject mirror;
    public GameObject goodbigsmile;
    public ParticleSystem attack2Effect;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();  
        Scoreborad.bossDown = false;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {   
        if(hp <= 0)
        {    
            Scoreborad.bossDown = true;
            goodbigsmile.SetActive(true);
        } 
        base.Update();
        attacking = anim.GetCurrentAnimatorStateInfo(0);
        if(attacking.IsName("Attack") == false)
        {
            distance = Mathf.Abs(63.29f - playerTransform.position.x);
            timer += Time.deltaTime;
            if(distance >= attackDistance)
            {    
                if(timer >= attackDelayTime)
                {              
                    anim.SetTrigger("Attack2");
                    timer = 0; 
                } 
            } 
            else
            {
                int count;         
                count = Random.Range(0, 2); // 0~1
                if(timer >= attackDelayTime)
                {
                    switch(count)
                    {
                        case 0:
                            anim.SetTrigger("Attack");
                            break;
                        case 1:
                            anim.SetTrigger("Attack2");
                            break;
                    }            
                    timer = 0;
                } 
            }
        }
    }
    void Attack2Event()
    {
        int time = 2;
        attack2Effect.Play();
        Scoreborad.boss1Attack2 = true;
        while(timer < time)
        {
            timer += Time.deltaTime;
        }
        timer = 0;
    }
}