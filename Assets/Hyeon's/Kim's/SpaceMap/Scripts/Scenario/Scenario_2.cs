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
        {//둘다 작동된 상태
            cmd1IsPass = true;
            Debug.Log("대사 여기를 막는건가");
        }
        else if ((is1_TriggerPass && is2_TriggerPass) && !cmd2IsPass)
        {//둘중 하나만 작동된 상태
            cmd2IsPass = true;
            Debug.Log("대사 이제 재시작을 하러 가보자");
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
            Debug.Log("대사 여기서 재시작하는건가? 작동 시켜보자 \r\n\r\n시스템 : 방화벽 프로그램 작동... 재시작 준비완료");
            is_End = true;
        }
    }
}
