using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Trigger : MonoBehaviour
{
    Player player;
    ParticleSystem ps;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ps = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("Player"))
        {
            
            player.DamegePlayer(2, Vector2.right);
        }    
        
    }
}
