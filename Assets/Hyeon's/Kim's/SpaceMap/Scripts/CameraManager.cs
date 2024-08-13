using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Player;
    public Transform[] targetPos;   //카메라의 위치들을 담는 배열
    public Camera FreeLookCam;
    public Camera TopCam;

    public int currentRoomIndex = -1;  //현재 방의 인덱스
    public bool isTopview;
    void Start()
    {
        FreeLookCam = GetComponent<Camera>();
        TopCam = GetComponent<Camera>();
        isTopview = true;
    }
    void Update()
    {
        if (isTopview)
        {
            TopCam.enabled = true;
            FreeLookCam.enabled = false;
        }
        else
        {
            TopCam.enabled = false;
            FreeLookCam.enabled = true;
            transform.LookAt(Player.transform);
        }
    }

    public void ChaingePointOfView() => isTopview = !isTopview;
    public void SetTarget(int idx)
    {
        this.transform.position = targetPos[idx].position;
        this.transform.rotation = targetPos[idx].rotation;

        currentRoomIndex = idx;
    }
}
