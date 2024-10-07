using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject BlockEnter;

    public void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BlockEnter.SetActive(true);
            PoolManager.poolManager.ActivefalseAllPool();
        }
    }
}
