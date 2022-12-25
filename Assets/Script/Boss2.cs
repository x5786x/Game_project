using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    private float time = 0;
    public float attackDelayTime = 3;
    private int count;
    private float playerLastPositionX;
    private int animSpeed = 0;
    private float animTime;
    private float distance;
    public float jumpSpeed;
    private Vector2 jumpVel;
    private Vector2 boss2Position;
    public BoxCollider2D bossCollider;
    public BoxCollider2D downAttackCollider;
    
    void Start()
    {
        base.Start();
        count = 0;
        jumpVel = new Vector2(0f, jumpSpeed);
        bossCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        base.Update();   
        time += Time.deltaTime;     
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        animTime = animInfo.normalizedTime;
        boss2Position = new Vector2(bossCollider.bounds.center.x - transform.position.x, transform.position.y); 
        distance = Vector2.Distance(playerTransform.position, boss2Position);
        if(distance < attackDistance && time > attackDelayTime)
        {
            if(!animInfo.IsName("jumpAttack"))
                anim.SetTrigger("JumpAttack");
        }
        if(animInfo.IsName("jumpAttack")) // JumpAttack總共40幀
        {
            if(animTime > 0.375f && animTime < 0.625f)  // 暫停跳躍動畫 跳躍動畫第13幀到25幀 動作:執行跳躍動畫
            {
                PlayAnim();
                anim.speed = animSpeed;
                if(boss2Position.y < 0)
                    JumpAttack();
                playerLastPositionX = playerTransform.position.x; // 拿到玩家最後的位置
            }
            else if(animTime > 0.975f && animTime < 0.99f) // 動作:移動至玩家x軸位置 並進行落下
            {
                if(count == 0)
                {
                    animSpeed = 0;
                    count++;
                }
                CheckGround();     
                anim.speed = animSpeed;
                rb.velocity = Vector2.down * jumpVel;
            }
            else 
            {
                rb.velocity = Vector2.zero;
                animSpeed = 0;
                time = 0;
                JumpAttackEventOver();
            }
        }
    }
    void JumpAttack()
    {
        rb.velocity = Vector2.up * jumpVel;
    }

    void PlayAnim()
    {
        if(boss2Position.y > 8) // 跳出畫面外
        {
            animSpeed = 1;       
            transform.position = new Vector2(playerLastPositionX - boss2Position.x, boss2Position.y); // 將Boss移至玩家正上方
        }
    }
    void CheckGround()
    {
        if(downAttackCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animSpeed = 1;
        }
        
    }
    void JumpAttackEventDownAttack()
    {
        bossCollider.enabled = false;
        downAttackCollider.enabled = true;
    }
    void JumpAttackEventOver()
    {
        bossCollider.enabled = true;
        downAttackCollider.enabled = false;
    }
}
