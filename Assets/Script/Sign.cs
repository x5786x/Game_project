using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Sign : MonoBehaviour
{
    public TypewriterEffect typewriter;
    public GameObject portal;
    private Vector3 originalPostion;
    public GameObject dialogBox;
    public GameObject Mug_shot_npc_1;
    public Text dialogBoxText;
    private bool isPlayerInSign;
    GameObject player;
    public int waitingTime = 2;
    public float timer = 0;
    public int count;
    static public bool canGoToBossRoom = false;
    void Start()
    {
        typewriter = dialogBox.GetComponentInChildren<TypewriterEffect>();
        Scoreborad.signText = "知道要怎麼移動嗎?";
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
        if (Player.enemyKilled == true && count == 0)
        {
            count++; // 1
            transform.position = new Vector3(13.66f, -5, 0);
            Scoreborad.signText = "做的不錯，但你的碎片之旅程才剛\n剛開始你要蒐集四片碎片記得嗎?\n加油吧!!!";
        }
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign) //在人物範圍內就可點擊E觸發
        {
            dialogBox.SetActive(true);
            typewriter.StartEffect();
            Mug_shot_npc_1.SetActive(true);
        }
        if (count == 1)
            canGoToBossRoom = true;
        if (canGoToBossRoom)
        {
            portal.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other) //判斷是否在人物範圍內
    {
        if (count == 0)
        {
            timer = 0;
            Scoreborad.signText = "知道要怎麼移動嗎?";
            dialogBoxText.text = Scoreborad.signText;
            dialogBox.SetActive(true);
            Mug_shot_npc_1.SetActive(true);
        }
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)  //判斷是否在人物範圍外，事就取消顯示ui
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
            Mug_shot_npc_1.SetActive(false);
            typewriter.OnFinish();
            typewriter.StartEffect();
        }

    }
}
