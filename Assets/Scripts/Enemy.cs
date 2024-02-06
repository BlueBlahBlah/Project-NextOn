using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public BoxCollider AttackRange;

    Rigidbody rigid;
    CapsuleCollider capsuleCollider;

    NavMeshAgent navMeshAgent;
    Material material;
    Animator animator;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        material = GetComponent<Material>();
        
        ChaseStart();
    }

    void Update()
    {
        if(navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(target.position);
            navMeshAgent.isStopped = !isChase;
        }
    }

    void ChaseStart()
    {
        isChase = true;
        animator.SetBool("Chase", true);
    }
    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;   
        }
    }

    void Targeting()
    {
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits = 
            Physics.SphereCastAll(transform.position, targetRadius,
                                                    transform.forward, targetRange,
                                                    LayerMask.GetMask("Player"));

        if(rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }
    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        animator.SetBool("Attack",true);


        yield return new WaitForSeconds(0.2f);

        AttackRange.enabled = true;

        yield return new WaitForSeconds(1f);
        AttackRange.enabled = false;

        isChase = true;
        isAttack = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Chase", true);
    }
    IEnumerator OnDamage(Vector3 reactVec)
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            material.color = Color.white;
        }
        else
        {
            material.color = Color.gray;
            gameObject.layer = 14;
            animator.SetTrigger("Dead");
            isChase = false; 
            navMeshAgent.enabled = false;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            Destroy(gameObject,4);
        }
    }
    
}
