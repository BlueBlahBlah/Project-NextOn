using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChangeGravity : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.8F)
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero; // Stop all movement
            rigidbody.angularVelocity = Vector3.zero; // Stop all rotation
            rigidbody.isKinematic = true; // Optionally, make the object kinematic to prevent any further physics interactions
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            EventManager.Instance.PrintMSG();
            Destroy(this.gameObject);
        }
        
    }
}
