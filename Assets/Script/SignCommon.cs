using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignCommon : MonoBehaviour
{
    public TypewriterEffect typewriter;
    public GameObject dialogBox;
    public Text dialogBoxText;
    public bool isPlayerInSign;
    public float timer = 0;
    public int count;
    // Start is called before the first frame update
    public void Start()
    {
        typewriter = dialogBox.GetComponentInChildren<TypewriterEffect>();
        count = 0;
        isPlayerInSign = false;
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign) //在人物範圍內就可點擊E觸發
        {
            dialogBoxText.text = Scoreborad.signText;
            dialogBox.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D other) //判斷是否在人物範圍內
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
        }

    }
    public void OnTriggerExit2D(Collider2D other)  //判斷是否在人物範圍外，事就取消顯示ui
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
            typewriter.OnFinish();
            typewriter.StartEffect();
        }
    }
}
