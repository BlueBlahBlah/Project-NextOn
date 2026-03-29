using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Peiz3Monster : MonoBehaviour
{
    public Transform target;
    public bool isChase; // ì¶”ì ??ê²°ì •?˜ëŠ” bool ë³€??    Material mat;
    NavMeshAgent nav; // Nav Agentë¥??¬ìš©?˜ê¸° ?„í•´?œëŠ” Nav Mesh ?ì„± ?„ìˆ˜
    // NavMesh : NavAgentê°€ ê²½ë¡œë¥?ê·¸ë¦¬ê¸??„í•œ ë°”íƒ•(Mesh)
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
        if(isChase) // ì¶”ì ??ê²°ì •?˜ëŠ” bool ë³€???¬ìš©
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
        Debug.LogError("RageDone ?¸ì¶œ");
        anim.SetTrigger("Chase");
        isChase = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //?¤í¬ë¦½íŠ¸ ê°€?¸ì??????³ê¸°
        }
    }
}


   

    
   
   
