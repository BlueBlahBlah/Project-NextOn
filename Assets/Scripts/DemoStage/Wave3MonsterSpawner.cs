using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave3MonsterSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> Monsters;
    private float time;
    private float period;
    private int index;
    //몬스터 생성 중앙제어
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        period = Random.Range(2, 5);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= period && Active)
        {
            period = Random.Range(10, 15);
            index = Random.Range(0, Monsters.Count);
            time = 0;
            //몬스터 생성
            GameObject newMon = Instantiate(Monsters[index], transform.position, Quaternion.identity);
            //배열에 추가
            MonsterManager.Instance.AddStackMonster_In_Array(newMon);
        }
    }
}
