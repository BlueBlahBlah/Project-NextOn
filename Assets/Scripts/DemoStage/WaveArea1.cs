using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveArea1 : MonoBehaviour
{

    [SerializeField] private bool Active;
    // Start is called before the first frame update
    void Start()
    {

        Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Active == false)
        {
            //튜토리얼 시작하는 부분 추가

            Active = true;
            EventManager.Instance.FirstWelcomeMSG();
        }
        
    }
}
