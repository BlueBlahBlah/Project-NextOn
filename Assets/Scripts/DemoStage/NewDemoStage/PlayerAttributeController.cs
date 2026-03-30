using UnityEngine;
using System;

/// <summary>
/// [Controller] 플레이어의 체력, 상태 등 속성을 UI 레이어에 중계하는 컨트롤러
/// </summary>
public class PlayerAttributeController : MonoBehaviour
{
    // View(UI)가 구독할 수 있는 이벤트
    public event Action<float, float> OnHealthChanged;

    private void Awake()
    {
        // PlayerManager에 본인을 등록하거나 찾는 로직
        Debug.Log("<color=yellow>[Player-Controller]</color> Awake.");
    }

    private void Start()
    {
        // Model(PlayerManager)의 이벤트를 구독하여 View(UI)로 중계
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnHealthChanged += HandleHealthChanged;
            
            // 초기값 방송
            HandleHealthChanged(PlayerManager.Instance.Health, PlayerManager.Instance.TotalHealth);
        }
    }

    private void HandleHealthChanged(float current, float max)
    {
        // Model에서 받은 소식을 View(UI)들에게 전파 (Broadcast)
        OnHealthChanged?.Invoke(current, max);
    }

    private void OnDestroy()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.OnHealthChanged -= HandleHealthChanged;
    }
}
