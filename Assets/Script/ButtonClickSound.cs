using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    AudioSource buttonClickAudio;
    void Start()
    {
        buttonClickAudio = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!buttonClickAudio.isPlaying)
            Destroy(gameObject);
    }
}
