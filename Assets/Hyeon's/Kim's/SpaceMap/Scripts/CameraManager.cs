using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public Transform[] targetPos;   //ī�޶��� ��ġ���� ��� �迭
    [SerializeField] private Camera FreeLookCam;
    [SerializeField] private Camera TopCam;
    [SerializeField] private Camera SpecialCam;

    public int currentRoomIndex = -1;  //���� ���� �ε���
    public bool isTopview;
    public bool SpecialView;

    private float offsetX = 0f; // ī�޶��� x�� ������
    private float offsetZ = -28f; // ī�޶��� z�� ������

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
        if (SpecialView)    //�ó����� 3���� ���� 3�� ���� 3�� ����
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
