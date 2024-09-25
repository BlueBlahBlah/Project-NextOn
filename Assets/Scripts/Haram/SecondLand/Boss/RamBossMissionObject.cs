using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RamBossMissionObject : MonoBehaviour
{
    [SerializeField]
    private RamBossStageManager ramBossStageManager;
    private bool isClose;
    private bool isStay;
    [SerializeField]
    public Slider slider;
    private float maxtime = 3;
    public float curtime;
    private void Update()
    {
        if (isStay && ramBossStageManager.isMissionStart)
        {
            curtime += Time.deltaTime;
            slider.value = curtime / maxtime;
            if (slider.value == 1)
            {
                Debug.Log("value 1");
                slider.value = 0;
                curtime = 0;
                slider.gameObject.SetActive(false);
                ramBossStageManager.isStageCleared = true;
                PoolManager.poolManager.ActivefalseAllPool();
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(ramBossStageManager.isMissionStart)
                slider.gameObject.SetActive(true);
            isClose = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if(isClose && collision.CompareTag("Player"))
        {
            if(ramBossStageManager.isMissionStart)
                slider.gameObject.SetActive(true);
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        isClose = false;
        isStay = false;
    }

}
