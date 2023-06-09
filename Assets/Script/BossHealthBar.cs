using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    public Text healthText;
    public static int BossHealthMax;
    public GameObject BossHpBar;
    public static int BossHealthCurrent;
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        if(!Scoreborad.bossDown && GoToBossroom.bossroom)
        {
            healthBar.fillAmount = (float)BossHealthCurrent / (float)BossHealthMax;
            healthText.text = BossHealthCurrent.ToString() + "/" + BossHealthMax.ToString();
        }
        else
        {
            BossHpBar.SetActive(false);
        } 
    }
}
