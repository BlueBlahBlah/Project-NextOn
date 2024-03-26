using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class Overflow : MonoBehaviour
{
    public static Overflow instance;
    public static int CountEnemy = 8;

    public static bool isclear;
    public static bool isrepeat;
    bool isclose;
    bool ready;

    void Start()
    {
        isclose = false;
        ready = true;
        isclear = false;
        isrepeat = true;
    }
    

    void Update()
    {
        if(ready)
        {
            EnemySpawn.instance.CreateNEnemy(8);
            ready = false;
        }
        CountEnemy = (int)GameObject.FindGameObjectsWithTag("Enemy").Length-1;

        if(CountEnemy == -1 && isrepeat) //오브젝트 상호작용없이 무조건 실패하는 상황
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject found in temp)
                Destroy(found);
            ready = true;
        }
        if(CountEnemy == 0){
            if(isclose) //오브젝트 상호작용으로 성공, 실패하는 상황
            {
                if(Input.GetKey(KeyCode.F))
                {
                    ready = false;
                    isclose = false;
                    isrepeat = false;
                }
            }
        }
        if(!isrepeat)
        {
            if((int)GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                isclear = true;
            }
        }
    }

    void OnCollisionEnter(Collision target) {
        if(target.gameObject.CompareTag("Player"))
        {
            isclose = true;
        }
    }
    void OnCollisionExit(Collision target) {
        if(target.gameObject.CompareTag("Player"))
        {
            isclose = false;
        }
    }
}
