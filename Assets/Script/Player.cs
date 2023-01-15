using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool playerDead;
    float moveDir;
    static public float hitWaitingTime;
    bool canDash = true;
    bool dashing = false;
    public float dashTime;
    public float dashSpeed;
    public float dashCooldownTime;
    public float knockbackTime;
    private bool knockbacking = false;
    private int direction;
    private int face;
    public float knockbackForceX;
    public float knockbackForceY;
    public int hp;
    public bool doubleJump;
    public float doubleJumpSpeed;
    public float jumpSpeed;
    public bool isGround;
    public BoxCollider2D feet;
    public Rigidbody2D playerRb;
    public Renderer render;
    public Animator anim;
    private PolygonCollider2D poly;
    AnimatorStateInfo currentstate;
    private float timer;
    public float invincibleTime = 0.7f;
    public float moveSpeed = 4.5f;
    public int blinks;
    public float blinkTime;
    static public bool teach1 = false; 
    static public bool move = false;
    static public bool enemyKilled = false;
    static public bool idleChange = false;
    [SerializeField] GameObject ReplayButton;
    [SerializeField] GameObject MainButton;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = hp;
        HealthBar.HealthCurrent = hp;
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        poly = GameObject.FindGameObjectWithTag("PlayerAttack").GetComponent<PolygonCollider2D>();
        hitWaitingTime = knockbackTime;
        playerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!playerDead)
        {
            if(!Scoreborad.eventOn)
            {
            if((!knockbacking && !dashing))
            {
                Move();
                Jump();    
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                anim.SetTrigger("Dash");
                StartCoroutine("Dash");
            }
            SwitchAnimation(); 
            }   
            if(Scoreborad.eventOn)
            {
                playerRb.velocity = Vector2.down;
                anim.Play("Idle", 0, 0);
            }
            CheckGround();  
        }
        if(hp <= 0)
        {
            playerDeath();            
        }    
         
    }
    
    void FixedUpdate() 
    {
        if(!playerDead && !Scoreborad.eventOn)
        {
            if((!knockbacking && !dashing))
            {
                Move();   
            }
        }
    }
    void CheckGround()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    void SwitchAnimation()
    {   
        if(idleChange == true)
        {
            anim.SetBool("Idle", true);
        }
        anim.SetBool("Idle", false);
        if(anim.GetBool("Jump"))
        {
            if(playerRb.velocity.y < 0.1f)
            {
                anim.SetBool("Jump", false);     
                anim.SetBool("Fall", true);
            }
        }
        else if(isGround)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Fall", false);
        }
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {

                anim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0f, jumpSpeed);
                playerRb.velocity = Vector2.up * jumpVel;
                doubleJump = true;
            }
            else
            {
                if(doubleJump)
                {
                    anim.SetBool("Jump", true);
                    Vector2 doubleJumpVel = new Vector2(0f, doubleJumpSpeed);
                    playerRb.velocity = Vector2.up * doubleJumpVel;
                    doubleJump = false;
                }
            }
        }
    }
    void Move()
    {
        moveDir = Input.GetAxis("Horizontal");
        bool run = Mathf.Abs(moveDir) > 0;
        anim.SetBool("Run", run);
        if(moveDir > 0.1f)
            this.transform.localScale = new Vector3(1, 1, 1);
        else if(moveDir < -0.1f)
            this.transform.localScale = new Vector3(-1, 1, 1);
        
        playerRb.velocity = new Vector2(moveDir*moveSpeed, playerRb.velocity.y);  
    }
    public void AccordingDirectionFlip(Vector2 enemyPosition)
    {
        face = (enemyPosition.x < transform.position.x) ? 1 : -1; // -1敵人在左邊 1敵人在右邊  
        transform.localScale = new Vector2(-face, 1);
    }
    public void DamegePlayer(int damege, Vector2 enemyPosition)    
    {     
        AccordingDirectionFlip(enemyPosition);
        currentstate = anim.GetCurrentAnimatorStateInfo(0);
        if(currentstate.IsName("GetHit") == false && timer >= invincibleTime)
        {             
            StartCoroutine(Knockback(knockbackTime));
            anim.SetTrigger("Hit");
            hp -= damege;
            if(hp <= 0)  //人物血量不能低於0
            {
                hp = 0;
            }
            HealthBar.HealthCurrent = hp;  //血量連結
            
            BlinkPlayer(blinks,blinkTime);
            timer = 0;  
        }
    }
    void playerDeath()
    {
        anim.SetBool("Death", true);
        playerDead = true;
        currentstate = anim.GetCurrentAnimatorStateInfo(0);
        if(currentstate.normalizedTime >= 0.95 && currentstate.IsName("Death"))
        {     
            Dietime();     
            Destroy(gameObject);          
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        playerRb.gravityScale = 0;  // 重力調為0 不然跳躍Dash時會往下掉
        playerRb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        playerRb.gravityScale = 1;
        dashing = false;
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }
    IEnumerator Knockback(float seconds)
    {
        knockbacking = true;
        playerRb.AddForce(new Vector2(face*knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
        yield return new WaitForSeconds(seconds);
        knockbacking = false;
    }
    void BlinkPlayer(int numBlink, float seconds)   //人物受傷閃爍
    {
        StartCoroutine(DoBlink(numBlink,seconds));
    }
    IEnumerator DoBlink(int numBlink, float seconds)            
    {
        for(int i=0; i<numBlink*2;i++)
        {
            render.enabled = !render.enabled;
            yield return new WaitForSeconds(seconds);
        }
        render.enabled = true ;
    }
    void AttackEventOn()
    {
        poly.enabled = true;
    }
    void AttackEventOff()
    {
        poly.enabled = false;
    }
    void Dietime()
    {
        Time.timeScale = 0f;
        ReplayButton.SetActive(true);
        MainButton.SetActive(true);
    }
    
}
