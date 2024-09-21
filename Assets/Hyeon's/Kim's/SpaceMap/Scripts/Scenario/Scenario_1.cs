using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scenario_1 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] bool is1_TriggerPass, is_End;
    public GameObject[] WeaponSpawn;
    public GameObject firstTrigger;
    public GameObject secondTrigger;

    [Header("Player Respawn")]
    public Transform Respawn;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        is1_TriggerPass = false;
        is_End = false;
        scenario = FindObjectOfType<Scenario>();
        for (int i = 0; i < WeaponSpawn.Length; i++)
        {
            WeaponSpawn[i].gameObject.SetActive(true);
        }
    }

    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        if(child.name == "1_Trigger" && !is1_TriggerPass)
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(),95);
            Debug.Log("��� ��������? �ֺ��� �ѷ�����");
            is1_TriggerPass=true;
        }
        if(child.name == "LightRayRound" && !is_End)
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 96);
            Debug.Log("��� �����Ϸ�... ����� �ʿ�...\r\n���� ���� ���α׷�... �۵� �ʿ�...\r\n\r\n�̰� ���� �ؾ��ϴ� �ϵ��ΰ�?\r\n�ϴ� �����Ⱥκ��� ���°� ���� �غ���");
            is_End=true;
        }
        if(child.name == "Prob")
        {
            scenario.UIManager.DialogueEventByNumber(scenario.Dialogue.GetComponent<Dialogue>(), 99);
            Debug.Log("��� ���⸦ �߰��ߴ�\r\n�̰� �� ���� �ִ����� �𸣰�����, ���°ź��ٴ�...");
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
            Scenario.instance.playing_Scenario = 2;
            Scenario.instance.StartCoroutine("Scenario2Start");
        }
    }
}
