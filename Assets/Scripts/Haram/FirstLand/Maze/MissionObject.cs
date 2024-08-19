using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionObject : MonoBehaviour
{
    private bool isClose;
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
            //if (Input.GetKey(KeyCode.F))
            //{
                curtime += Time.deltaTime;
                slider.value = curtime / maxtime;
                if (slider.value == 1)
                {
                    slider.gameObject.SetActive(false);
                    slider.value = 0;
                    ItemImage.SetActive(true);
                    MazeGenerate.mazeGenerate.Generate(5 * maze.missionCount + 1);
                    maze.missionCount++;
                    parent.SetActive(false);
                }
            //}
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
        slider.value = 0;
        slider.gameObject.SetActive(false);
        isClose = false;
        isStay = false;
    }
}
