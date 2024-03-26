using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairMemory : MonoBehaviour
{
    public Slider MemorySlider;
    public Slider RepairSlider;
    GameObject repair; 
    bool isclose;
    float ftime = 0f;
    float maxftime = 3f;

    void Start()
    {
        isclose = false;
        repair = GameObject.Find("Repair").transform.Find("RepairCanvas").gameObject;   
    }
    void Update()
    {
        if(isclose)
        {
            RepairSlider.value = 0;
            if(Input.GetKey(KeyCode.F))
            {
                ftime += Time.deltaTime;
                RepairSlider.value = ftime / maxftime;
                if(RepairSlider.value == 1){
                    MemorySlider.value += 0.25f;
                    repair.SetActive(false);
                    gameObject.SetActive(false);
                    RepairSlider.value = 0; //다음 repair를 진행하기 전 초기화 진행
                    EnemySpawn.instance.CreateNEnemy(5);
                }
            }
            else
                RepairSlider.value = ftime / maxftime;
        }
    }

    void OnCollisionEnter(Collision target) {
        if(target.gameObject.CompareTag("Player"))
        {
            isclose = true;
            repair.SetActive(true);
        }
    }
    void OnCollisionExit(Collision target) {
        if(target.gameObject.CompareTag("Player"))
        {
            isclose = false;
            ftime = 0;
            repair.SetActive(false);
        }
    }
       /*void OnTriggerEnter(Collider target) {
        if(target.CompareTag("InteractiveObject"))
        {
            Debug.Log("true");
            isclose = true;
        }
    }*/
}
