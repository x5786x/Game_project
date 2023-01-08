using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Trigger : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject destination; 
    Vector2 originalPosition;
    float timer = 0;
    Animator attackAnim;
    public BoxCollider2D attackCollider;
    public Player playerHp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        originalPosition = transform.position;
        attackAnim = GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Scoreborad.boss1Attack2)
        {
            timer += Time.deltaTime;  
            attackAnim.SetBool("Attack2On", true);
            rb.velocity = new Vector2(-6.5f, 0);   
            if(timer >= 3)
            {
                timer = 0;
                Scoreborad.boss1Attack2 = false;
                attackAnim.SetBool("Attack2On", false);
            }
        }
        else
        {
            transform.position = originalPosition;
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerHp.DamegePlayer(2, Vector2.right);
        }
    }
}
