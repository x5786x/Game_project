using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToBossroom : MonoBehaviour
{
    public GameObject bossHp;
    Vector3 playerMovePosition;
    public GameObject bossRoomPosition;   
    public BoxCollider2D check;
    public static bool bossroom = false;
    public Transform player;
    CameraFollow ca;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        check = GetComponent<BoxCollider2D>();
        playerMovePosition = bossRoomPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {  
            player.transform.position = playerMovePosition;
            bossroom = true; 
            if(Scoreborad.level == 2 || Scoreborad.level == 3)
            {
                bossHp.SetActive(true);
                BossHealthBar.BossHealthCurrent = 50;
                BossHealthBar.BossHealthMax = 50;
            }
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }
    }
}
