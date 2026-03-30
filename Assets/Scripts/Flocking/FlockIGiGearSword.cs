using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockIGiGearSword : MonoBehaviour
{
    [SerializeField] private GameObject Duck;
    [SerializeField] private GameObject Effect;
    [SerializeField] private int respawnTime;

    void Start()
    {
        Invoke("attack", Random.Range(1, 20));
    }

    void Update()
    {
        
    }

    void attack()
    {
        GameObject nearestEnemy = FindRandomEnemy();
        respawnTime = Random.Range(8, 15);
        
        if (nearestEnemy != null)
        {
            Vector3 enemyPosition = nearestEnemy.transform.position;
            Instantiate(Effect, enemyPosition, Quaternion.identity);
        }
        
        Invoke("attack", respawnTime);
    }
    
    private GameObject FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            return null;
        }

        List<GameObject> nearestEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (nearestEnemies.Count < 5)
            {
                nearestEnemies.Add(enemy);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (distance < Vector3.Distance(transform.position, nearestEnemies[i].transform.position))
                    {
                        nearestEnemies[i] = enemy;
                        break;
                    }
                }
            }
        }

        if (nearestEnemies.Count == 0) return null;

        GameObject randomEnemy = nearestEnemies[Random.Range(0, nearestEnemies.Count)];
        return randomEnemy;
    }
}
