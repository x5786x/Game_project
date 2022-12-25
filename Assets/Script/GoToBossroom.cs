using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToBossroom : MonoBehaviour
{
    
    public BoxCollider2D check;
    public static bool bossroom = false;
    public Transform player;
    CameraFollow ca;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        check = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Sign.canGoToBossRoom)
        {  
            player.transform.position = new Vector3 (49, -5, 0);
            bossroom = true;
        }
    }
}
