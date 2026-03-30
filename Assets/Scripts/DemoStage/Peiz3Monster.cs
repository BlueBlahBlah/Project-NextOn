using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peiz3Monster : MonoBehaviour
{
    public Transform target;
    public bool isChase;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material; 
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
    }
    
    void Update()
    {
        if(isChase)
            nav.SetDestination(target.position);
    }
    
    public void stopNav()
    {
        nav.Stop();
    }

    public void startNav()
    {
        nav.Resume();
    }

    private void RageDone()
    {
        Debug.LogError("RageDone");
        anim.SetTrigger("Chase");
        isChase = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            
        }
    }
}
