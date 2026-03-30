using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpiralSkillSpawn : MonoBehaviour
{
    public GameObject Skill;

    public void OnTriggerEnter(Collider other)
    {
        // 異⑸룎??臾쇱껜媛 Player ?쒓렇瑜?媛吏?寃쎌슦
        if (other.CompareTag("Player"))
        {
            // "Enemy" ?쒓렇瑜?媛吏?媛??媛源뚯슫 寃뚯엫 ?ㅻ툕?앺듃瑜?李얠쓬
            GameObject nearestEnemy = FindNearestEnemy();

            // ?곸씠 諛쒓껄?섎㈃
            if (nearestEnemy != null)
            {
                // 媛??媛源뚯슫 ?곸쓽 ?꾩튂瑜??살쓬
                Vector3 enemyPosition = nearestEnemy.transform.position;

                // ?대떦 ?꾩튂???ㅽ궗???앹꽦
                Instantiate(Skill, enemyPosition, Quaternion.identity);
            }
        }
    }

    // "Enemy" ?쒓렇瑜?媛吏?媛??媛源뚯슫 寃뚯엫 ?ㅻ툕?앺듃瑜?李얜뒗 ?⑥닔
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
