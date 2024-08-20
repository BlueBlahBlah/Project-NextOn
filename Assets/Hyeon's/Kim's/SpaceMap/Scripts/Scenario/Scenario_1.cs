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
            Debug.Log("대사 여긴어디지? 주변을 둘러보자");
            is1_TriggerPass=true;
        }
        if(child.name == "LightRayRound" && !is_End)
        {
            Debug.Log("대사 컴파일러... 재시작 필요...\r\n누수 방지 프로그램... 작동 필요...\r\n\r\n이게 내가 해야하는 일들인가?\r\n일단 누수된부분을 막는거 부터 해보자");
            is_End=true;
        }
        if(child.name == "Prob")
        {
            Debug.Log("대사 도끼를 발견했다\r\n이게 왜 여기 있는지는 모르겠지만, 없는거보다는...");
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
