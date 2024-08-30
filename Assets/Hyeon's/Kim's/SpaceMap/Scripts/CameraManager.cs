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
                                  // �̱��� ����
    #region
    public static CameraManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }
    #endregion
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
