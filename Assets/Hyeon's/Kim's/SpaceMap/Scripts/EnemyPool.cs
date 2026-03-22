using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 서로 다른 적들의 프리팹을 배열로 저장
    public int poolSize = 5; // 각 적 타입당 풀 사이즈
    private Dictionary<int, List<GameObject>> pools; // 오브젝트 풀을 저장할 딕셔너리

    void Start()
    {
        pools = new Dictionary<int, List<GameObject>>();

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(enemyPrefabs[i]);
                obj.SetActive(false);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject GetPooledEnemy(int enemyType)
    {
        foreach (GameObject obj in pools[enemyType])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // 풀에 사용 가능한 오브젝트가 없으면 새로 생성하여 추가
        GameObject newObj = Instantiate(enemyPrefabs[enemyType]);
        newObj.SetActive(false);
        pools[enemyType].Add(newObj);
        return newObj;
    }
}
