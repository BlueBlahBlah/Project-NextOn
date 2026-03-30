using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockPinkEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        explore();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void explore()
    {
        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);      
        if (colls.Length == 0)      
        {
            return;
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       
            {
                Debug.Log("Attack");
            }
        }
    }
}
