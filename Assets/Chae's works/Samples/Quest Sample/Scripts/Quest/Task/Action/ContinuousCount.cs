using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/ContinuousCount", fileName = "ContinuousCount")]
public class ContinuousCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return currentSuccess > 0 ? currentSuccess + successCount : 0;
    }
}
