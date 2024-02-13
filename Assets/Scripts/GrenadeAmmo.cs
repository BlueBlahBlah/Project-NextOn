using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Effect;

    [SerializeField] private Rigidbody rigidbody;
    public float boomTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        Bullet.SetActive(true);
        Effect.SetActive(false);
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rigidbody.velocity;
        if (transform.position.y < 0.2F)
        {
            explosion();
        }
        boomTimer -= Time.deltaTime;
        if (boomTimer < 0)
        {
            explosion();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))        //적 오브젝트에 부딪히면
        {
            explosion();
        }
    }

    void explosion()
    {
        rigidbody.velocity = Vector3.zero;                                  //움직임 그만
        rigidbody.angularVelocity = new Vector3(0, 0, 0);             //회전 그만
        Effect.SetActive(true);
        Bullet.SetActive(false);

        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 5f);
        if (colls.Length == 0)      //반경에 아무것도 없는 경우
        {
            Destroy(gameObject,1.5f);
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       //Enemy tag를 가진경우
            {
                //공격하는 매커니즘
                Debug.Log("유탄 공격성공");
            }
        }
        
        
        
        Destroy(gameObject,1.5f);
    }
}
