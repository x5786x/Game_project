using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Sign : SignCommon
{
    public AudioSource music;
    bool on = false;
    public Animator anim;
    public AnimatorStateInfo animInfo;
    public GameObject boss, bossShield;
    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        Scoreborad.signText = "太好了你已經收集完碎片了，我就知道你可以辦到";
    }
    void Update()
    {
        
        base.Update();  
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        switch(count)
        {
            case 1:
                Scoreborad.signText = "我到現在好像都沒自我介紹";
                turnon = false;
                break;
            case 2:
                Scoreborad.signText = "我就是天使最後的子嗣”墮天使·加百列”";
                break;
            case 3:
                Scoreborad.signText = "感謝你幫我蒐集碎片了，其實你也是天使的一員，但是你自甘墮落了成為了”迷失者”";
                break;
            case 4:
                Scoreborad.signText = "我的能力可以讓你再度成為天使與我一起開創新的紀元，但你得先把碎片交給我";
                break;
            case 5:
                Scoreborad.signText = "或者你可以用你那微弱的力量打敗我奪取的碎片，但那是不可能的:)";
                break;        
        }
        if(count == 1 && Scoreborad.complete)
        {
            count = 2;
            Scoreborad.complete = false;
            StartCoroutine(Event());
        }
        if(count >= 5)
        {
            turnon = false;
            StartCoroutine(Distroy());
        }
            
    }
    IEnumerator Event()
    {
        yield return new WaitForSeconds(2);
        dialogBox.SetActive(false);
        anim.SetBool("Event", true);
        yield return new WaitForSeconds(2);
        dialogBoxText.text = "";
        dialogBox.SetActive(true);
        typewriter.StartEffect();
        turnon = true;
    }
    IEnumerator Distroy()
    {
        yield return new WaitForSeconds(5);
        boss.SetActive(true);
        bossShield.SetActive(true);
        music.Play();
        Destroy(gameObject);
    }
}


