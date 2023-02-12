using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class teach : MonoBehaviour
{
    public TypewriterEffect typewriter;

    public GameObject dialogBox;
    public GameObject sign;
    public Text dialogBoxText;
    public bool isPlayerInSign;
    public int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        typewriter = dialogBox.GetComponentInChildren<TypewriterEffect>();
        sign.SetActive(true);
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign) //在人物範圍內就可點擊E觸發
        {
            switch (Scoreborad.count)
            {
                case 0:
                    {
                        Scoreborad.signText = "嗨嗨我是告示牌";
                        break;
                    }
                case 1:
                    {   

                        Scoreborad.signText = "\"A\"\"D\"移動空白鍵跳躍\"Shift\"進行衝刺";
                        break;
                    }
                case 2:
                    {
                        Scoreborad.signText = "\"J\"鍵進行普通攻擊，\"K\"鍵進行射擊";
                        break;
                    }
                case 3:
                    {
                        Scoreborad.signText = "加油!!!";
                        Scoreborad.count = -1;
                        break;
                    }
            }    
            Scoreborad.count++;
            dialogBox.SetActive(true);
            typewriter.StartEffect();
        }
    }
    void OnTriggerEnter2D(Collider2D other) //判斷是否在人物範圍內
    {
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
            count = 0;
            typewriter.OnFinish();
        }
    }
}
