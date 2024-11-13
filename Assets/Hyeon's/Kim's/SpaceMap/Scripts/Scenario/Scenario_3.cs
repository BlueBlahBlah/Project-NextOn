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

    [Header("Player Respawn")]
    public Transform Respawn;

    [Header("Smoke")]
    public GameObject[] Smoke;


    private bool isSideScrollingCamActivated = false;
    void Start()
    {
        _doorClose1 = false;
        _doorClose2 = false;
        is1_TriggerPass = false;
        is2_TriggerPass = false;
        is_End = false;
        
        scenario.MidBGM.mute = true;
        scenario.FinalBGM.mute = true;

        for(int i=0;i < 2; i++)
        {
            firstTrigger[i].GetComponent<CloseDoor2>().enabled = false;
            secondTrigger[i].GetComponent<CloseDoor2>().enabled = false;
        }


        for(int i= 0; i < Smoke.Length; i++)
        {
            Smoke[i].SetActive(false);
        }

        for(int i=0; i < EnemySpawn.Length; i++)
        {
            EnemySpawn[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (is1_TriggerPass && is2_TriggerPass && is_End)
        {
            cameraManager.SpecialView = false;
            Scenario.instance.playing_Scenario = 4;
            Scenario.instance.StartCoroutine(scenario.Scenario4Start());
        }

        if (!isSideScrollingCamActivated && scenario.Player.transform.position.y > 5f)
        {
            SideScrollingCam();
            isSideScrollingCamActivated = true; // �Լ� ȣ�� �� �÷��׸� true�� ����
        }

    }

    void SideScrollingCam()
    {
        cameraManager.SpecialView = true;
        EnemySpawn[5].SetActive(true);
        EnemySpawn[6].SetActive(true);
        EnemySpawn[7].SetActive(true);
    }
    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if(child.name == "First_Trigger" && other.tag == "Player" && !is1_TriggerPass)
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 218);
            Debug.Log("��� �ý��� : �˼� ���� ���� �߻� Ȯ�� �����Ϸ� �ڵ� ������ �����մϴ� ���Ż�� ���α׷��� �����մϴ�");
            Debug.Log("��� ���Ż��? �켱 ��ƾ߰ھ� �ִ��� �Ʒ��� ����ġ��");
            Smoke[0].SetActive(true);
            Smoke[1].SetActive(true);
            EnemySpawn[0].SetActive(true);
            is1_TriggerPass = true;
            scenario.BGM.mute = true;
            scenario.MidBGM.mute = false;
        }
        else if(child.name == "Second_Trigger" && other.tag == "Player" && !is2_TriggerPass)
        {
            Smoke[2].SetActive(true);
            Smoke[3].SetActive(true);
            EnemySpawn[1].SetActive(true);
            EnemySpawn[2].SetActive(true);
            EnemySpawn[3].SetActive(true);
            is2_TriggerPass = true;
        }
        else if((child.name == "1_Door"  && !_doorClose1) && other.tag == "Player" && other.tag == "Player")
        {
            firstTrigger[0].GetComponent<CloseDoor2>().enabled = true;
            firstTrigger[1].GetComponent<CloseDoor2>().enabled = true;
            EnemySpawn[4].SetActive(true);
            _doorClose1 = true;
        }
        else if(child.name == "2_Door" && other.tag == "Player" && !_doorClose2)
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 221);
            Debug.Log("��� ���� ���� �����Ѱ� ���� �Ʊ���� ���Ż������ ã�ƺ���");
            EnemySpawn[0].SetActive(false);
            EnemySpawn[1].SetActive(false);
            EnemySpawn[2].SetActive(false);
            EnemySpawn[3].SetActive(false);
            EnemySpawn[4].SetActive(false);
            secondTrigger[0].GetComponent<CloseDoor2>().enabled = true;
            secondTrigger[1].GetComponent<CloseDoor2>().enabled = true;
            _doorClose2 = true;
            scenario.BGM.mute = false;
            scenario.MidBGM.mute = true;
        }
        else if (child.name == "3_End" && other.tag == "Player" && !is_End)
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 231);
            Debug.Log("��� ���� �ִ� ���༱�� Ÿ�� Ż���Ҽ� �ְھ� � �غ��ϰ� Ż������");
            is_End = true;
        }
    }
}
