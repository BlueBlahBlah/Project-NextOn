using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunspiral : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);
        if (colls.Length == 0)
        {
            Destroy(gameObject,5);
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))
            {
                //공격하는 메커니즘
            }
        }
    }
}
