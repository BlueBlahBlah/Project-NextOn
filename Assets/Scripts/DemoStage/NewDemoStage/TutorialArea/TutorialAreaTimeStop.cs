using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAreaTimeStop : AfterMovingTutorial
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            EventManager.Instance.TimeStop();   //시간 정지
        }
        
    }
}
