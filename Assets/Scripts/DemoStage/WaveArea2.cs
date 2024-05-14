using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveArea2 : MonoBehaviour
{
    [SerializeField] private bool peiz2Active;
    [SerializeField] private bool peiz3Active;
    // Start is called before the first frame update
    void Start()
    {
        peiz2Active = false;
        peiz3Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        /*//2페이즈시작
        if (other.CompareTag("Player") && peiz2Active == false)
        {
            peiz2Active = true;
            StageManager.Area2Function();
        }*/
        /*if (other.CompareTag("Player") && peiz2Active == true && peiz3Active == false && EventManager.Instance.Area3 == true)  //3페이즈 중에 Area 통과
        {
            peiz3Active = true;
            //StageManager.Area2Function();
            
        }*/
        
    }
}
