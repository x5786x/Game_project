using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathFloor : MonoBehaviour
{
    public Player playerHp;   
    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        playerHp.DamegePlayer(20, Vector2.up);
    }
}
