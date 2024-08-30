using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_2 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] bool is1_TriggerPass, is2_TriggerPass, is_End;
    private bool cmd1IsPass, cmd2IsPass;
    public GameObject[] EnemySpawn;
    [Header("Door Trigger")]
    public GameObject firstTrigger;
    public GameObject secondTrigger;
    [Header("End Point")]
    public GameObject thirdTrigger;


    void Start()
    {
        is1_TriggerPass = false;
        is2_TriggerPass = false;
        is_End = false;
        cmd1IsPass = false;
        cmd2IsPass = false;

        firstTrigger.GetComponent<CloseDoor>().enabled = false;
        secondTrigger.GetComponent<CloseDoor>().enabled = false;

        for(int i = 3; i< EnemySpawn.Length; i++)
        {
            EnemySpawn[i].SetActive(false);
        }
        EnemySpawn[0].SetActive(true);
        EnemySpawn[1].SetActive(true);
        EnemySpawn[2].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTrigger.GetComponent<CloseDoor>().isActiveAndEnabled && !is1_TriggerPass)
        {
            is1_TriggerPass = true;
        }
        if(secondTrigger.GetComponent<CloseDoor>().isActiveAndEnabled && !is2_TriggerPass)
        {
            is2_TriggerPass= true;
        }

        if ((is1_TriggerPass || is2_TriggerPass) && !cmd1IsPass)
        {//�Ѵ� �۵��� ����
            cmd1IsPass = true;
            EnemySpawn[3].SetActive(true);
            EnemySpawn[4].SetActive(true);
            EnemySpawn[5].SetActive(true);
            Debug.Log("��� ���⸦ ���°ǰ�");
        }
        else if ((is1_TriggerPass && is2_TriggerPass) && !cmd2IsPass)
        {//���� �ϳ��� �۵��� ����
            cmd2IsPass = true;
            EnemySpawn[6].SetActive(true);
            EnemySpawn[7].SetActive(true);
            EnemySpawn[8].SetActive(true);
            Debug.Log("��� ���� ������� �Ϸ� ������");
        }
        


        if(is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine(scenario.Scenario3Start());
        }
    }

    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if (child.name == "2_End" && !is_End)
        {
            Debug.Log("��� ���⼭ ������ϴ°ǰ�? �۵� ���Ѻ��� \r\n\r\n�ý��� : ��ȭ�� ���α׷� �۵�... ����� �غ�Ϸ�");
            is_End = true;
        }
    }
}
