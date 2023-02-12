using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    bool check = true;
    public GameObject buttonClickAudio;
    public AudioClip clip;
    public GameObject level_2, level_3, level_4,level_2_lock,level_3_lock,level_4_lock;
    
    void Start() 
    {
        Goodbigsmile.end = false;
        Scoreborad.bossDown = false;
        GoToBossroom.bossroom = false;
        if(Scoreborad.switchlevel)
        {     
            Instantiate(buttonClickAudio);
            Scoreborad.switchlevel = false;
        }
        Screen.SetResolution(1920, 1080, true);  
        level_2.gameObject.SetActive(false);
        level_3.gameObject.SetActive(false);
        level_4.gameObject.SetActive(false);
    }
    void Update()
    {  
        
        if(check)
        {
            if(Scoreborad.score>=2)
            {
                level_2.gameObject.SetActive(true);
            }  
            if(Scoreborad.score>=3)
            {
                level_3.gameObject.SetActive(true);
            }
            if(Scoreborad.score>=4)
            {
                level_4.gameObject.SetActive(true);
            }
            check = false;
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
        Scoreborad.sceneName = "Level1";
        Scoreborad.level = 1;
        SceneManager.LoadScene("Loading");
    }
    public void Level_2()
    {

        Scoreborad.sceneName = "Level2";
        Scoreborad.level = 2;
        SceneManager.LoadScene("Loading");
    }  
    public void Level_3()
    {
        Scoreborad.sceneName = "Level3";
        Scoreborad.level = 3;
        SceneManager.LoadScene("Loading");
    }
    public void Level_4()
    {
        Scoreborad.sceneName = "Level4";
        Scoreborad.level = 4;
        SceneManager.LoadScene("Loading");
    }  
    public void ButtonClickSound()
    {
        Instantiate(buttonClickAudio);
    }

}
