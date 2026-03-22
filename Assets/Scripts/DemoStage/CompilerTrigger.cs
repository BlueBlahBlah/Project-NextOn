using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompilerTrigger : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject eventBtn;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //player와 발전기 사이의 거리가 4이하일때
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 4f)
           // && GameObject.Find("StageManager").GetComponent<StageManager>().Wave2MonsterClear)    //스택 몬스터를 다 잡아야만 활설화 할때
        {
            eventBtn.SetActive(true);
        }
        else
        {
            eventBtn.SetActive(false);
        }
    }
}
