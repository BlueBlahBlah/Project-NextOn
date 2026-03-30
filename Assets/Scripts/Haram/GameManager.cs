using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameStart;
    private bool isFirstLandStart;
    private bool isMazeStart;
    private bool isSecondLandStart;
    private bool isLastMissionStart;

    void Awake()
    {
        isGameStart = true;
    }

    public bool GetisGameStart(){ return isGameStart; }
    public void SetisGameStart(bool set){ isGameStart = set; }
    
    public bool GetisFirstLandStart(){ return isFirstLandStart; }
    public void SetisFirstLandStart(bool set){ isFirstLandStart = set; }
    
    public bool GetisMazeStart(){ return isMazeStart; }
    public void SetisMazeStart(bool set){ isMazeStart = set; }
    
    public bool GetisSecondLandStart(){ return isSecondLandStart; }
    public void SetisSecondLandStart(bool set){ isSecondLandStart = set; }
    
    public bool GetisLastMissionStart(){ return isLastMissionStart; }
    public void SetisLastMissionStart(bool set){ isLastMissionStart = set; }
}
