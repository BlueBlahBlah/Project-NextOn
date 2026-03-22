using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private GameObject _gameClearPanel;
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FinishDial");
        }
    }
    public IEnumerator FinishDial()
    {
        UIManager.instance.DialogueEventByNumber(_dialogue, 160);
        yield return new WaitUntil(() => _dialogue.gameObject.activeSelf == false);
        _gameClearPanel.SetActive(true);
    }
}
