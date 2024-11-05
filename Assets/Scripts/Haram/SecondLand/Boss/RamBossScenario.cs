using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RamBossScenario : MonoBehaviour
{
    public static RamBossScenario instance;
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
    [SerializeField]
    private GameObject clearObject;
    WaitForSeconds wfs2 = new WaitForSeconds(2f);
    WaitForSeconds wfs1 = new WaitForSeconds(1f);
    void Awake()
    {
        instance = new RamBossScenario();
    }

    public IEnumerator BossCoroutine()
    {
        _bossTrigger.SetActive(false);
        //_bossMissionObject.SetActive(true);
        this.GetComponent<RamBossStageManager>().mobnum = 8;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        yield return wfs1;
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isFirstFin);
        
        yield return wfs2;
        this.GetComponent<RamBossStageManager>().mobnum = 16;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        yield return wfs1;
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isSecondFin);

        yield return wfs2;
        this.GetComponent<RamBossStageManager>().mobnum = 32;
        this.GetComponent<RamBossStageManager>().NoticeOn();
        yield return wfs1;
        this.GetComponent<RamBossStageManager>().StageSetting();
        yield return new WaitUntil(() => isThirdFin);

        CircleTree.SetActive(false);
        //_bossMob.GetComponent<Animator>().SetBool("Death", true);
        yield return new WaitForSeconds(2f);
        //_bossMob.SetActive(false);
        _lastFence.SetActive(false);
        _pathToClear.SetActive(true);
        clearObject.SetActive(true);
    }   
}
