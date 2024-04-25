using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Peiz3Monster : MonoBehaviour
{
    public Transform target;
    public bool isChase; // 추적을 결정하는 bool 변수
    Material mat;
    NavMeshAgent nav; // Nav Agent를 사용하기 위해서는 Nav Mesh 생성 필수
    // NavMesh : NavAgent가 경로를 그리기 위한 바탕(Mesh)
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
        if(isChase) // 추적을 결정하는 bool 변수 사용
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
        Debug.LogError("RageDone 호출");
        anim.SetTrigger("Chase");
        isChase = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //스크립트 가져와서 피 닳기
        }
    }
}


   

    
   
   
