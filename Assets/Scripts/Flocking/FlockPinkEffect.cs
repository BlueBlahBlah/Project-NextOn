using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockPinkEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        explore();
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void explore()
    {
        //콜라이더를 담는 배열
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);      //반경 5에 위치한 오브젝트들을 배열에 담는다
        if (colls.Length == 0)      //반경에 아무것도 없는 경우
        {
            return;
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       //Enemy tag를 가진경우
            {
                //공격하는 매커니즘
                Debug.Log("이기어검 공격성공");
            }
        }
    }
}
