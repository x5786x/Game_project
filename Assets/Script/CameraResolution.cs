using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    float defultWidth = 1920;
    float defultHeight = 1080;
    float defultOrthographicSize = 5;
    void Awake() 
    {
        float newOrthographicSize = (float)Screen.height / (float)Screen.width * defultWidth / defultHeight * defultOrthographicSize;
        Camera.main.orthographicSize = Mathf.Max(newOrthographicSize , defultOrthographicSize);
    }
}
