using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathToEnter : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private GameObject[] _triggers;
    private bool _isEnter;
    private bool _isfinish;
    [SerializeField]
    private Transform[] _mobSpawnPoints;

    void Start()
    {
        StartCoroutine(PathCoroutine());
    }

    void Update()
    {
        if(_isfinish)
        {
            StopAllCoroutines();
            _isfinish = false;
        }
    }
    IEnumerator PathCoroutine()
    {
        yield return new WaitUntil(() => _triggers[0].GetComponent<Trigger>().isTriggered);

        UIManager.instance.DialogueEventByNumber(dialogue, 156);
        yield return new WaitUntil(() => dialogue.gameObject.activeSelf == false);

        for(int i = 0; i < 10; i++)
        {
            int x = Random.Range(0,2);
            PoolManager.poolManager.MonsterSpawn(_mobSpawnPoints,x);
            
        }
        yield return new WaitUntil(() => _triggers[1].GetComponent<Trigger>().isTriggered);

        PoolManager.poolManager.ActivefalseAllPool();
        _isfinish = true;
    }
}
