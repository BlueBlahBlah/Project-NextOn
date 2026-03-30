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
        // x異??뚯쟾
        transform.Rotate(Vector3.up * xRotationSpeed * Time.deltaTime);

       
    }

    
}
