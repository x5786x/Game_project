using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Event : MonoBehaviour
{
    public GameObject bossHpBar;
    public GameObject boss;
    public GameObject generateBoss;
    public Transform movePosition;
    public Transform cameraTransform;
    public GameObject cameraFollow;
    public Animator bossAnim;
    AnimatorStateInfo End;
    public float smooth;
    float distance;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow.GetComponent<CameraFollow>().enabled = false;
        cameraTransform = cameraFollow.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, movePosition.position, smooth * Time.deltaTime); // A到B點
        distance = (cameraTransform.position - movePosition.position).magnitude;
        if(distance <= 0.3f)
        {   
            bossAnim.SetBool("Generate", true);
            End = bossAnim.GetCurrentAnimatorStateInfo(0);
            if(End.normalizedTime >= 0.99 && End.IsName("First"))
            {
                timer += Time.deltaTime;
                if(timer >= 1.5f)
                {
                    boss.SetActive(true);
                    Scoreborad.eventOn = false;
                    cameraFollow.GetComponent<CameraFollow>().enabled = true;
                    bossHpBar.SetActive(true);
                    Destroy(generateBoss);
                    Destroy(gameObject.transform.parent.gameObject); // 摧毀父物件
                }
            } 
        }
    }
}
