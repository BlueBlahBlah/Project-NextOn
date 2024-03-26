using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    void Update()
    {
        transform.position = this.offset + target.position;
    }
}
