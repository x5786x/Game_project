using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool free = false;
    public GameObject firstSceneMin;
    public GameObject firstSceneMax;
    public GameObject bossSceneMin;
    public GameObject bossSceneMax;
    private Vector3 CameraPos;
    public Transform Player;
    private Vector2 maxPosition;
    private Vector2 minPosition;
    public float speed = 2f;
    void Start()
    {
        CameraPos = Camera.main.transform.position;
        if(!free)
            SetCamPosLimit(firstSceneMin.transform.position, firstSceneMax.transform.position);
    }
    void LateUpdate() 
    {
        Debug.Log(minPosition);
        if(Player != null)
        {
            if(transform.position != Player.position)
            {
                if(GoToBossroom.bossroom)
                {
                    ChangeToBossRoom();
                }
                if(Scoreborad.restart)
                {
                    SetCamPosLimit(firstSceneMin.transform.position, firstSceneMax.transform.position);
                    Scoreborad.restart = false;
                }
                Vector3 playerPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
                if(!free)
                {
                    playerPos.x = Mathf.Clamp(playerPos.x, minPosition.x, maxPosition.x);
                    playerPos.y = Mathf.Clamp(playerPos.y, minPosition.y, maxPosition.y);   
                }    
                transform.position = playerPos;
                Vector3 CamPos = new Vector2 (playerPos.x, playerPos.y);
            }
        }
    }
    public void ChangeToBossRoom()
    {
        SetCamPosLimit(bossSceneMin.transform.position, bossSceneMax.transform.position);
    }
    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
    
}
