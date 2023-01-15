using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class teach : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject sign;
    public Text dialogBoxText;
    public bool isPlayerInSign;
    public int count = 0;
    public string signText;
    // Start is called before the first frame update
    void Start()
    {
        sign.SetActive(true); 
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isPlayerInSign) //在人物範圍內就可點擊E觸發
        {       
            dialogBox.SetActive(true);
            if(count == 0)
                signText = "嗨嗨我是告示牌~~";               
            if(count == 1) 
                signText = "\"A、D左右移\"動，\"空白鍵\"跳躍，\"SHIFT\"進行閃現。";               
            if(count == 2)
                signText = "\"J\"鍵進行攻擊。";
            if(count == 3){
                signText = "加油!!!";
                count = 0;
        }
            count++;
            dialogBoxText.text = signText;
            dialogBox.SetActive(true);   
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
            

        }
    }
}
