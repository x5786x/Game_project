using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Sign : MonoBehaviour
{
    public GameObject portal;
    private Vector3 originalPostion ;
    public GameObject dialogBox;
    public GameObject Mug_shot_npc_1;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerInSign;
    GameObject player;
    public int waitingTime = 2;
    public float timer = 0;
    public int count;
    static public bool canGoToBossRoom = false;
    void Start()
    {    
        count = 0;
        Player.teach1 = false; 
        Player.move = false;
        Player.enemyKilled = false;
        Player.idleChange = false;
        isPlayerInSign = false;
    }   
    void Update()
    {
        timer += Time.deltaTime;
        if(Player.enemyKilled == true && count == 0)
        {
            count++; // 1
            transform.position = new Vector3 (13.66f, -5, 0);
            signText = "做的不錯，但你的碎片之旅程才剛剛開始你要蒐集四片碎片記得嗎?  加油吧!!!";
            dialogBoxText.text = signText;
        }
        if(Input.GetKeyDown(KeyCode.E) && isPlayerInSign) //在人物範圍內就可點擊E觸發
        { 
            if(count == 1)
            {
                timer = 0;
                count++;
            }
            dialogBoxText.text = signText;
            dialogBox.SetActive(true);
            Mug_shot_npc_1.SetActive(true);
        }
        if(count == 2)
            canGoToBossRoom = true;
        if(canGoToBossRoom)
        {
            portal.SetActive(true);
        }
    }
    void Teaching1()
    {
        timer = 0;
        Player.teach1 = true;
        if(Player.move == true)
        {
            dialogBox.SetActive(false);
            Mug_shot_npc_1.SetActive(false);

            count++; // 1
        }
    }
    void OnTriggerEnter2D(Collider2D other) //判斷是否在人物範圍內
    {
        if(count == 1)
        {
            timer = 0;
            signText = "看來前面有一隻怪物希望你還記得怎麼戰鬥";
            dialogBoxText.text = signText;
            dialogBox.SetActive(true);  
            Mug_shot_npc_1.SetActive(true);
 
        }
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")         
        {
          isPlayerInSign = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D other)  //判斷是否在人物範圍外，事就取消顯示ui
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
            Mug_shot_npc_1.SetActive(false);

        }
        
    }
}

