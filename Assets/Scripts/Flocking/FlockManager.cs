using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager FM;
    public GameObject duckPrefab;
    public int numDuck = 20;
    public GameObject[] allDuck;
    public Vector3 swimLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos = Vector3.zero;

    [Header("Fish Settinigs")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(5.0f, 10.0f)]
    public float maxSpeed;
    [Range(10.0f, 15.0f)]
    public float neighbourDistance;
    [Range(1.0f, 5.0f)]
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        allDuck = new GameObject[numDuck];
        for (int i = 0; i < numDuck; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            
            // Quaternion.Euler를 사용하여 원하는 회전값을 설정
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            allDuck[i] = Instantiate(duckPrefab, pos, rotation);
        }

        FM = this;
        goalPos = this.transform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
            
        }
    }
}
