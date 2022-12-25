using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMirror :  MonoBehaviour
{
    public BoxCollider2D mirror;
    static public bool getmirror = false;
    public GameObject mirror1;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        getmirror = false;
        mirror = GameObject.FindGameObjectWithTag("Mirror").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss1.boss1down)
        {
            Instantiate(mirror1, new Vector3(57.49f, -3.87f, 0f), new Quaternion(0,0,0,0));
            Boss1.boss1down = false;
        }
        if(getmirror && Goodbigsmile.end)
        {
            portal.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Mirror")
        {
            Destroy(GameObject.FindGameObjectWithTag("Mirror"));
            getmirror = true;
            
            Scoreborad.score += 1;
        }
            
    }
}
 
