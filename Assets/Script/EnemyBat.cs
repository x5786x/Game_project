using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    
    public float trackDistance;
    public float distance;
    public float speed;
    public float startWaitTime;
    public float waitTime;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public Transform movePos;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        distance = (transform.position - playerTransform.position).sqrMagnitude;
        if(distance <= trackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            waitTime = 0;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);
        }
        
        if(Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if(waitTime <= 0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 randomPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return randomPos;
    }
}
