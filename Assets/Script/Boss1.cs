using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    public GameObject BossHpBar;
    float distance;
    public float attackDelayTime;
    float timer = 0;
    AnimatorStateInfo attacking;
    public CapsuleCollider2D original;
    public GameObject mirror;
    public GameObject goodbigsmile;
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
            if(distance <= attackDistance)
            {                  
                if(timer >= attackDelayTime)
                {
                    BossHpBar.SetActive(true);
                    anim.SetTrigger("Attack");
                    timer = 0;
                }        
            } 
        }
    }
    public void AttackOn()
    {
        original.enabled = false;
    }
    public void Recover()
    {
        original.enabled = true;
    }
    
}