using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMonsterStartArea : MonoBehaviour
{
    [SerializeField] private bool Active;       //諛쒕룞?덈뒗吏? ?쒕쾲 諛쒕룞?섎㈃ ?ㅼ떆 諛쒕룞?섏? ?딅룄濡?    // Start is called before the first frame update
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
            //?쒗넗由ъ뼹 ?쒖옉?섎뒗 遺遺?異붽?
            Active = true;
            //EventManager.Instance.TimeStop();   //?쒓컙 ?뺤?
            EventManager.Instance.PrintMSG();
        }
        
    }
}
