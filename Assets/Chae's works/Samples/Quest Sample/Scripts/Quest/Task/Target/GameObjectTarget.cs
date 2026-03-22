using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]

public class GameObjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject value;

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        // GameObject 비교
        var targetAsGameObject = target as GameObject;
        if (targetAsGameObject == null)
            return false;
        // 씬 내의 GameObject 가 해당 이름을 '포함' 하는지 비교 (넘버 혹은 clone 경우 고려)
        return targetAsGameObject.name.Contains(value.name);
    }
}
