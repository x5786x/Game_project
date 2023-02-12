using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool isboss;
    float timer = 0;
    public float time;
    public float speed;
    public Rigidbody2D rb;
    Transform player;
    Vector2 direction;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
        direction = new Vector2(player.localScale.x, 0);
        transform.localScale = new Vector3(player.localScale.x, 1, 1);
    }

    void Update()
    {
        timer += Time.deltaTime;
        rb.velocity = direction * speed;
        if(timer >= time)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            isboss = other.gameObject.CompareTag("Boss");
            other.gameObject.GetComponent<Enemy>().TakeDamage(2, isboss);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Shield"))
        {
            other.gameObject.GetComponent<BossShield>().TakeDamage(2);
            PlayerAttack.hit = true;
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
