using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public float TotalHealth;                          
    public float Health;                               
    public float HealthGen;                            
    private float curHealth;                               
    [SerializeField] private TextMeshPro damaged;
    public Image hpBar;

    private bool updateStart = false;

    private void Start()
    {
        InitializeAsync();
    }

    private async void InitializeAsync() 
    {
        // 컨트롤러를 찾을 때까지 대기 (혹은 자동 할당 로직)
        PlayerAttributeController controller = null;
        while (controller == null)
        {
            controller = FindObjectOfType<PlayerAttributeController>();
            if (controller == null) await System.Threading.Tasks.Task.Delay(100);
        }

        // 초기화 및 이벤트 구독 (Subscribe)
        controller.OnHealthChanged += HandleHealthChanged;
        
        Initialize();
    }

    private void Initialize()       
    {
        if (PlayerManager.Instance != null)
        {
            Health = PlayerManager.Instance.Health;
            curHealth = Health;
            TotalHealth = PlayerManager.Instance.TotalHealth;
        }
        
        damaged.SetText("");  
        InitHPBarSize();  
        updateStart = true;
    }

    private void HandleHealthChanged(float newHealth, float maxHealth)
    {
        if (!updateStart) return;

        float prevHealth = Health;
        Health = newHealth;
        TotalHealth = maxHealth;

        // 데미지 연출 로직 (MVC View의 역할)
        if (Health < prevHealth && PlayerManager.Instance.Death == false)
        {
            float damageDone = prevHealth - Health;
            ShowDamage(damageDone);
        }

        // UI 바 업데이트
        if (maxHealth > 0)
        {
            hpBar.rectTransform.localScale = new Vector3(Health / maxHealth, 1f, 1f);
        }

        Debug.Log($"<color=green>[Player-View]</color> UI Refreshed: {Health} / {maxHealth}");
    }

    // [DEPRECATED] 더 이상 매 프레임 폴링하지 않습니다.
    // void Update() { ... }
    
    private void ShowDamage(float d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0,3.5f,0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
    }
    
    void InitHPBarSize()
    {
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void UpdateHealthInfo()     
    {
        Health = PlayerManager.Instance.Health;     
        HealthGen = PlayerManager.Instance.HealthGen;
        curHealth = Health;
        updateStart = true;
    }
    
}
