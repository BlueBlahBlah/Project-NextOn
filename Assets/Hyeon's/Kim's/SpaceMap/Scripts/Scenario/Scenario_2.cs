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
            Debug.Log("��� ���⸦ ���°ǰ�");
        }
        else if ((is1_TriggerPass && is2_TriggerPass) && !cmd2IsPass)
        {//���� �ϳ��� �۵��� ����
            cmd2IsPass = true;
            Debug.Log("��� ���� ������� �Ϸ� ������");
        }
        


        if(is1_TriggerPass && is2_TriggerPass && is_End)
        {
            scenario.StartCoroutine("Scenario3Start");
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
