using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public Animator anim;
    public float falshTime;
    Color originalColor;
    public SpriteRenderer srenderer;
    public float hp;
    public static bool bossShieldBroken = false;
    void Start()
    {  
        originalColor = srenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
            bossShieldBroken = true;
        }
        else if(hp <= 10)
        {
            anim.SetBool("3", true);
        }
        else if(hp <= 25)
        {
            anim.SetBool("2", true);
        }
        
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Hit(falshTime);
    }
    public void Hit(float falshTime)
    {
        srenderer.color = new Color(1, 1, 1, 0.5f);
        Invoke("ResetColor", falshTime);
    }
    public void ResetColor()
    {
        srenderer.color = originalColor;
    }
}
