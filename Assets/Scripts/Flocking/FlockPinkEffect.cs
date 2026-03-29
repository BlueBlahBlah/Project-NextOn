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
        //콜라?�더�??�는 배열
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);      //반경 5???�치???�브?�트?�을 배열???�는??        if (colls.Length == 0)      //반경???�무것도 ?�는 경우
        {
            return;
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       //Enemy tag�?가진경??            {
                //공격?�는 매커?�즘
                Debug.Log("?�기?��? 공격?�공");
            }
        }
    }
}
