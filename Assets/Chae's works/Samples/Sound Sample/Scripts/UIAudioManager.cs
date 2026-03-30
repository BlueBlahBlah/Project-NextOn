using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAudioManager : MonoBehaviour
{
    [SerializeField]
    private int uiType; // 0: Button, 1: Toggle, 2: Slider, 3: ScrollRect
    [SerializeField]
    private string soundName;

    private void Start()
    {
        switch (uiType)
        {
            case 0: // Button
                Button button = GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => PlaySound(soundName));
                    Debug.Log("Button 사운드 등록 완료");
                }
                else
                {
                    Debug.LogWarning("Button 컴포넌트가 없습니다.");
                }
                break;
            case 1: // Toggle
                Toggle toggle = GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("Toggle 사운드 등록 완료");
                }
                else
                {
                    Debug.LogWarning("Toggle 컴포넌트가 없습니다.");
                }
                break;
            case 2: // Slider
                Slider slider = GetComponent<Slider>();
                if (slider != null)
                {
                    slider.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("Slider 사운드 등록 완료");
                }
                else
                {
                    Debug.LogWarning("Slider 컴포넌트가 없습니다.");
                }
                break;
            case 3: // ScrollRect
                ScrollRect scrollRect = GetComponent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("ScrollRect 사운드 등록 완료");
                }
                else
                {
                    Debug.LogWarning("ScrollRect 컴포넌트가 없습니다.");
                }
                break;
            default:
                Debug.LogError("유효하지 않은 uiType입니다.");
                break;
        }
    }

    private void PlaySound(string soundFileName)
    {
        Debug.Log($"UI Sound Name : {soundFileName} ");
        SoundManager.instance.PlayEffectSound("UISound/" + soundFileName, 1f);
    }

}