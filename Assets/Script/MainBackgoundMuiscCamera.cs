using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainBackgoundMuiscCamera : MonoBehaviour
{
    public GameObject sound;
    GameObject BGM = null;
    void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("Sound");
        if(BGM == null)
        {
            Instantiate(sound);
        }
    }
}