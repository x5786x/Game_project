using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource hitAudio;
    public float distance;
    public float timer;
    private Vector2 direction;
    public float attackDistance;
    public int hp;
    public int damage;
    public float falshTime;
    private Vector3 hitEffectPostion;
    public GameObject hitEffect;
    public Transform playerTransform;
    public Animator anim;
    public SpriteRenderer srenderer;
    private Player playerHp;
    public Color originalColor;
    public Rigidbody2D rb;
    public AnimatorStateInfo animInfo;
    public float moveForce = 3.0f;
    private Vector2 enemyPosition;
    public GameObject moveLimitMin;
    public GameObject moveLimitMax;
    // Start is called before the first frame update
    public void Start()
    {
        timer = 0;
        BossHealthBar.BossHealthMax = hp;
        BossHealthBar.BossHealthCurrent = hp;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        srenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = srenderer.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    public void Hit(float falshTime)
    {
        srenderer.color = Color.red;
        Invoke("ResetColor", falshTime);
    }
    public void GetHit(Vector2 direction)
    {
        transform.localScale = new Vector3(direction.x, 1, 1); // 朝向玩家方向
        this.direction = new Vector2(direction.x, direction.y);
    }
    public void ResetColor()
    {
        srenderer.color = originalColor;
    }

    public void TakeDamage(int damage, bool isboss) // 判斷敵人是否為boss和敵人受傷
    {  
        hp -= damage;
        hitAudio.Play();
        hitEffectPostion = new Vector3(transform.position.x, transform.position.y, -5); // z軸負值才正常顯示   
        Hit(falshTime);
        if(isboss == true)
        {
            if(hp <= 0)
            {
                Player.enemyKilled = true;
                hp = 0;
                Scoreborad.bossDown = true;
                GoToBossroom.bossroom = false;
            }
            BossHealthBar.BossHealthCurrent = hp;
        }
        else
        {
            Instantiate(hitEffect, hitEffectPostion, Quaternion.identity);
            rb.AddForce(new Vector2(-moveForce, rb.velocity.y), ForceMode2D.Impulse);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//偵測碰撞然後受傷
        {
            if(hp != null)
            {
                timer = 0;
                enemyPosition = rb.worldCenterOfMass;
                playerHp.DamegePlayer(damage, enemyPosition);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(hp != null)
            {
                timer = 0;
                enemyPosition = rb.worldCenterOfMass;
                playerHp.DamegePlayer(damage, enemyPosition);
                int playerLayer = other.gameObject.layer;
                StartCoroutine(CollisionOff(Player.hitWaitingTime, playerLayer));
            }
        }
    }

    IEnumerator CollisionOff(float waitTime, int player)
    {
        Physics2D.IgnoreLayerCollision(8, 9); // 忽視
        yield return new WaitForSeconds(waitTime);
        Physics2D.IgnoreLayerCollision(8, 9, false); // 開啟
    }
}
    
