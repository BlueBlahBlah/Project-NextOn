using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RamBossScenario : MonoBehaviour
{
    public static RamBossScenario instance;
    public bool isBossStart = false;
    public bool isFirstFin = false;
    public bool isSecondFin = false;
    public bool isThirdFin = false;
    [SerializeField]
    private GameObject _bossMob;

    [SerializeField]
    private GameObject _bossTrigger;
    [SerializeField]
    private GameObject CircleTree;
    [SerializeField]
    private GameObject _bossMissionObject;
    [SerializeField]
    private GameObject _lastFence;
    [SerializeField]
    private GameObject _pathToClear;
    void Awake()
    {
        instance = new RamBossScenario();
    }

    public IEnumerator BossCoroutine()
    {
        _bossTrigger.SetActive(false);
        _bossMissionObject.SetActive(true);
        isBossStart = true;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        this.GetComponent<RamBossStageManager>().mobnum = 8;
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isFirstFin);
        this.GetComponent<RamBossStageManager>().mobnum = 16;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isSecondFin);
        this.GetComponent<RamBossStageManager>().mobnum = 32;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isThirdFin);
        CircleTree.SetActive(false);
        _bossMob.GetComponent<Animator>().SetBool("Death", true);
        yield return new WaitForSeconds(2f);
        _bossMob.SetActive(false);
        _lastFence.SetActive(false);
        _pathToClear.SetActive(true);
    }   
}
