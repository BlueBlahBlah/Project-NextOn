using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderMob : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent nav;
    [SerializeField]
    private Transform[] targets;
    private int targetNum = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector3.Distance(gameObject.transform.position,target.position) < 1f)
        //    Destroy(gameObject);
    }


    public void SetTarget(Transform target)
    {
        this.target = target; 
        nav.SetDestination(target.position);
    }
}