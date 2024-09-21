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
        FreeLookCam = FreeLookCam.GetComponent<Camera>();
        TopCam = TopCam.GetComponent<Camera>();
        SpecialCam = SpecialCam.GetComponent<Camera>();
        isTopview = true;
        SpecialView = false;
    }
    void Update()
    {
        if (isTopview)
        {
            TopCam.enabled = true;
            FreeLookCam.enabled = false;
            SpecialCam.enabled = false;
        }
        else
        {
            if (SpecialView)
            {
                SpecialCam.enabled = true;
                SpecialCam.gameObject.transform.position = new Vector3(Player.transform.position.x + offsetX, SpecialCam.transform.position.y, offsetZ);
                TopCam.enabled = false;
                FreeLookCam.enabled = false;
            }
            else
            {
                SpecialCam.enabled = false;
                TopCam.enabled = false;
                FreeLookCam.enabled = true;
                transform.LookAt(Player.transform);
            }
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
