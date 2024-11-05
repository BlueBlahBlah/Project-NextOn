using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private bool isStart = false;

    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject MazePlayerSpawnPoint;
    [SerializeField]
    private GameObject leaderMob;
    //[SerializeField]
    //private GameObject[] MazeSpawnPoint;
    [SerializeField]
    private GameObject[] MissionObject;
    [SerializeField]
    private GameObject StartTrigger;
    public int missionCount = 0;
    [SerializeField]
    private Transform MazePlayerSecondPoint;

    // Update is called once per frame
    void Start(){
        isStart = true;
        _dialogue.DialogueNumber = 0;
        StartCoroutine(MazeRoutine());
    }
    void Update()
    {
        if(missionCount == 3)
        {
            if(PoolManager.poolManager.GetAllPoolSetActive() == 0)
            {
                MazeStart.mazeStart.isMazeStart = false;
            }
        }
    }

    public void PrintDialogue_haram()
    {
        _dialogue.PrintDialogueByNumber(_dialogue.DialogueNumber);
    }

    public IEnumerator MazeRoutine(){
        //처음 시작하면 스폰포인트에 플레이어를 스폰한다
        //Player.transform.position = MazePlayerSpawnPoint.transform.position;
        yield return new WaitForSeconds(0.5f);
        //메이즈 시작 트리거를 지나갈 때까지 대기
        yield return new WaitUntil(() => MazeStart.mazeStart.isMazeStart);
       // PrintDialogue_haram();

        yield return new WaitUntil(() => missionCount == 1);
        
        Debug.Log("대사 이런, 유출된 메모리 틈으로 몬스터들이 침입했어, 몬스터를 처치하고 나머지 메모리도 복구해줘");
        yield return new WaitUntil(() => missionCount == 2);
        Debug.Log("마지막으로 하나만 남았어. 얼른 부탁할게");
        yield return new WaitUntil(() => missionCount == 3);
        Debug.Log("메모리를 모두 복구했구나. 남은 몬스터를 처치해줘?");

        yield return new WaitUntil(() => PoolManager.poolManager.GetAllPoolSetActive() == 0);
        MazeStart.mazeStart.isMazeStart = false; 

        //세 개의 메모리를 찾아올 때까지 대기
        yield return new WaitUntil(() => !MazeStart.mazeStart.isMazeStart);
        //찾은거 가지고 자신이 있는 곳으로 와달라는 대사 끝나면
        Debug.Log("스폰된 몬스터를 따라와줘 내가 키우는 몬스터야");
        GameObject leader = Instantiate(leaderMob,Player.transform.position, Player.transform.rotation);
        leader.GetComponent<LeaderMob>().SetTarget(GameObject.Find("PointtoDrop").transform);
        
        yield return new WaitUntil(() => MissionObject[3].GetComponent<FinishMaze>().isClose);
        Debug.Log("isclose");
        FirstLandManager.firstLandManager.isMazeFin = true;
    }
}
