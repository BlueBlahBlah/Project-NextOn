using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private bool isStart = false;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject MazePlayerSpawnPoint;
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
        Debug.Log("대사 영화를 봐야하는데 메모리가 유출됐어. 메모리가 유출된 곳이 세 군데인데 고쳐줄 수 있겠어?");
        yield return new WaitUntil(() => missionCount == 1);
        Debug.Log("대사 이런, 유출된 메모리 틈으로 몬스터들이 침입했어, 몬스터를 처치하고 나머지 메모리도 복구해줘");
        yield return new WaitUntil(() => missionCount == 2);
        Debug.Log("마지막으로 하나만 남았어. 얼른 부탁할게");
        yield return new WaitUntil(() => missionCount == 3);
        Debug.Log("메모리를 모두 복구했구나. 맵에 있는 나만의 영화관에 찾아와주겠어?");
        Debug.Log("지금 몬스터들이 내 영화관 근처에도 침입해서 움직일 수가 없어..");
    
        MazeStart.mazeStart.isMazeStart = false; 

        //말풍선 영화를 봐야하는데 컴퓨터의 메모리가 유출됐으니 찾아달라는 대사

        //세 개의 메모리를 찾아올 때까지 대기
        yield return new WaitUntil(() => !MazeStart.mazeStart.isMazeStart);
        
        //찾은거 가지고 자신이 있는 곳으로 와달라는 대사 끝나면
        
        Player.transform.position = MazePlayerSecondPoint.transform.position;
    }
}
