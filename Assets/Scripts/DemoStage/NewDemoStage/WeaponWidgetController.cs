using UnityEngine;
using System;

/// <summary>
/// [Controller] 무기와 UI 사이의 가교 역할을 하는 언리얼 스타일 WidgetController
/// </summary>
public class WeaponWidgetController : MonoBehaviour
{
    private GunBase _boundGun;

    // View(UI)가 구독할 수 있는 이벤트
    public event Action<int, int> OnAmmoChanged;

    private void Awake()
    {
        Debug.Log("<color=yellow>[Controller]</color> WeaponWidgetController Awake (Script is Live).");
    }

    private void Update()
    {
        // 실시간으로 총기 교체 등에 대응하기 위해 캐싱 확인
        SyncWeaponReference();
    }

    private void SyncWeaponReference()
    {
        if (PlayerManager.Instance == null || PlayerManager.Instance.player_LongWeapon == null)
        {
            // Debug.LogWarning("<color=yellow>[Controller]</color> PlayerManager or player_LongWeapon is null.");
            return;
        }

        var rifleComponent = PlayerManager.Instance.player_LongWeapon.GetComponent<PlayerScriptRifle>();
        if (rifleComponent == null) return;

        var currentGun = rifleComponent.currentGun;
        
        if (_boundGun != currentGun)
        {
            // 기존 구독 해제
            if (_boundGun != null)
                _boundGun.OnAmmoChanged -= HandleAmmoChanged;

            _boundGun = currentGun;

            // 새 무기 구독 (Broadcast 연결)
            if (_boundGun != null)
            {
                _boundGun.OnAmmoChanged += HandleAmmoChanged;
                // 초기 값 전송
                HandleAmmoChanged(_boundGun.bulletCount, _boundGun.maxBulletCount);
            }
        }
    }

    private void HandleAmmoChanged(int current, int max)
    {
        Debug.Log($"<color=yellow>[Controller]</color> Received Model Broadcast -> Current: {current}, Max: {max}");
        
        // 1. [New MVC] View(UI)에게 방송하여 신규 UI 위젯 갱신
        OnAmmoChanged?.Invoke(current, max);

        // 2. [Legacy Support] 기존 PlayerManager 필드도 동기화
        // 이렇게 하면 기존 UIManager나 PlayerScriptRifle의 BulletInfo를 쓰던 UI들이 모두 정상 작동하게 됩니다.
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.TotalBullet = max;
            PlayerManager.Instance.CurrentBullet = current;
        }
    }

    private void OnDestroy()
    {
        if (_boundGun != null)
            _boundGun.OnAmmoChanged -= HandleAmmoChanged;
    }
}
