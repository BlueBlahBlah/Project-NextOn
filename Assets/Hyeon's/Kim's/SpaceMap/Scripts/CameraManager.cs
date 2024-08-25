using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public Transform[] targetPos;   //카메라의 위치들을 담는 배열
    [SerializeField] private Camera FreeLookCam;
    [SerializeField] private Camera TopCam;
    [SerializeField] private Camera SpecialCam;

    public int currentRoomIndex = -1;  //현재 방의 인덱스
    public bool isTopview;
    public bool SpecialView;

    private float offsetX = 0f; // 카메라의 x축 오프셋
    private float offsetZ = -28f; // 카메라의 z축 오프셋

    void Start()
    {
        FreeLookCam = GetComponent<Camera>();
        TopCam = GetComponent<Camera>();
        SpecialCam = GetComponent<Camera>();
        isTopview = true;
        SpecialView = false;
    }
    void Update()
    {
        if (SpecialView)    //시나리오 3만을 위한 3에 의한 3을 위해
        {
            transform.position = new Vector3(Player.transform.position.x + offsetX, transform.position.y, offsetZ);
            if (isTopview)
            {
                TopCam.enabled = false;
                FreeLookCam.enabled = true;
                transform.LookAt(Player.transform);
            }
        }
        else
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

    }

    public void ChaingePointOfView() 
    {
        if (!SpecialView)
        {
            isTopview = !isTopview;
        }
    }
    public void SetTarget(int idx)
    {
        this.transform.position = targetPos[idx].position;
        this.transform.rotation = targetPos[idx].rotation;

        currentRoomIndex = idx;
    }
}
