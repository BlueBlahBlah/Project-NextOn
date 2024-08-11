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
            Debug.LogError("컴파일러를 먼저 고쳐보자");
        }
        
    }
    
}
