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


    public IEnumerator MazeRoutine(){
        //처음 시작하면 스폰포인트에 플레이어를 스폰한다
        //Player.transform.position = MazePlayerSpawnPoint.transform.position;
        yield return new WaitForSeconds(0.5f);
        //메이즈 시작 트리거를 지나갈 때까지 대기
        UIManager.instance.DialogueEventByNumber(_dialogue, 120);
        yield return new WaitUntil(() => MazeStart.mazeStart.isMazeStart);

        yield return new WaitUntil(() => missionCount == 1);
        UIManager.instance.DialogueEventByNumber(_dialogue, 129);
        yield return new WaitUntil(() => missionCount == 2);
        UIManager.instance.DialogueEventByNumber(_dialogue, 132);
        yield return new WaitUntil(() => missionCount == 3);

        yield return new WaitUntil(() => PoolManager.poolManager.GetAllPoolSetActive() == 0);
        UIManager.instance.DialogueEventByNumber(_dialogue, 133);
        MazeStart.mazeStart.isMazeStart = false; 

        yield return new WaitUntil(() => !MazeStart.mazeStart.isMazeStart);
        GameObject leader = Instantiate(leaderMob,Player.transform.position, Player.transform.rotation);
        leader.GetComponent<LeaderMob>().SetTarget(GameObject.Find("PointtoDrop").transform);
        
        yield return new WaitUntil(() => MissionObject[3].GetComponent<FinishMaze>().isClose);
        Debug.Log("isclose");
        FirstLandManager.firstLandManager.isMazeFin = true;
    }
}
