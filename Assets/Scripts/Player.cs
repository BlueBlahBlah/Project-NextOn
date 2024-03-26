using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    bool Run;
    bool jdown;
    bool isJump;

    public float speed;
    public float jumpPower;

    Animator animator;
    Rigidbody rigid;
    CharacterController characterController;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        GetInput();
        Move();
        CameraTurn();
        Jump();
        Attack();
    }
    
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        FreezeRotation();
    }

    

    void Attack()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
        List<GameObject> Enemies;
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        float shortDis = Vector3.Distance(gameObject.transform.position, Enemies[0].transform.position); // 첫번째를 기준으로 잡아주기 
        GameObject enemy = Enemies[0];
        foreach (GameObject found in Enemies)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
 
            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                enemy = found;
                shortDis = Distance;
            }
        }
            Destroy(enemy);
        }
 
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        Run = Input.GetButton("Run");
        jdown = Input.GetButtonDown("Jump");
    }
    void Move()
    {
        moveVec = new Vector3(hAxis,0,vAxis).normalized;

        if(moveVec != Vector3.zero)
        {
            animator.SetBool("isWalk", true);

            if(Run)
            {
                transform.position += moveVec *speed * Time.deltaTime;
                animator.SetBool("isRun", Run);
            }
            else
            {
                animator.SetBool("isRun",Run);
                transform.position += moveVec *speed * 0.5f * Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", Run);
        }
    }

    void Jump()
    {
        if(jdown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse) ;
            isJump = true;
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Floor")
            isJump = false;   
    }
    void CameraTurn()
    {
        transform.LookAt(transform.position + moveVec);
    }
}
