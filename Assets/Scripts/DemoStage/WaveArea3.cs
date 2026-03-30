using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArea3 : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        bool Permission = EventManager.Instance.Area3;
        if (other.CompareTag("Player") && Permission == false)
        {
            Debug.LogError("而댄뙆?쇰윭瑜?癒쇱? 怨좎퀜蹂댁옄");
        }
        
    }
    
}
