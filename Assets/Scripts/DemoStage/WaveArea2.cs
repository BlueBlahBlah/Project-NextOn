using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArea2 : MonoBehaviour
{
    [SerializeField] private StageManager StageManager;
    // Start is called before the first frame update
    void Start()
    {
        StageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager.Area2Function();
        }
        
    }
}
