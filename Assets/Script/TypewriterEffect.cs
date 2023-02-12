using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{

    
    public float charsPerSecond = 0.02f;//打字时间间隔
    public bool isActive = false;
    private float timer;//计时器
    private Text myText;
    private int currentPos = 0;
    void Start()
    {
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(0.05f, charsPerSecond);
        myText = GetComponent<Text>();
        myText.text = "";//获取Text的文本信息，保存到words中，然后动态更新文本显示内容，实现打字机的效果
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
    }

    public void StartEffect()
    {
        isActive = true;
        currentPos = 0;
    }
    /// <summary>
    /// 执行打字任务
    /// </summary>
    void OnStartWriter()
    {

        if(isActive){
            Scoreborad.complete = false;
            timer += Time.deltaTime;
            if(timer>=charsPerSecond){//判断计时器时间是否到达
                timer = 0;
                currentPos++;
                myText.text = Scoreborad.signText.Substring(0,currentPos);//刷新文本显示内容
                if(currentPos>=Scoreborad.signText.Length) {
                    OnFinish();
                }
            }
        }
    }
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    public void OnFinish(){
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = Scoreborad.signText;
        Scoreborad.complete = true;
    }
}