using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;
    public Transform[] points;
    public GameObject[] prefabs;
    bool isCreate;

    public int beginenemy;
    float time;
    int createTime = 3;
    public Slider MemorySlider;

    void Awake()
    {
        if(EnemySpawn.instance == null)
            EnemySpawn.instance = this;
    }
    void Start()
    {
        points = GetComponentsInChildren<Transform>();  
    }

    void Update(){
        time += Time.deltaTime;
        /*if(MemorySlider.value == 0) //메모리 누수가 되어 0이 되면 몬스터를 생성할 수 있도록 isCreate를 true
            isCreate = true;
            
        if(isCreate && time > createTime){ //isCreate
            CreatetenEnemy();
            time = 0;
        }*/
    }


    public void CreateBegin()
    {
        for(int i = 0; i < beginenemy; i++)
        {
            int idx = UnityEngine.Random.Range(1, points.Length);   
            Instantiate(prefabs[0],points[idx].position,points[idx].rotation);
        }
    }
    public void CreatetenEnemy()
    {
        int EnemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(EnemyCount < 10){
            for(int i = EnemyCount; i < 10; i++){
                int idx = UnityEngine.Random.Range(1, points.Length);
                Instantiate(prefabs[0],points[idx].position,points[idx].rotation);
            }
        }
    }
    public void CreateNEnemy(int N)
    {
        for(int i = 0; i < N; i++){
            int idx = UnityEngine.Random.Range(1, points.Length);
            Instantiate(prefabs[0],points[idx].position,points[idx].rotation);
        }
    }
}
