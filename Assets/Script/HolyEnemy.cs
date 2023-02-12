using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyEnemy : MonoBehaviour
{
    float speed = 1f;
    public Animator anim;
    AnimatorStateInfo animInfo;
    Player player;
    bool check;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(transform.localScale.x == 1)
            check = false;
        else
            check = true;
    }

    void Update()
    {
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(animInfo.normalizedTime >= 0.99f)
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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
