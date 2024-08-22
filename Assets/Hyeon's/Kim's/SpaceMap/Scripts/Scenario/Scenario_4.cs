using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_4 : MonoBehaviour
{
    [SerializeField] private Scenario scenario;
    [SerializeField] bool _doorClose1, _doorClose2, is1_TriggerPass, is2_TriggerPass, is_End;
    public GameObject[] EnemySpawn;

    [Header("Door Trigger")]
    public GameObject firstTrigger;

    [Header("End Point")]
    public GameObject secondTrigger;    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
