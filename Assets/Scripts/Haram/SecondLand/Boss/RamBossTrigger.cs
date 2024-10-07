using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamBossTrigger : MonoBehaviour
{
    public static RamBossTrigger instance;
    [SerializeField]
    private GameObject _circleTree;
    [SerializeField]
    private RamBossScenario _bossObject;

    public bool start;
    void Awake()
    {
        instance = new RamBossTrigger();
    }
    public void OnTriggerEnter(Collider collision)
    {
        StopAllCoroutines();
        if(collision.gameObject.CompareTag("Player"))
        {
            _bossObject = _bossObject.GetComponent<RamBossScenario>();
            _bossObject.StartCoroutine(_bossObject.BossCoroutine());
            _circleTree.SetActive(true);
        }
    }
}
