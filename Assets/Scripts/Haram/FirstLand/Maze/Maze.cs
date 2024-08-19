using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private bool isStart = false;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject MazePlayerSpawnPoint;
    [SerializeField]
    private GameObject[] MazeSpawnPoint;
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
            if(MazeGenerate.mazeGenerate.monsCount == 0)
            {
                MazeStart.mazeStart.isMazeStart = false;
            }
        }
    }

    IEnumerator MazeRoutine(){
        //처음 시작하면 스폰포인트에 플레이어를 스폰한다
        Player.transform.position = MazePlayerSpawnPoint.transform.position;
        yield return new WaitForSeconds(0.5f);
        //메이즈 시작 트리거를 지나갈 때까지 대기
        yield return new WaitUntil(() => MazeStart.mazeStart.isMazeStart);
        MazeStart.mazeStart.isMazeStart = false;

        //말풍선 영화를 봐야하는데 컴퓨터의 메모리가 유출됐으니 찾아달라는 대사

        //세 개의 메모리를 찾아올 때까지 대기
        yield return new WaitUntil(() => !MazeStart.mazeStart.isMazeStart);
        
        //찾은거 가지고 자신이 있는 곳으로 와달라는 대사 끝나면
        
        Player.transform.position = MazePlayerSecondPoint.transform.position;
    }
}
