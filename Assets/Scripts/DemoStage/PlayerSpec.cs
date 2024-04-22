using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSpec : MonoBehaviour
{
    //근처의 몬스터들을 담는 배열
    private Collider[] colls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //대형 몬스터 등장 시 근처 몬스터들 일시정지
    public void ProtectPlayerWhenBigMonAppear()
    {
        
        colls = Physics.OverlapSphere(transform.position, 25f);
        if (colls.Length == 0)
        {
            return;
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<NavMeshAgent>().speed = 0;
            }
        }
    }

    public void ResumeMonsters()
    {
        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<NavMeshAgent>().speed = 2;
            }
        }
    }
}
