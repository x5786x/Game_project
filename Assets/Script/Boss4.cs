using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : Enemy
{
    public GameObject music;
    float check;
    public float waitTime;
    public GameObject bossHp;
    public float attackDelayTime;
    bool action, bossAttackMode1;
    Quaternion rotation;
    int count, twoLaserRandom;
    int twoLaserCount, twoLaserAngle;
    public GameObject laser;
    public GameObject HolyEnemy1;
    public GameObject HolyEnemy2;
    public GameObject[] sixLaserArray = new GameObject[6];
    int[] sixLaserAngle = new int[6]{-45, -180, -120, 45, 0, 120};
    public Vector3[] sixLaserPosArray = new Vector3[6];
    public GameObject[] twoLaserArray = new GameObject[8];
    public Vector3[] twoLaserPosArray = new Vector3[8];
    Vector3 playerPos;
    public GameObject[] holyEnemyArray = new GameObject[2];
    public Quaternion[] sixLaserRotationArray = new Quaternion[6];
    public GameObject shieldBrokenPos;
    void Start()
    {
        Scoreborad.FinallBossDown = false;
        base.Start();
        bossAttackMode1 = true;
        action = false;
        twoLaserCount = 0;
        for(int i=0; i<6; i++)
        {
            sixLaserPosArray[i] = sixLaserArray[i].transform.position;
            sixLaserRotationArray[i] = sixLaserArray[i].transform.rotation;
            Destroy(sixLaserArray[i]);
        }
        for(int i=0; i<8; i++)
        {
            twoLaserPosArray[i] = twoLaserArray[i].transform.position;
            Destroy(twoLaserArray[i]);
        }
        twoLaserCount = 0;
        rotation = Quaternion.Euler(0, 0, -90);
    }

    
    void Update()
    {
        if(hp <= 0)
        {
            Scoreborad.FinallBossDown = true;
            Destroy(music);
        }
            
        base.Update();
        playerPos = new Vector3(playerTransform.position.x+(playerTransform.localScale.x == 1 ? -1.2f : 1.2f), playerTransform.position.y+0.44f, playerTransform.position.z);
        if(BossShield.bossShieldBroken)
        {
            Scoreborad.bossDown = false;
            GoToBossroom.bossroom = true;
            gameObject.tag = "Boss";
            bossHp.SetActive(true);
            BossShield.bossShieldBroken = false;
            bossAttackMode1 = false;
            count = 0;
            anim.SetBool("Broken", true);
            while(transform.position.y != shieldBrokenPos.transform.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, shieldBrokenPos.transform.position, 0.02f * Time.deltaTime);
            }
            
        }
        timer += Time.deltaTime;
        if(timer >= attackDelayTime)
        {
            action = true;
        }
        if(action)
        {
            if(bossAttackMode1)
            {
                count++;
                if(count == 4)
                {
                    for(int i=0; i<4; i++)
                        TwoLaser();
                    twoLaserCount = 0;
                }
                else if(count < 6)
                {
                    TwoLaserRandom();
                }
                else if(count == 6)
                {
                    SixLaser();
                    count = 0;
                }
                action = false;
                timer = 0;
            }
            else
            {
                attackDelayTime = 4;
                if(count >= 6)
                {
                    for(int i=0; i<5; i++)
                    {
                        waitTime = 1.5f;
                        timer = 0;
                        while(timer < waitTime)
                        {
                            timer += Time.deltaTime;
                        }
                        playerPos = new Vector3(playerTransform.position.x+(playerTransform.localScale.x == 1 ? -1.2f : 1.2f), playerTransform.position.y+0.44f, playerTransform.position.z);
                        GameObject holyEnemy2 = Instantiate(HolyEnemy2, playerPos, Quaternion.identity);
                        holyEnemy2.transform.localScale = new Vector3(playerTransform.localScale.x == 1 ? -1 : 1, 1, 1);
                    }
                    count = 0;
                }
                else if(count%3 == 0)
                {
                    GameObject holyEnemy1 = Instantiate(HolyEnemy1, holyEnemyArray[0].transform.position, Quaternion.identity);
                    holyEnemy1.transform.localScale = new Vector3(-1, 1, 1);
                    Instantiate(HolyEnemy1, holyEnemyArray[1].transform.position, Quaternion.identity);
                }
                else if(count%3 != 0)
                {
                    GameObject holyEnemy2 = Instantiate(HolyEnemy2, playerPos, Quaternion.identity);
                    holyEnemy2.transform.localScale = new Vector3(playerTransform.localScale.x == 1 ? -1 : 1, 1, 1);
                }
                count++;
                action = false;
                timer = 0;
            } 
        }
    }

    void SixLaser()
    {
        for(int i=0; i<6; i++)
            Instantiate(laser, sixLaserPosArray[i],  sixLaserRotationArray[i]);
    }
    void TwoLaser()
    {
        int limit = twoLaserCount+2;
        for(; twoLaserCount<limit; twoLaserCount++)
        {
            if(twoLaserCount < 6)
            {
                if(limit-twoLaserCount == 2)
                    Instantiate(laser, twoLaserPosArray[twoLaserCount],  Quaternion.Euler(0, 0, 180));
                else
                    Instantiate(laser, twoLaserPosArray[twoLaserCount],  Quaternion.Euler(0, 0, 0));
            }
            else  
                Instantiate(laser, twoLaserPosArray[twoLaserCount],  Quaternion.Euler(0, 0, -90));
        }
    }
    void TwoLaserRandom()
    {
        int original;
        twoLaserRandom = Random.Range(0, 8);
        original = twoLaserRandom;
        Check();
        Instantiate(laser, twoLaserPosArray[twoLaserRandom],  Quaternion.Euler(0, 0, twoLaserAngle));
        while(original == twoLaserRandom)
            twoLaserRandom = Random.Range(0, 8);
        Check();
        Instantiate(laser, twoLaserPosArray[twoLaserRandom],  Quaternion.Euler(0, 0, twoLaserAngle));
    }

    void Check()
    {
        
        if(twoLaserRandom < 6)
        {
            if(((twoLaserRandom+1)%2) == 0)
                twoLaserAngle = 0;        
            else
                twoLaserAngle = 180;        
        }
            
        else
            twoLaserAngle = -90;
    }
    IEnumerator ShelidBroken()
    {
        yield return new WaitForSeconds(3);
    }
}
