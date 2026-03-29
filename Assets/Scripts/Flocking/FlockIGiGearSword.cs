using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockIGiGearSword : MonoBehaviour
{
    [SerializeField] private GameObject Duck;
    [SerializeField] private GameObject Effect;
    [SerializeField] private int respawnTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("attack",Random.Range(1,20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {
        // "Enemy" ?�그�?가�?가??가까운 게임 ?�브?�트�?찾음
        GameObject nearestEnemy = FindRandomEnemy();
        respawnTime = Random.Range(8, 15);
        // ?�이 발견?�면
        if (nearestEnemy != null)
        {
            // 가??가까운 ?�의 ?�치�??�음
            Vector3 enemyPosition = nearestEnemy.transform.position;

            /*Duck.SetActive(false);
            Invoke("respawn",3);*/
            // ?�당 ?�치???�킬???�성
            Instantiate(Effect, enemyPosition, Quaternion.identity);
            
        }

        
        Invoke("attack",respawnTime);
    }

    /*void respawn()
    {
        Debug.Log("분홍?�리");
        Duck.SetActive(true);
    }*/
    
    // "Enemy" ?�그�?가�?가??가까운 게임 ?�브?�트 5개중 ?�나�?고르???�수
    private GameObject FindRandomEnemy()
    {
        // "Enemy" ?�그�?가�?모든 ?�브?�트�?배열�?가?�옴
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 만약 enemies 배열??비어 ?�다�?null??반환
        if (enemies.Length == 0)
        {
            return null;
        }

        // 가??가까운 5개의 ?�을 ?�?�할 리스??        List<GameObject> nearestEnemies = new List<GameObject>();

        // 모든 ?�을 ?�면??가??가까운 5개�? 찾음
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

        // ?�덤?�로 ?�나 ?�택
        GameObject randomEnemy = nearestEnemies[Random.Range(0, nearestEnemies.Count)];

        return randomEnemy;
    }
}
