using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ���� �ٸ� ������ �������� �迭�� ����
    public int poolSize = 5; // �� �� Ÿ�Դ� Ǯ ������
    private Dictionary<int, List<GameObject>> pools; // ������Ʈ Ǯ�� ������ ��ųʸ�

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

        // Ǯ�� ��� ������ ������Ʈ�� ������ ���� �����Ͽ� �߰�
        GameObject newObj = Instantiate(enemyPrefabs[enemyType]);
        newObj.SetActive(false);
        pools[enemyType].Add(newObj);
        return newObj;
    }
}
