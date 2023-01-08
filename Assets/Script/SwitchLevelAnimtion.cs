using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchLevelAnimtion : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float speed;
    public Image panel;
    private Color color;
    private float alpha;
    private bool level = false;
    private bool levelMenu = false;
    public GameObject loadingPanel;
    // Start is called before the first frame update
    void Start()
    {

        panel = GetComponent<Image>();
        color = new Color(0, 0, 0, 0);
        alpha = panel.color.a;
        if(alpha == 1)
            level = true;
        else 
            levelMenu = true;
    }

    // Update is called once per frame
    void Update()
    {  
        if(level)
        {
            if(alpha == 0)
            {

                loadingPanel.SetActive(false);
            }
            else
            {
                panel.color = new Color(0, 0, 0, alpha -= speed*Time.deltaTime);
            }
        }
        if(levelMenu)
        {
            panel.color = new Color(0, 0, 0, alpha += speed*Time.deltaTime);
        }
    }
}
