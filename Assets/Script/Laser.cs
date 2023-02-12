using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    float speed = 1f;
    public SpriteRenderer sr;
    public Animator anim;
    AnimatorStateInfo animInfo;
    Player player;
    bool check, start = true;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(gameObject.transform.rotation.z >= 0)
            check = true;
        else
            check = false;
        
    }

    void Update()
    {
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(start)
        {
            if(sr.color.a >= 1)
            {
                start = false;
                StartCoroutine(Shoot());
            }
            sr.color = new Color(255, 255, 255, sr.color.a+speed*Time.deltaTime);
        }
        if(animInfo.IsName("Laser") && animInfo.normalizedTime >= 0.99)
            StartCoroutine(Distory());
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(check)
                player.DamegePlayer(4, Vector2.right);
            else
                player.DamegePlayer(4, Vector2.left); 
        }
    }
    IEnumerator Distory()
    {
        while(sr.color.a > 0)
            sr.color = new Color(255, 255, 255, sr.color.a-speed*Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Laser", true);
    }
}
