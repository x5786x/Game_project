using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timer = 0;
    public float attackDelayTime;
    AnimatorStateInfo currentstate;
    private Animator anim;
    public int damage;
    public bool isboss;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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
                timer = 0;
            }
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy" )
        {  
            isboss = false;
            other.GetComponent<Enemy>().TakeDamage(damage, isboss);
            if(transform.localScale.x > 0)
            {
                other.GetComponent<Enemy>().GetHit(Vector2.right);
            }
            else if (transform.localScale.x < 0)
            {
                other.GetComponent<Enemy>().GetHit(Vector2.left);
            }
        }
        else if(other.gameObject.tag == "Boss")
        {
            isboss = true;
            other.GetComponent<Enemy>().TakeDamage(damage, isboss);
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy" )
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
        else if(other.gameObject.tag == "Boss")
        {
            isboss = true;
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage, isboss);
        }
    }
        
}

