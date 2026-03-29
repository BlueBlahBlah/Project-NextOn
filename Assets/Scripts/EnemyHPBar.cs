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
        //??ÉÅ Ïπ¥Î©î?ºÎ? ?ïÎ©¥?ºÎ°ú Î∞îÎùº Î≥????àÎèÑÎ°?        transform.LookAt(transform.position + Cam.rotation * Vector3.forward,Cam.rotation * Vector3.up);
    }
}
