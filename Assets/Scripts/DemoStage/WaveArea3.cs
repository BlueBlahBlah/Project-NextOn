using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArea3 : MonoBehaviour
{
    [SerializeField] private StageManager StageManager;
    
    void Start()
    {
        StageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        bool Permission = StageManager.GetComponent<StageManager>().Area3;
        if (other.CompareTag("Player") && Permission == false)
        {
            Debug.LogError("컴파일러를 먼저 고쳐보자");
        }
        
    }
    
}
