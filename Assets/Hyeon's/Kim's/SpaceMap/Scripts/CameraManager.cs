using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Player;
    public Transform[] targetPos;   //ī�޶��� ��ġ���� ��� �迭

    public int currentRoomIndex = -1;  //���� ���� �ε���
    
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
