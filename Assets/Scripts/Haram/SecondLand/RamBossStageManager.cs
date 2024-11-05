using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

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
    public int stageMobCount = 0; //각 스테이지 총 몬스터 마릿수
    public int stageNum = 0;
    public bool isStageCleared;
    public bool isStageRestart;
    public bool isMissionStart;
    public bool isWaitForNextStage;
    public int mobnum;
    public float curtime = 0;
    public int missioncount = 0;
    public float missionTime= 0;
    public float completeTime;
    public Slider slider;
    WaitForSeconds wfs2 = new WaitForSeconds(2f);
    private RamBossScenario ramBossScenario;
    void Awake()
    {
        instance = new RamBossStageManager();
    }
    void Start()
    {
        ramBossScenario = this.GetComponent<RamBossScenario>();
    }
    void Update()
    {
        if(isMissionStart)
        {
            InspectComplete();
            if(isStageCleared)
            {
                missioncount = 0;
                _mobCountText.text = "Cleared!!!";
            }
            else
                _mobCountText.text = missioncount.ToString();

            curtime += Time.deltaTime;
            missionTime += Time.deltaTime;
            slider.value = missionTime / completeTime;
            if(curtime >= 1)
            {
                missioncount += stageNum + 1;
                curtime = 0 ;
                for(int i = 0; i < stageNum + 1; i++)
                {
                    PoolManager.poolManager.MonsterSpawn(_spawnPoint,stageNum);
                    stageMobCount++;
                }
            }
            if(PoolManager.poolManager.GetAllPoolSetActive() < stageMobCount)
            {
                int temp = stageMobCount - PoolManager.poolManager.GetAllPoolSetActive();
                stageMobCount -= temp;
                missioncount -= temp;
            }
            
        }
    }
    public int GetStageNum()
    {
        return stageNum;
    }
    public void GoNextStage()
    {
        missionTime = 0;
        stageNum++;
    }

    public void InspectComplete()
    {
        //숫자가 mobnum을 넘어갈 경우
        if(missioncount >= mobnum)
        {
            Debug.Log("실패");
            isMissionStart = false;
            missionTime = 0;
            StartCoroutine("StageReStart");
        }
        // 숫자가 mobnum을 넘어가지 않고 클리어됨
        else if(missionTime >= completeTime)
        {
            PoolManager.poolManager.ActivefalseAllPool();
            isStageCleared = true;
            isMissionStart = false;
            _mobCountText.text = "Cleared!!";
            slider.gameObject.SetActive(false);
            StageFin();
        }   
    }
    
    public void StageSetting()
    {
        PoolManager.poolManager.ActivefalseAllPool();
        _mobCountText.gameObject.SetActive(true);
        _mobCountText.text = "0";
        completeTime = (stageNum + 1) * 10; 
        missioncount = 0;
        isMissionStart = true;
        isStageCleared = false;
        slider.gameObject.SetActive(true);
    }

    public IEnumerator StageReStart()
    {
        PoolManager.poolManager.ActivefalseAllPool();
        yield return new WaitForSeconds(1f);
        _mobCountText.text = "OverFlowed!!!";
        slider.gameObject.SetActive(false);
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
