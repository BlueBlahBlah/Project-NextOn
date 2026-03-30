using UnityEngine;
using System;

/// <summary>
/// [Model] 체력 데이터를 관리하고 변경 사항을 방송하는 독립 컴포넌트
/// 플레이어와 몬스터 모두에서 공용으로 사용할 수 있습니다.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // 데이터가 변경될 때 방송하는 이벤트 (Current, Max)
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    public float CurrentHealth 
    { 
        get => currentHealth; 
        private set 
        {
            float prev = currentHealth;
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            
            if (Mathf.Abs(prev - currentHealth) > 0.001f)
            {
                OnHealthChanged?.Invoke(currentHealth, maxHealth);
                if (currentHealth <= 0) OnDeath?.Invoke();
            }
        }
    }

    public float MaxHealth 
    { 
        get => maxHealth; 
        set 
        {
            maxHealth = value;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Initialize(float initialMaxHealth)
    {
        maxHealth = initialMaxHealth;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0) return;
        CurrentHealth -= amount;
        Debug.Log($"<color=red>[Health]</color> {gameObject.name} took {amount} damage. (HP: {currentHealth})");
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;
        CurrentHealth += amount;
        Debug.Log($"<color=green>[Health]</color> {gameObject.name} healed {amount}. (HP: {currentHealth})");
    }

    // 하위 호환성을 위해 직접 값 설정 허용 (사용 자제 권장)
    public void SetHealthManual(float value)
    {
        CurrentHealth = value;
    }
}
