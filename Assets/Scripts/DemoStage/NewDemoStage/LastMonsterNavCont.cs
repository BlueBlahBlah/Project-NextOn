using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LastMonsterNavCont : MonoBehaviour
{
    [SerializeField] private bool Landing;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Landing = false;
        rb = gameObject.AddComponent<Rigidbody>(); // Add Rigidbody component to apply gravity
        rb.useGravity = true; // Enable gravity
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional logic here if needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the "Ground" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Landing = true;
            this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            this.gameObject.GetComponent<Enemy>().startNav();
        }
    }
}