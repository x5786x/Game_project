using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Attack : MonoBehaviour
{
    float speed = 1;
    public SpriteRenderer sr;
    public Animator anim;
    AnimatorStateInfo animInfo;
    Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(sr.color.a < 1)
        {
            Display();
        } 
        else
        {
            anim.SetTrigger("Attack");
            animInfo = anim.GetCurrentAnimatorStateInfo(0);
            if(animInfo.normalizedTime >= 0.99f && animInfo.IsName("AttackF"))
            {
                Destroy(gameObject);
            }
        }
        
    }

    void Display()
    {
        sr.color = new Color(255, 255, 255, sr.color.a + speed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            player.DamegePlayer(4, Vector2.right);
        }
    }
}
