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
        //??긽 移대찓?쇰? ?뺣㈃?쇰줈 諛붾씪 蹂????덈룄濡?        transform.LookAt(transform.position + Cam.rotation * Vector3.forward,Cam.rotation * Vector3.up);
    }
}
