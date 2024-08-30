using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Scenario_4 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject Gague;
    [SerializeField] bool is1_TriggerPass, is2_TriggerPass, is_End;
    public GameObject[] EnemySpawn;

    [Header("Door Trigger")]
    public GameObject firstTrigger;

    [Header("End Point")]
    public GameObject secondTrigger;
    public GameObject Open_Door1;
    public GameObject Open_Door2;
    public GameObject Plane;
    public GameObject AirPlane;
    
    
    private float duration = 120f;
    private float Door_Distance = 10;
    private float Air_Distance = 3;
    private float spawnInterval = 20;

    public GameObject Player;
    
    
    void Start()
    {
        cameraManager.SpecialView = false;
        Gague.SetActive(false);
    }
    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if (child.name == firstTrigger.name && !is1_TriggerPass)
        {
            firstTrigger.GetComponent<CloseDoor2>().enabled = true;
            Debug.Log("��� ���� �����ִ� ���༱���� �̵��غ���");
            is1_TriggerPass = true;

        }
        else if(child.name ==secondTrigger.name && !is2_TriggerPass)
        {
            Debug.Log("�� �������� ��� ���°���? �ϴ� ���� ���������� ���ߺ���");
            StartCoroutine("StartLastGame");
            StartCoroutine("StartLastSpawn");
            is2_TriggerPass = true;
        }
    }

    IEnumerator StartLastSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            //Instantiate(objectCPrefab, Vector3.zero, Quaternion.identity); // C ������Ʈ ����
        }
        yield return null;
    }

    IEnumerator StartLastGame()
    {
        Vector3 startPositionA = Open_Door1.transform.position;
        Vector3 startPositionB = Open_Door2.transform.position;

        Vector3 endPositionA = startPositionA + Vector3.right * Door_Distance;
        Vector3 endPositionB = startPositionB + Vector3.left * Door_Distance;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            Open_Door1.transform.position = Vector3.Lerp(startPositionA, endPositionA, t);
            Open_Door2.transform.position = Vector3.Lerp(startPositionB, endPositionB, t);

            yield return null;
        }

        Open_Door1.transform.position = endPositionA;
        Open_Door2.transform.position = endPositionB;
    }
        // Update is called once per frame
        void Update()
    {
        /*
        if (Player.transform.position.y > 5f)
        {
            cameraManager.SpecialView = true;
            if (Player.transform.position.z < -35f)
            {
                cameraManager.isTopview = false;
            }
            else
            {
                cameraManager.isTopview = true;
            }
        }
        else
        {
            cameraManager.SpecialView = false;
        }
        */
        if (is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine("END");
        }
    }
}
