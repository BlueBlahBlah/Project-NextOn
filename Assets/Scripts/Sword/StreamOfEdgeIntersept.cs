using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StreamOfEdgeIntersept : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    private Rigidbody rigidbody;
    public float moveSpeed = 5f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        toMove();
    }

    void Update()
    {
        
    }

    public void toMove()
    {
        StartCoroutine(MoveToDestination(destination.transform.position));
    }

    IEnumerator MoveToDestination(Vector3 destination)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; 
        }
    }
}
