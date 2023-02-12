using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Portal : MonoBehaviour
{
    static public bool inPortal = false;
    public GameObject Effect;
    Vector3 effectPostion;
    public Image panelStatus;
    public GameObject panel;
    public BoxCollider2D portalCollider;
    void Start()
    {
        effectPostion = new Vector3(transform.position.x, transform.position.y, -5);
        Instantiate(Effect, effectPostion, new Quaternion(0 ,0 ,0 ,0));
        portalCollider = GetComponent<BoxCollider2D>();
        panelStatus = GetComponent<Image>();
    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Wait");
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        panel.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        inPortal = true;
        if(Scoreborad.FinallBossDown)
            SceneManager.LoadScene("End");
        else
            SceneManager.LoadScene("Level menu");
    }
}
