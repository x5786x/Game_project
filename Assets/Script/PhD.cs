using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhD : SignCommon
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Scoreborad.signText = "吼!我的一個實驗品不見了。你能幫我找到他嗎?";
    }
}
