using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireGunFlame : MonoBehaviour
{
    [SerializeField] private Collider thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            Debug.Log("화염방사기 공격");
        }
    }
    
}
