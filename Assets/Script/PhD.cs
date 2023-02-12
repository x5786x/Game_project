using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhD : SignCommon
{
    public GameObject protal;
    void Start()
    {
        base.Start();
        Scoreborad.complete = false;
    }

    
    void Update()
    {
        base.Update();
        Scoreborad.signText = "吼!我的一個實驗品不見了。你能幫我找到他嗎?";
        if(Scoreborad.complete)
            protal.SetActive(true);
    }
}
