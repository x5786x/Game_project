using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject buttonClickAudio;
    public AudioClip clip;
    private static bool GamePasue = false;
    public GameObject PasueMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePasue)
                Resume();
            else
                Pasue();
        }
            
    }

    public void Pasue()
    {
        PasueMenuUI.SetActive(true); // 顯示UI
        Time.timeScale = 0.0f;
        GamePasue = true;
    }
    public void Resume()
    {
        PasueMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePasue = false;
    }

    public void MainMenu()
    {
        GamePasue = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ButtonClickSound()
    { 
        Instantiate(buttonClickAudio);
    }
    void WaitTime()
    {
        float waitTime = clip.length;
        float timer = 0;
        while(timer <= waitTime)
        {
            timer += Time.deltaTime;
        }
    }
}
