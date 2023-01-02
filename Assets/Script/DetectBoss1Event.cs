using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoss1Event : MonoBehaviour
{
    public GameObject eventScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        Scoreborad.eventOn = true;
        eventScript.GetComponent<Boss1Event>().enabled = true;
    }
}
