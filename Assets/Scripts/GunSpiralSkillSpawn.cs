using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpiralSkillSpawn : MonoBehaviour
{
    public GameObject Skill;

    public void OnTriggerEnter(Collider other)
    {
        // ì¶©ëŒ??ë¬¼ì²´ê°€ Player ?œê·¸ë¥?ê°€ì§?ê²½ìš°
        if (other.CompareTag("Player"))
        {
            // "Enemy" ?œê·¸ë¥?ê°€ì§?ê°€??ê°€ê¹Œìš´ ê²Œì„ ?¤ë¸Œ?íŠ¸ë¥?ì°¾ìŒ
            GameObject nearestEnemy = FindNearestEnemy();

            // ?ì´ ë°œê²¬?˜ë©´
            if (nearestEnemy != null)
            {
                // ê°€??ê°€ê¹Œìš´ ?ì˜ ?„ì¹˜ë¥??»ìŒ
                Vector3 enemyPosition = nearestEnemy.transform.position;

                // ?´ë‹¹ ?„ì¹˜???¤í‚¬???ì„±
                Instantiate(Skill, enemyPosition, Quaternion.identity);
            }
        }
    }

    // "Enemy" ?œê·¸ë¥?ê°€ì§?ê°€??ê°€ê¹Œìš´ ê²Œì„ ?¤ë¸Œ?íŠ¸ë¥?ì°¾ëŠ” ?¨ìˆ˜
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
