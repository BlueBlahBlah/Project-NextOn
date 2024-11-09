using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject BlockEnter;
    [SerializeField]
    private Dialogue _dialogue;

    public void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.DialogueEventByNumber(_dialogue, 158);
            BlockEnter.SetActive(true);
            PoolManager.poolManager.ActivefalseAllPool();
        }
    }
}
