using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class RamBossStageManager : MonoBehaviour
{
    public static RamBossStageManager instance;

    [SerializeField]
    private TMP_Text _mobCountText;
    [SerializeField]
    private RamBossMissionObject ramBossMissionObject;
    [SerializeField]
    private GameObject[] Notices;
    [SerializeField]
    private Transform[] _spawnPoint;
    public int stageMaxMobCount = 0; //각 스테이지 총 몬스터 마릿수
    public int stageNum = 0;
    public bool isStageCleared;
    public bool isStageRestart;
    public bool isMissionStart;
    public bool isWaitForNextStage;
    public int mobnum;

    private RamBossScenario ramBossScenario;
    void Awake()
    {
        instance = new RamBossStageManager();
    }
    void Start()
    {
        ramBossScenario = this.GetComponent<RamBossScenario>();
    }
    public int GetStageNum()
    {
        return stageNum;
    }
    public void GoNextStage()
    {
        stageNum++;
    }

    public void StageSetting()
    {
        for(int i = 0; i < mobnum; i++)
        {
            int x = Random.Range(0,2);
            PoolManager.poolManager.MonsterSpawn(_spawnPoint,stageNum);
        }
        _mobCountText.gameObject.SetActive(true);
        _mobCountText.text = "0";
        isStageRestart = false;
        isWaitForNextStage = false;
    }
    public void ViewMobCount()
    {
        StopCoroutine(StageReStart());
        int curMob = PoolManager.poolManager.GetAllPoolSetActive();
        if(isStageCleared)
        {
            isMissionStart = false;
            _mobCountText.text = "Cleared!!";
            isStageCleared = false;
            isWaitForNextStage = true;
            StageFin();
        }
        else if((mobnum - curMob) == mobnum) //오버플로우
        {
            isMissionStart = false;
            ramBossMissionObject.curtime = 0;
            ramBossMissionObject.slider.value = 0;
            ramBossMissionObject.slider.gameObject.SetActive(false);
            isStageRestart = true;
            _mobCountText.text = "-1";
            StartCoroutine(StageReStart());
        }
        else if((mobnum - curMob) == (mobnum - 1))
        {
            _mobCountText.text = (mobnum - curMob).ToString();
            isMissionStart = true;
        }
        else
        {
            _mobCountText.text = (mobnum - curMob).ToString();
        }
    }

    public IEnumerator StageReStart()
    {
        WaitForSeconds wfs2 = new WaitForSeconds(2f);
        yield return wfs2;
        _mobCountText.text = "OverFlowed!!!";
        yield return wfs2;
        NoticeOn();
        yield return wfs2;
        StageSetting();
    }

    public void NoticeOn()
    {
        Notices[stageNum].SetActive(false);
        Notices[stageNum].SetActive(true);
    }
    public void StageFin()
    {
        switch(stageNum)
        {
            case 0:
                Debug.Log(stageNum);
                this.GetComponent<RamBossScenario>().isFirstFin = true;
                break;
            case 1:
                Debug.Log(stageNum);
                this.GetComponent<RamBossScenario>().isSecondFin = true;
                break;
            case 2:
                Debug.Log(stageNum);
                this.GetComponent<RamBossScenario>().isThirdFin = true;
                break;
        }
        GoNextStage();
    }
}
