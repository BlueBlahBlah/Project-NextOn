using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Player;
    public Transform[] targetPos;   //카메라의 위치들을 담는 배열

    public int currentRoomIndex = -1;  //현재 방의 인덱스
    
    void Update()
    {
        transform.LookAt(Player.transform);
    }


    public void SetTarget(int idx)
    {
        this.transform.position = targetPos[idx].position;
        this.transform.rotation = targetPos[idx].rotation;
        //Camera.main.transform.position = targetPos[idx].position;
        //Camera.main.transform.rotation = targetPos[idx].rotation;
        currentRoomIndex = idx;
    }
}
