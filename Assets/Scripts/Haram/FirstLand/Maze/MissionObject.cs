using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionObject : MonoBehaviour
{
    public bool isClose;
    private bool isStay;
    [SerializeField]
    private GameObject ItemImage;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject parent;
    private float maxtime = 3;
    private float curtime;
    [SerializeField]
    private Maze maze;
    private void Update()
    {
        if (isStay)
        {

            curtime += Time.deltaTime;
            slider.value = curtime / maxtime;
            if (slider.value == 1)
            {
                slider.gameObject.SetActive(false);
                slider.value = 0;
                ItemImage.SetActive(true);
                ItemImage.GetComponent<Button>().interactable = false;
                for(int i = 0; i < 5 * (maze.missionCount + 1); i++)
                {
                    PoolManager.poolManager.FirstGet(0);
                }
                maze.missionCount++;
                parent.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            slider.gameObject.SetActive(true);
            isClose = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if(isClose && collision.CompareTag("Player"))
        {
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(isClose && collision.CompareTag("Player"))
        {   
            curtime = 0;
            slider.gameObject.SetActive(false);
            isClose = false;
            isStay = false;
        }
    }
}
