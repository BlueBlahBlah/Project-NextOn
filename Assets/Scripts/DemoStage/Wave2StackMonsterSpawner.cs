using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave2StackMonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    private float time;
    private float period;
    //몬스???�성 중앙?�어
    public bool Active;
    // Start is called before the first frame update
    void Start()
    {
        Active = true;
        period = Random.Range(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= period && Active)
        {
            period = Random.Range(5, 10);
            time = 0;
            //몬스???�성
            GameObject newMon = Instantiate(Monster, transform.position, Quaternion.identity);
            //배열??추�?
            //MonsterManager.Instance.AddStackMonster_In_Array(newMon);
        }
    }
    
    
}
