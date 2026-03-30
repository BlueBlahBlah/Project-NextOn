using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // ?곕씪媛?紐⑺몴? ?꾩튂 ?ㅽ봽?뗭쓣 Public 蹂?섎줈 ?좎뼵
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // ?寃??꾩튂 ?ㅼ젙
    }
}
