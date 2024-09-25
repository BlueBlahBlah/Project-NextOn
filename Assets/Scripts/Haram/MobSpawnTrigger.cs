using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobSpawnTrigger : Trigger
{
    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private GameObject _destroyGround;
    [SerializeField]
    private GameObject _wallToPreventDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isTriggered)
        {
            foreach(GameObject _destroy in _destroyGround)
            {
                _destroy.SetActive(false);
            }
            for(int i = 0; i < 10; i++)
            {
                int x = Random.Range(0,2);
                PoolManager.poolManager.MonsterGet(_spawnPoints, x);
            }
            isTriggered = false;
        }*/
    }

    public override void OnTriggerEnter(Collider collision)
    {
        isTriggered = true;
        
        if(_destroyGround != null)
            _destroyGround.SetActive(false);
        if(_wallToPreventDestroy != null)
            _wallToPreventDestroy.SetActive(true);
        
        if(_spawnPoints.Length > 0)
        {
            for(int i = 0; i < 10; i++)
            {
                int x = Random.Range(0,2);
                PoolManager.poolManager.MonsterSpawn(_spawnPoints, x);
            } 
        }
        isTriggered = false;
        this.gameObject.SetActive(false);
    }
}
