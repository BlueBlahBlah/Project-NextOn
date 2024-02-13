using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSilverEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            //Debug.LogError("실버스톤 공격");
        }
    }
}
