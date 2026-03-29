using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //?„ì²´ ê²Œì„ ?¸ë¦¬ê±?    private bool isGameStart;
    //ì²«ë²ˆì§????¸ë¦¬ê±?    private bool isFirstLandStart;
    //ì²«ë²ˆì§??¬ì˜ ë¯¸ë¡œ ?¸ë¦¬ê±?    private bool isMazeStart;
    //?ë²ˆì§????¸ë¦¬ê±?    private bool isSecondLandStart;
    //?ë²ˆì§???ë§ˆì?ë§?ë¯¸ì…˜ ?¸ë¦¬ê±?    private bool isLastMissionStart;

    void Awake()
    {
        isGameStart = true;
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
