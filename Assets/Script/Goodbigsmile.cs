using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Goodbigsmile : MonoBehaviour
{
    public TypewriterEffect typewriter;
    public int count;
    public int waitingTime = 3;
    public float timer = 0;
    public GameObject dialogBox;
    public GameObject abc;
    public GameObject Mug_shot_npc_2;
    public Text dialogBoxText;
    public bool isPlayerInSign;
    static public bool end;
    // Start is called before the first frame update
    void Start()
    {
        typewriter = dialogBox.GetComponentInChildren<TypewriterEffect>();
        end = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && isPlayerInSign && count!=3) //在人物範圍內就可點擊E觸發
        {       
            Mug_shot_npc_2.SetActive(true);
            if(count == 0)
                Scoreborad.signText = "你好先生 嘻嘻嘻~~";               
            if(count == 1)
                Scoreborad.signText = "你怎會出現在這裡，這裡可不歡迎你喔 嘻嘻嘻~~";
            if(count == 2)
                Scoreborad.signText = "喔~ 碎片那，應該是遺落之境的碎片吧! 嘻嘻嘻";
            if(count == 3)
            {      
                Scoreborad.signText = "喔~ 你有一股特殊的氣息 嘻嘻嘻。";
                timer = 0;
                dialogBox.SetActive(false);
            }   
            count++;
            dialogBox.SetActive(true);   
            typewriter.StartEffect();
        }
        if(count >= 4 && timer >= 2.0f)
        {
            end = true;
            abc.transform.position += new Vector3(1f,0,0);   
            Invoke("Destroy", 1);
        }   
    }
    void OnTriggerEnter2D(Collider2D other) //判斷是否在人物範圍內
    {
        
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")         
        {
            isPlayerInSign = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D other)  //判斷是否在人物範圍外，是就取消顯示ui
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
            Mug_shot_npc_2.SetActive(false);
            typewriter.OnFinish();
            typewriter.StartEffect();
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
 
}
