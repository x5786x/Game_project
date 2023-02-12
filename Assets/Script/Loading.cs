using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    public GameObject Audio;
    int count = 0;
    private float timer = 0;
    private float waitTime = 3;
    public Text text;
    private AsyncOperation async = null;
    private bool loadingComplete = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadScene");
        Audio = GameObject.FindGameObjectWithTag("Sound");
        Destroy(Audio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(Scoreborad.sceneName);
        async.allowSceneActivation = false;
        while(!async.isDone)
        {
            if(async.progress >= 0.9f && count == 0)
            {        
                loadingComplete = true;
                yield return new WaitForSeconds(1.5f);
                count = 1;
            }
            if(loadingComplete)
            {
                text.color = new Color(128, 124, 124, text.color.a + Time.deltaTime);
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }       
    }
}
