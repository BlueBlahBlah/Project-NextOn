using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave2StackMonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    private float time;
    private float period;
    private StageManager StageManager;
    //몬스터 생성 중앙제어
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        period = Random.Range(5, 10);
        StageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= period && Active)
        {
            period = Random.Range(5, 10);
            time = 0;
            //몬스터 생성
            GameObject newMon = Instantiate(Monster, transform.position, Quaternion.identity);
            //배열에 추가
            StageManager.AddStackMonster_In_Array(newMon);
        }
    }
}
