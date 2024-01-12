using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpiralSkillSpawn : MonoBehaviour
{
    public GameObject Skill;

    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            // "Enemy" 태그를 가진 가장 가까운 게임 오브젝트를 찾음
            GameObject nearestEnemy = FindNearestEnemy();

            // 적이 발견되면
            if (nearestEnemy != null)
            {
                // 가장 가까운 적의 위치를 얻음
                Vector3 enemyPosition = nearestEnemy.transform.position;

                // 해당 위치에 스킬을 생성
                Instantiate(Skill, enemyPosition, Quaternion.identity);
            }
        }
    }

    // "Enemy" 태그를 가진 가장 가까운 게임 오브젝트를 찾는 함수
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }
}