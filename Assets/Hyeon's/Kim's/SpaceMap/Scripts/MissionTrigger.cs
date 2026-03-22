using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    private GameObject Player;
    [Header("2 Scenario Trigger")]
    public GameObject Trigger1;
    public GameObject Trigger2;
    public GameObject Door1;
    public GameObject Door2;

    [SerializeField] private GameObject eventBtn;

    [SerializeField] bool Door1_isClose, Door2_isClose;

    void Start()
    {
        Player = GameObject.Find("Player");
        Door1_isClose = false;
        Door2_isClose = false;
    }

    void Update()
    {
        if (Vector3.Distance(Trigger1.transform.position, Player.transform.position) <= 4f)
        {
            eventBtn.SetActive(true);
            Door1_isClose=true;
        }
        else if(Vector3.Distance(Trigger2.transform.position, Player.transform.position) <= 4f)
        {
            eventBtn.SetActive(true);
            Door2_isClose = true;
        }
        else
        {
            Door1_isClose = false;
            Door2_isClose = false;
            eventBtn.SetActive(false);
        }

    }

    public void Close()
    {
        if (Door1_isClose)
        {
            Door1.GetComponent<CloseDoor>().enabled = true;
        }
        if (Door2_isClose)
        {
            Door2.GetComponent<CloseDoor>().enabled = true;
        }
    }
}
