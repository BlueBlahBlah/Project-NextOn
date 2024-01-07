using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] Transform lookAtPos;
    Transform cameraMain;

    void Awake(){
        cameraMain = Camera.main.gameObject.transform;
    }
    // Update is called once per frame
    void Update()
    {
        cameraMain.LookAt(lookAtPos);//looks at player
    }
}
