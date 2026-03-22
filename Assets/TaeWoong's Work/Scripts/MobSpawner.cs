using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 스폰할 몬스터 Prefab
    public float spawnRadius = 5f; // 스폰할 범위
    public int maxMonsters = 10; // 최대 몬스터 수
    public RepairArea repairArea; // RepairArea 참조

    [SerializeField] private List<GameObject> spawnedMonsters = new List<GameObject>(); // 현재 스폰된 몬스터 리스트

    public void SpawnMonster()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            if (spawnedMonsters.Count < maxMonsters)
            {
                // 오브젝트 주변 범위 내 랜덤한 위치 스폰
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                spawnPosition.y = transform.position.y + 1; // Y좌표 유지
                
                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                spawnedMonsters.Add(monster); // 스폰된 몬스터 추가

                // 몬스터가 비활성화되지 않도록 확인
                if (monster != null)
                {
                    monster.SetActive(true); // 몬스터를 활성화
                    // Enemy_Golem 스크립트 컴포넌트 가져오기
                    Mob enemyComponent = monster.GetComponent<Mob>();
                    if (enemyComponent != null)
                    {
                        enemyComponent.mobSpawner = this; // MobSpawner 참조 설정
                    }
                }
            }
        }
    }

    public int GetSpawnedMonsterCount()
    {
        return spawnedMonsters.Count; // 현재 스폰된 몬스터 수 반환
    }

    // 몬스터가 죽었을 때 호출할 메서드 (예: 몬스터가 파괴될 때)
    public void RemoveMonster(GameObject monster)
    {
        spawnedMonsters.Remove(monster);
        // RepairArea의 CheckMonstersStatus() 호출
        repairArea.CheckMonstersStatus(); 
    }

    // 모든 몬스터를 제거하는 메서드
    public void RemoveAllMonsters()
    {
        foreach (GameObject monster in spawnedMonsters)
        {
            if (monster != null)
            {
                Destroy(monster); // 몬스터를 파괴
            }
        }
        spawnedMonsters.Clear(); // 리스트 초기화
        // repairArea.CheckMonstersStatus(); // 상태 확인 호출
    }
}
