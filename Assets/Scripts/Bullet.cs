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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 26)
        {
            Debug.Log("탄피탄피");
            Destroy(gameObject);
        }
    }
}
