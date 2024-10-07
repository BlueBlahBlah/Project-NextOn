using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButton : MonoBehaviour
{
    private bool isComplete;
    public void MissionComplete()
    {
        this.gameObject.SetActive(false);
        isComplete = true;
        UnderMaze.underMaze.missionCount++;
    }
}
