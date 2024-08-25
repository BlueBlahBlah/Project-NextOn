using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_3 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] bool _doorClose1, _doorClose2,is1_TriggerPass, is2_TriggerPass, is_End;
    public GameObject[] EnemySpawn;

    [Header("Door Trigger")]
    public GameObject[] firstTrigger;
    public GameObject[] secondTrigger;
    [Header("End Point")]
    public GameObject thirdTrigger;

    public GameObject Player;

    [Header("Smoke")]
    public GameObject[] Smoke;

    void Start()
    {
        _doorClose1 = false;
        _doorClose2 = false;
        is1_TriggerPass = false;
        is2_TriggerPass = false;
        is_End = false;


        for(int i=0;i < 2; i++)
        {
            firstTrigger[i].GetComponent<CloseDoor2>().enabled = false;
            secondTrigger[i].GetComponent<CloseDoor2>().enabled = false;
        }


        for(int i= 0; i < Smoke.Length; i++)
        {
            Smoke[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.y > 2f)
        {
            cameraManager.SpecialView = true;
            if (Player.transform.position.z < -34f)
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

        if(is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine("Scenario4Start");
        }
    }
    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if(child.name == "First_Trigger" && !is1_TriggerPass)
        {
            Debug.Log("��� �ý��� : �˼� ���� ���� �߻� Ȯ�� �����Ϸ� �ڵ� ������ �����մϴ� ���Ż�� ���α׷��� �����մϴ�");
            Debug.Log("��� ���Ż��? �켱 ��ƾ߰ھ� �ִ��� �Ʒ��� ����ġ��");
            Smoke[0].SetActive(true);
            Smoke[1].SetActive(true);
            is1_TriggerPass = true;
        }
        else if(child.name == "Second_Trigger" && !is2_TriggerPass)
        {
            Smoke[2].SetActive(true);
            Smoke[3].SetActive(true);
            is2_TriggerPass = true;
        }
        else if(child.name == "1_Door"  && !_doorClose1)
        {
            firstTrigger[0].GetComponent<CloseDoor2>().enabled = true;
            firstTrigger[1].GetComponent<CloseDoor2>().enabled = true;
            _doorClose1 = true;
        }
        else if(child.name == "2_Door" && !_doorClose2)
        {
            Debug.Log("��� ���� ���� �����Ѱ� ���� �Ʊ���� ���Ż������ ã�ƺ���");
            secondTrigger[0].GetComponent<CloseDoor2>().enabled = true;
            secondTrigger[1].GetComponent<CloseDoor2>().enabled = true;
            _doorClose2 = true;
        }
        else if (child.name == "3_End" && !is_End)
        {
            Debug.Log("��� ���� �ִ� ���༱�� Ÿ�� Ż���Ҽ� �ְھ� � �غ��ϰ� Ż������");
            is_End = true;
        }
    }
}
