using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWings : MonoBehaviour
{
    public float rotationSpeed = 500f; // Adjust this to control the rotation speed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate around the Z-axis based on time and rotation speed
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
