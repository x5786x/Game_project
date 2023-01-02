using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Event : MonoBehaviour
{
    public GameObject boss;
    public GameObject generateBoss;
    public Transform movePosition;
    public Transform cameraTransform;
    public GameObject cameraFollow;
    public Animator bossAnim;
    AnimatorStateInfo End;
    public float smooth;
    public float distance;
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
            if(End.normalizedTime >= 0.95 && End.IsName("First"))
            {
                boss.SetActive(true);
                Scoreborad.eventOn = false;
                Destroy(generateBoss);
                Destroy(gameObject.transform.parent.gameObject); // 摧毀父物件
            } 
        }
    }
}
