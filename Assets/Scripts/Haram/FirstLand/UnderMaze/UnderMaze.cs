using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnderMaze : MonoBehaviour
{
    public static UnderMaze underMaze;
    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private GameObject[] TempWalls;
    [SerializeField]
    public GameObject[] Triggers;
    [SerializeField]
    private GameObject leaderMob;
    [SerializeField]
    private GameObject Player;
    
    [SerializeField]
    private GameObject[] MissionButton;
    [SerializeField]
    private GameObject FoodWallTrigger;
    [SerializeField]
    private GameObject PathtoSecondLand;
    [SerializeField]
    private GameObject CompletePath;

    public int missionCount;

    void Awake()
    {
        underMaze = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(FirstLandManager.firstLandManager.isUnderStart)
        {
            StartCoroutine(UnderMazeRoutine());
            FirstLandManager.firstLandManager.isUnderStart = false;
        }
    }

    IEnumerator UnderMazeRoutine()
    {
        //캐릭터가 첫번째 트리거를 지나는 것을 대기
        yield return new WaitUntil(() => Triggers[0].GetComponent<Trigger>().isTriggered);

        //몬스터 10마리를 스폰구역 1,2,3에서 랜덤 스폰한다
        for(int i = 0; i < 10; i++)
        {
            int x = Random.Range(0,2);
            PoolManager.poolManager.SecondGet(x,0,3);
        }
        //첫번째 지역에 나온 몬스터를 다 잡을 때까지 기다림
        yield return new WaitUntil(() => PoolManager.poolManager.GetAllPoolSetActive() == 0);
        //오르막길 생성
        TempWalls[1].SetActive(true);
        
        yield return new WaitUntil(() => Triggers[1].GetComponent<Trigger>().isTriggered);
        for(int i = 0; i < 15; i++)
            PoolManager.poolManager.SecondGet(0,3,7);
        
        //두번째 지역에 나온 몬스터를 다 잡을 때까지 기다림
        yield return new WaitUntil(() => PoolManager.poolManager.GetAllPoolSetActive() == 0);

        //세번째 지역으로 가는 길 열기
        TempWalls[3].SetActive(false);
        TempWalls[0].SetActive(false);


        yield return new WaitUntil(() => Triggers[2].GetComponent<Trigger>().isTriggered);


        //입장 후 길을 막음
        TempWalls[0].SetActive(true);

        //세번째 지역에 몬스터 스폰
        for(int i = 0; i < 10; i++)
            PoolManager.poolManager.SecondGet(0,7,10);

        //세번째 지역에 나온 몬스터를 다 잡을 때까지 기다림
        yield return new WaitUntil(() => PoolManager.poolManager.GetAllPoolSetActive() == 0);

        //NPC에게 가는 길이 열림
        TempWalls[4].SetActive(false);
        yield return new WaitUntil(() => Triggers[3].GetComponent<Trigger>().isTriggered);

        UIManager.instance.DialogueEventByNumber(_dialogue, 134);

        CompletePath.SetActive(false);
        //버튼을 활성화시킴
        for(int i = 0; i < 3; i++)
            MissionButton[i].GetComponent<Button>().interactable = true;
        
        yield return new WaitUntil(() => missionCount == 3);
        PathtoSecondLand.SetActive(true);
        UIManager.instance.DialogueEventByNumber(_dialogue, 148);

        yield return new WaitUntil(() => FoodWallTrigger.GetComponent<Trigger>().isTriggered);
        
        FirstLandManager.firstLandManager.isUnderFin = true;
        
    }
}
