using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveArea1 : MonoBehaviour
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
            StageManager.Area1Function();
        }
        
    }
}
