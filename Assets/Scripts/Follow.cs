using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // ?°ë¼ê°?ëª©í‘œ?€ ?„ì¹˜ ?¤í”„?‹ì„ Public ë³€?˜ë¡œ ? ì–¸
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // ?€ê²??„ì¹˜ ?¤ì •
    }
}
