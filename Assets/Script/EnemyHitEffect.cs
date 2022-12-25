using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    public float destoryTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
