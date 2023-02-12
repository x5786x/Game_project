using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool shoot = false;
    float timer = 0;
    public float attackDelayTime;
    AnimatorStateInfo currentstate;
    Animator anim;
    public int damage;
    public bool isboss;
    public AudioSource arrowAudio;
    public GameObject attackAudio;
    public GameObject arrow;
    public GameObject arrowPosition;
    public static bool hit = false;
    static public bool attacking;
    public float freezeFrameDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    void Update()
    {
        Attack();
        BowAttack();
        if(hit)
        {
            StartCoroutine("FreezeFrame");
        }
    }

    void Attack()
    {
        timer += Time.deltaTime;
        currentstate = anim.GetCurrentAnimatorStateInfo(0);
        if(timer >= attackDelayTime)
        {
            if(Input.GetKeyDown(KeyCode.J) && (currentstate.IsName("Attack") == false))
            {
                anim.SetTrigger("Attack");
                Instantiate(attackAudio);
                timer = 0;
            }
        }   
    }
    void BowAttack()
    {
        currentstate = anim.GetCurrentAnimatorStateInfo(0);
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(!currentstate.IsName("Bow"))
            {
                attacking = true;
                anim.SetTrigger("Bow");
                shoot = true;
            }     
        } 
        if(currentstate.normalizedTime >= 0.875f && currentstate.IsName("Bow") && shoot)
        {
            arrowAudio.Play();
            Instantiate(arrow, arrowPosition.transform.position, new Quaternion(0, 0, 0, 0));
            shoot = false;
            attacking = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if(other.CompareTag("Enemy"))
            {  
                isboss = false;
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage, isboss);
                if(transform.localScale.x > 0)
                {
                    other.gameObject.GetComponent<Enemy>().GetHit(Vector2.right);
                }
                else if (transform.localScale.x < 0)
                {
                    other.gameObject.GetComponent<Enemy>().GetHit(Vector2.left);
                }
            }
            else if(other.CompareTag("Boss"))
            {
                isboss = true;
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage, isboss);
            }
            hit = true;
        }
        else if(other.CompareTag("Shield"))
        {
            other.gameObject.GetComponent<BossShield>().TakeDamage(damage);
            hit = true;
        }
    }

    IEnumerator FreezeFrame()
    {
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        yield return new WaitForSecondsRealtime(freezeFrameDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        hit = false;
    }
}

