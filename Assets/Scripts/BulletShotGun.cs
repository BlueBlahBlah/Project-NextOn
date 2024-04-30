using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotGun : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private DamageManager DamageManager;
    public float destroyDistance = 30f;
    public int Damage;

    private void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
        player = GameObject.Find("Player");
        Damage = 1;
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
        int TempDamage = DamageManager.ShotGun_DamageCounting * Damage;
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().curHealth -= TempDamage;
            Destroy(gameObject);
        }
    }
}
