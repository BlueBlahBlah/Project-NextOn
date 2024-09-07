using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMonsterStartArea : MonoBehaviour
{
    [SerializeField] private bool Active;       //발동했는지? 한번 발동하면 다시 발동하지 않도록
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
            //EventManager.Instance.TimeStop();   //시간 정지
            EventManager.Instance.PrintMSG();
        }
        
    }
}
