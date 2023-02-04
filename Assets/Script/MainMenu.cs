using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject level_2, level_3, level_4,level_2_lock,level_3_lock,level_4_lock;
    static public string sceneName;
    void Start() 
    {
        sceneName = "";
        Screen.SetResolution(1920, 1080, true);  
    }
    void Update()
    {
        
            level_2.gameObject.SetActive(false);
            level_3.gameObject.SetActive(false);
            level_4.gameObject.SetActive(false);
            level_2_lock.gameObject.SetActive(true);
            level_3_lock.gameObject.SetActive(true);
            level_4_lock.gameObject.SetActive(true);    
            if(Scoreborad.score>=2)
            {
                level_2_lock.gameObject.SetActive(false);
                level_2.gameObject.SetActive(true);
            }  
            if(Scoreborad.score>=3)
            {
                level_3_lock.gameObject.SetActive(false);
                level_3.gameObject.SetActive(true);
            }
            if(Scoreborad.score>=4)
            {
                level_4_lock.gameObject.SetActive(false);
                level_4.gameObject.SetActive(true);
        }
        
    }
    public void Playgame()
    {
        SceneManager.LoadScene("Level menu");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void Back()
    {   
        SceneManager.LoadScene("MainMenu");
    }
    public void Level_1()
    {
        sceneName = "Level1";
        Scoreborad.level = 1;
        SceneManager.LoadScene("Loading");
    }
    public void Level_2()
    {
        sceneName = "Level2";
        Scoreborad.level = 2;
        SceneManager.LoadScene("Loading");
    }  
    public void Level_3()
    {
        sceneName = "Level3";
        Scoreborad.level = 3;
        SceneManager.LoadScene("Loading");
    }
    public void Level_4()
    {
        sceneName = "Level4";
        Scoreborad.level = 4;
        SceneManager.LoadScene("Loading");
    }  
}
