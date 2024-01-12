using System.Collections;
using UnityEngine;

public class GunSpiral_Gun : MonoBehaviour
{
    public float xRotationSpeed = 2000f;

    void Start()
    {
        
    }

    void Update()
    {
        // x축 회전
        transform.Rotate(Vector3.up * xRotationSpeed * Time.deltaTime);

       
    }

    
}