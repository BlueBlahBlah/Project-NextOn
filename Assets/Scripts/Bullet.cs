using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float destroyDistance = 30f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // 만약 플레이어로부터 일정 거리 이상 떨어지면 총알 제거
        if (distanceToPlayer > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("명중");
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("탄-몬스터 타격");
            other.GetComponent<Enemy>().curHealth--;
            Destroy(gameObject);
        }
    }
}
