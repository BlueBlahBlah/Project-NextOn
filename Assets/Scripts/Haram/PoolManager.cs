using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;

    [SerializeField]
    private GameObject[] prefabs;

    List<GameObject>[] pools;

    [SerializeField]
    private Transform[] MazeSpawnPoints;
    [SerializeField]
    private Transform[] UnderMazeSpawnPoints;

    void Awake()
    {
        poolManager = this;
        pools = new List<GameObject>[prefabs.Length];
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public void PoolDestroy(int index)
    {
        GameObject select = null;
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
                Destroy(select);
        }
    }
    public GameObject FirstGet(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            int x = Random.Range(0,6);
            select = Instantiate(prefabs[index],MazeSpawnPoints[x].position, MazeSpawnPoints[x].rotation);
            pools[index].Add(select);
        }
        return select;
    }
    public GameObject SecondGet(int index, int first, int second)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                int x = Random.Range(first,second);
                select = item;
                select.transform.position = UnderMazeSpawnPoints[x].position;
                select.SetActive(true);
                
                break;
            }
        }

        if(!select)
        {
            int x = Random.Range(first,second);
            select = Instantiate(prefabs[index],UnderMazeSpawnPoints[x].position, UnderMazeSpawnPoints[x].rotation);
            select.transform.position = UnderMazeSpawnPoints[x].position;
            pools[index].Add(select);
        }
        return select;
    }
    public GameObject MonsterSpawn(Transform[] mobPoint, int index)
    {
        GameObject select = null;
        int x = Random.Range(0, mobPoint.Length);
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.transform.position = mobPoint[x].position;
                select.SetActive(true);
                
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(prefabs[index],mobPoint[x].position, mobPoint[x].rotation);
            select.transform.position = mobPoint[x].position;
            pools[index].Add(select);
        }
        return select;
    }
    public int GetAllPoolSetActive()
    {
        int poolNum = pools.Length;
        int num = 0;
        for(int  i = 0; i < poolNum; i++)
        {
            foreach(GameObject item in pools[i])
            {
                if(item.activeSelf)
                {
                    num++; 
                }
            }
        }

        return num;
    }

    public void ActivefalseIndexPool(int index)
    {
        foreach(GameObject item in pools[index])
        {
            if(item.activeSelf)
            {
                item.SetActive(false);
            }
        }
    }
    public void ActivefalseAllPool()
    {
        for(int i = 0; i < pools.Length; i++)
        {
            foreach(GameObject item in pools[i])
            {
                if(item.activeSelf)
                {
                    item.SetActive(false);
                }
            }    
        }
    }
    public void DestroyIndexPool(int index)
    {
        foreach(GameObject item in pools[index])
        {
            if(item.activeSelf)
            {
                Destroy(item);
            }
        }
    }

    public void DestroyAllPool()
    {
        for(int i = 0; i < pools.Length; i++)
        {
            foreach(GameObject item in pools[i])
            {
                if(item.activeSelf)
                {
                    Destroy(item);
                }
            }    
        }
    }
}
