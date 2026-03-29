using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBtn : MonoBehaviour
{
    public bool BtnDown;

    public void PointerDown()
    {
        BtnDown = true;
        SetInputControllerFiring(true);
    }
    
    public void PointerUp()
    {
        BtnDown = false;
        SetInputControllerFiring(false);
    }

    private void SetInputControllerFiring(bool firing)
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.inputController != null)
        {
            PlayerManager.Instance.inputController.SetFiring(firing);
            // 만약 단발 마우스 클릭 느낌을 주려면 Down시에만 트리거 호출 가능
            if (firing) PlayerManager.Instance.inputController.TriggerFirePressed();
        }
    }
}
