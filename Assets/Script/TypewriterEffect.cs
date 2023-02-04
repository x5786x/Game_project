using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour {

    public float charsPerSecond = 0.2f;
    private Text myText;
    public bool isActive = false;
    private float timer;
    public int currentPos=0;

    // Use this for initialization
    void Start () {
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max (0.2f,charsPerSecond);
        myText = GetComponent<Text>();
        myText.text = "";
    }

    // Update is called once per frame
    void Update () {
        OnStartWriter ();
    }

    public void StartEffect(){
        isActive = true;
        currentPos = 0;
    }

    void OnStartWriter(){
        if(isActive){
            timer += Time.deltaTime;
            if(timer>=charsPerSecond){
                timer = 0;
                currentPos++;
                myText.text = Scoreborad.signText.Substring(0,currentPos);
                if(currentPos>=Scoreborad.signText.Length) {
                    OnFinish();
                }
            }
        }
    }

    public void OnFinish(){
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = Scoreborad.signText;
    }
}