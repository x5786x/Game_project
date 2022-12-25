using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool isDash = false;
    float moveDir;
    static public float hitWaitingTime;
    public float dashSpeed;
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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if((!knockbacking && !isDash) || Scoreborad.Level1Animtion)
        {
            Move();
            Jump();    
        }
          
        CheckGround();
        SwitchAnimation();
        StartCoroutine("Dash");
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
    public void DamegePlayer(int damege, Transform enemy, Vector2 enemyPosition)    
    {     
        AccordingDirectionFlip(enemyPosition);
        currentstate = anim.GetCurrentAnimatorStateInfo(0);
        if(currentstate.IsName("GetHit") == false && timer >= invincibleTime)
        {             
            StartCoroutine(Knockback(knockbackTime));
            anim.SetTrigger("Hit");
            hp -= damege;
            if(hp < 0)  //人物血量不能低於0
            {
                hp = 0;
            }
            HealthBar.HealthCurrent = hp;  //血量連結
            if(hp <= 0)
            {
                Dietime();  
                Destroy(gameObject);         
            }
            BlinkPlayer(blinks,blinkTime);
            timer = 0;  
        }
    }
    IEnumerator Dash()
    {
        float dashFace;
        dashFace = this.transform.localScale.x;
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerRb.AddForce(new Vector2(playerRb.velocity.x * dashSpeed, 0), ForceMode2D.Impulse);
            isDash = true;
            yield return new WaitForSeconds(0.6f);
        }
        isDash = false;
        

    }
    IEnumerator Knockback(float seconds)
    {
        
        playerRb.velocity= new Vector2(0, 0);
        playerRb.AddForce(new Vector2(face*knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
        knockbacking = true;
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
