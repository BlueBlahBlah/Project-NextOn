using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int camIdx;
    private CameraManager cameraManager;

    void Start()
    {
        cameraManager = FindAnyObjectByType<CameraManager>();
        string objName = this.name;

        int.TryParse(objName, out camIdx);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraManager.SetTarget(camIdx - 1);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            cameraManager.SetTarget(camIdx - 1);

        }
    }
}
