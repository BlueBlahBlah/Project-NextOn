using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //전체 게임 트리거
    private bool isGameStart;
    //첫번째 섬 트리거
    private bool isFirstLandStart;
    //첫번째 섬의 미로 트리거
    private bool isMazeStart;
    //두번째 섬 트리거
    private bool isSecondLandStart;
    //두번째 섬 마지막 미션 트리거
    private bool isLastMissionStart;

    void Awake()
    {
        isGameStart = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetisGameStart(){
        return isGameStart;
    }
    public void SetisGameStart(bool set){
        isGameStart = set;
    }
    public bool GetisFirstLandStart(){
        return isFirstLandStart;
    }
    public void SetisFirstLandStart(bool set){
        isFirstLandStart = set;
    }
    public bool GetisMazeStart(){
        return isMazeStart;
    }
    public void SetisMazeStart(bool set){
        isMazeStart = set;
    }
    public bool GetisSecondLandStart(){
        return isSecondLandStart;      
    }
    public void SetisSecondLandStart(bool set){
        isSecondLandStart = set;
    }
    public bool GetisLastMissionStart(){
        return isLastMissionStart;
    }
    public void SetisLastMissionStart(bool set){
        isLastMissionStart = set;
    }
}
