using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_1 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] bool is1_TriggerPass, is_End;
    public GameObject[] WeaponSpawn;
    public GameObject firstTrigger;
    public GameObject secondTrigger;


    // Start is called before the first frame update
    void Start()
    {
        is1_TriggerPass = false;
        is_End = false;
        scenario = FindObjectOfType<Scenario>();

    }

    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if(child.name == "1_Trigger" && !is1_TriggerPass)
        {
            Debug.Log("��� ��������? �ֺ��� �ѷ�����");
            is1_TriggerPass=true;
        }
        if(child.name == "LightRayRound" && !is_End)
        {
            Debug.Log("��� �����Ϸ�... ����� �ʿ�...\r\n���� ���� ���α׷�... �۵� �ʿ�...\r\n\r\n�̰� ���� �ؾ��ϴ� �ϵ��ΰ�?\r\n�ϴ� �����Ⱥκ��� ���°� ���� �غ���");
            is_End=true;
        }
        if(child.name == "Prob")
        {
            Debug.Log("��� ������ �߰��ߴ�\r\n�̰� �� ���� �ִ����� �𸣰�����, ���°ź��ٴ�...");
            for(int i=0;i < WeaponSpawn.Length; i++)
            {
                WeaponSpawn[i].gameObject.SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(is1_TriggerPass && is_End)
        {
            scenario.StartCoroutine("Scenario2Start");
        }
    }
}
