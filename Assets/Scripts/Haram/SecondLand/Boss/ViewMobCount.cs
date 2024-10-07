using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMobCount : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<RamBossScenario>().isBossStart && !this.GetComponent<RamBossStageManager>().isWaitForNextStage && !this.GetComponent<RamBossStageManager>().isStageRestart)
            this.GetComponent<RamBossStageManager>().ViewMobCount();
    }
}
