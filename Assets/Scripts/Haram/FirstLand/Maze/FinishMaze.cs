using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMaze : MonoBehaviour
{
    public bool isClose;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isClose = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            isClose = false;
    }
}
