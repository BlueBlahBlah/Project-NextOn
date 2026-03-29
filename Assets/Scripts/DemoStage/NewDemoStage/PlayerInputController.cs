using System;
using UnityEngine;

/// <summary>
/// 플레이어의 모든 입력을 중앙에서 관리하는 컨트롤러입니다.
/// PC(키보드/마우스) 입력을 직접 감지하거나, UI 버튼으로부터 입력을 전달받습니다.
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    [Header("Input States")]
    public bool IsFiring { get; private set; }
    
    // 이벤트 기반 통신을 위한 액션들
    public event Action OnReload;
    public event Action OnFirePressed; // 단발성 공격 (근접 공격 등)
    public event Action<int> OnWeaponSwap; // 1, 2번 키 등

    private void Update()
    {
        // 1. PC 입력 감지 (마우스 왼쪽 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            SetFiring(true);
            OnFirePressed?.Invoke();
        }
        if (Input.GetMouseButtonUp(0)) SetFiring(false);

        // 2. 재장전 입력 (R 키)
        if (Input.GetKeyDown(KeyCode.R))
        {
            TriggerReload();
        }

        // 3. 무기 스왑 입력 (숫자 키)
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnWeaponSwap?.Invoke(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) OnWeaponSwap?.Invoke(1);
    }

    /// <summary>
    /// UI 버튼(FireBtn 등)에서 호출하여 사격 상태를 설정합니다.
    /// </summary>
    public void SetFiring(bool firing)
    {
        IsFiring = firing;
    }

    /// <summary>
    /// 단발성 공격 입력을 외부(UI 버튼 등)에서 트리거합니다.
    /// </summary>
    public void TriggerFirePressed()
    {
        OnFirePressed?.Invoke();
    }

    /// <summary>
    /// UI 버튼(ReloadBtn 등)에서 호출하여 재장전을 트리거합니다.
    /// </summary>
    public void TriggerReload()
    {
        OnReload?.Invoke();
    }
}
