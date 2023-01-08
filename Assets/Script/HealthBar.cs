using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public static int HealthCurrent;
    public static int HealthMax;    
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

 
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
    
    public void Replay()
    {
        Time.timeScale = 1f;
        Scoreborad.restart = true;
        GoToBossroom.bossroom = false;
        SceneManager.LoadScene(MainMenu.sceneName);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
