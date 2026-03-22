using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    private Transform Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //항상 카메라를 정면으로 바라 볼 수 있도록
        transform.LookAt(transform.position + Cam.rotation * Vector3.forward,Cam.rotation * Vector3.up);
    }
}
