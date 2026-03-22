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
        // "Enemy" 태그를 가진 가장 가까운 게임 오브젝트를 찾음
        GameObject nearestEnemy = FindRandomEnemy();
        respawnTime = Random.Range(8, 15);
        // 적이 발견되면
        if (nearestEnemy != null)
        {
            // 가장 가까운 적의 위치를 얻음
            Vector3 enemyPosition = nearestEnemy.transform.position;

            /*Duck.SetActive(false);
            Invoke("respawn",3);*/
            // 해당 위치에 스킬을 생성
            Instantiate(Effect, enemyPosition, Quaternion.identity);
            
        }

        
        Invoke("attack",respawnTime);
    }

    /*void respawn()
    {
        Debug.Log("분홍오리");
        Duck.SetActive(true);
    }*/
    
    // "Enemy" 태그를 가진 가장 가까운 게임 오브젝트 5개중 하나를 고르는 함수
    private GameObject FindRandomEnemy()
    {
        // "Enemy" 태그를 가진 모든 오브젝트를 배열로 가져옴
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 만약 enemies 배열이 비어 있다면 null을 반환
        if (enemies.Length == 0)
        {
            return null;
        }

        // 가장 가까운 5개의 적을 저장할 리스트
        List<GameObject> nearestEnemies = new List<GameObject>();

        // 모든 적을 돌면서 가장 가까운 5개를 찾음
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

        // 랜덤으로 하나 선택
        GameObject randomEnemy = nearestEnemies[Random.Range(0, nearestEnemies.Count)];

        return randomEnemy;
    }
}
