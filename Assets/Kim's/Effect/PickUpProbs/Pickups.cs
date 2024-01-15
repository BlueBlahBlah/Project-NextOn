using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            Debug.Log("Destroy!!!!!!!!!!!!!!!!!!!!!!!");
            Destroy(gameObject);
        }
    }

}
