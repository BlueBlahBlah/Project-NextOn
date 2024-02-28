using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamOfEdgeSphere : MonoBehaviour
{
    private SphereCollider SphereCollider;

    private float TickTime;       //데미지를 주는 틱 간격
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        TickTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TickTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && TickTime >= 0.25f)
        {
            other.GetComponent<Enemy>().curHealth--;
            TickTime = 0;
        }
    }
}
