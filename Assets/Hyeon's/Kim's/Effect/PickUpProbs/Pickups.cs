using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
           Invoke("destroy_Invoke",1f);
        }
    }

    private void destroy_Invoke()
    {
        Destroy(gameObject);
    }

}
