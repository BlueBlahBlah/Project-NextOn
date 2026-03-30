using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// [View] 컨트롤러의 방송을 받아서 화면에 탄약을 표시하는 수동형 UI Widget
/// </summary>
public class AmmoWidget : MonoBehaviour
{
    [SerializeField] private WeaponWidgetController controller;
    [SerializeField] private Text ammoText;             // Legacy Text용
    [SerializeField] private TextMeshProUGUI tmpText;   // TextMeshPro용

    private void Start()
    {
        if (controller == null)
            controller = FindObjectOfType<WeaponWidgetController>();

        if (controller != null)
        {
            // 컨트롤러의 방송(이벤트)을 구독 (Subscribe)
            controller.OnAmmoChanged += UpdateAmmoDisplay;
        }
    }

    private void UpdateAmmoDisplay(int current, int max)
    {
        // 컨트롤러가 줄 때만 화면을 갱신 (No Update Loop!)
        string displayStr = $"{current} / {max}";

        if (ammoText != null) ammoText.text = displayStr;
        if (tmpText != null) tmpText.text = displayStr;
        
        Debug.Log($"<color=green>[View]</color> UI Refreshed: {displayStr}");
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.OnAmmoChanged -= UpdateAmmoDisplay;
    }
}
