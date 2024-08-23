using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAudioManager : MonoBehaviour
{
    // 사운드 클립을 할당할 수 있는 공개 변수
    [Header("UI Interaction Sounds")]
    public string buttonClickSound = "ButtonClick";
    public string toggleOnSound = "ToggleOn";
    public string toggleOffSound = "ToggleOff";
    public string sliderMoveSound = "SliderMove";
    public string scrollSound = "Scroll";

    // 초기화 시 UI 요소의 이벤트에 구독하는 메서드
    private void Start()
    {
        // 모든 Button 컴포넌트 찾기
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonClickSound());
        }

        // 모든 Toggle 컴포넌트 찾기
        Toggle[] toggles = GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((isOn) => PlayToggleSound(isOn));
        }

        // 모든 Slider 컴포넌트 찾기
        Slider[] sliders = GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            // 슬라이더 값이 변경될 때마다 사운드 재생
            slider.onValueChanged.AddListener((value) => PlaySliderSound());
        }

        // 모든 ScrollRect 컴포넌트 찾기
        ScrollRect[] scrollRects = GetComponentsInChildren<ScrollRect>(true);
        foreach (ScrollRect scrollRect in scrollRects)
        {
            // ScrollRect의 OnValueChanged 이벤트에 사운드 재생
            scrollRect.onValueChanged.AddListener((value) => PlayScrollSound());
        }

        // 기타 UI 요소들에 대한 추가적인 이벤트 구독은 여기에 추가할 수 있습니다.
    }

    // 버튼 클릭 시 호출되는 메서드
    private void PlayButtonClickSound()
    {
        if (!string.IsNullOrEmpty(buttonClickSound))
        {
            SoundManager.instance.PlayEffectSound(buttonClickSound, 1f);
        }
    }

    // 토글 변경 시 호출되는 메서드
    private void PlayToggleSound(bool isOn)
    {
        if (isOn)
        {
            if (!string.IsNullOrEmpty(toggleOnSound))
            {
                SoundManager.instance.PlayEffectSound(toggleOnSound, 1f);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(toggleOffSound))
            {
                SoundManager.instance.PlayEffectSound(toggleOffSound, 1f);
            }
        }
    }

    // 슬라이더 조절 시 호출되는 메서드
    private void PlaySliderSound()
    {
        if (!string.IsNullOrEmpty(sliderMoveSound))
        {
            SoundManager.instance.PlayEffectSound(sliderMoveSound, 0.5f); // 슬라이더는 자주 변경되므로 볼륨을 낮춤
        }
    }

    // 스크롤 조절 시 호출되는 메서드
    private void PlayScrollSound()
    {
        if (!string.IsNullOrEmpty(scrollSound))
        {
            SoundManager.instance.PlayEffectSound(scrollSound, 0.5f); // 스크롤도 자주 변경되므로 볼륨을 낮춤
        }
    }

    // 필요에 따라 추가적인 UI 상호작용 이벤트 핸들러를 구현할 수 있습니다.
}