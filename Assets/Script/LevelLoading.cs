using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelLoading : MonoBehaviour
{
    static public bool levelLoadingComplete = false;
    public Image panel;
    public GameObject loadingPanel;
    void Awake()
    {
        levelLoadingComplete = false;
        panel = loadingPanel.GetComponent<Image>(); 
        loadingPanel.SetActive(true);
    }
    void Update()
    {
        Debug.Log(panel.color.a);
        if(panel.color.a <= 0)
        {
            loadingPanel.SetActive(false);
        }
    }
}
