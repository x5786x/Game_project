using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMirror :  MonoBehaviour
{
    public GameObject generateMirrorPosition;
    static public bool getmirror = false;
    public GameObject mirror1;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        getmirror = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Scoreborad.bossDown)
        {
            Instantiate(mirror1, generateMirrorPosition.transform.position, new Quaternion(0, 0, 0 ,0));
            Scoreborad.bossDown = false;
        }
        if(Scoreborad.level == 1)
        {
            if(Goodbigsmile.end && getmirror)
                portal.SetActive(true);
        }
        else
        {   
            if(getmirror)
                portal.SetActive(true);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Mirror")
        {
            Destroy(GameObject.FindGameObjectWithTag("Mirror"));
            getmirror = true;
            if(Scoreborad.score <= 3)
                Scoreborad.score = Scoreborad.level + 1;
        }
    }
}
 
