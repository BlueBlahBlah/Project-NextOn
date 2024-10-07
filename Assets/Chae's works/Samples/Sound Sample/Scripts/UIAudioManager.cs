using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAudioManager : MonoBehaviour
{
    // ���� Ŭ���� �Ҵ��� �� �ִ� ���� ����
    [Header("UI Interaction Sounds")]
    public string buttonClickSound = "ButtonClick";
    public string toggleOnSound = "ToggleOn";
    public string toggleOffSound = "ToggleOff";
    public string sliderMoveSound = "SliderMove";
    public string scrollSound = "Scroll";

    // �ʱ�ȭ �� UI ����� �̺�Ʈ�� �����ϴ� �޼���
    private void Start()
    {
        // ��� Button ������Ʈ ã��
        Button[] buttons = GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonClickSound());
        }

        // ��� Toggle ������Ʈ ã��
        Toggle[] toggles = GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((isOn) => PlayToggleSound(isOn));
        }

        // ��� Slider ������Ʈ ã��
        Slider[] sliders = GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            // �����̴� ���� ����� ������ ���� ���
            slider.onValueChanged.AddListener((value) => PlaySliderSound());
        }

        // ��� ScrollRect ������Ʈ ã��
        ScrollRect[] scrollRects = GetComponentsInChildren<ScrollRect>(true);
        foreach (ScrollRect scrollRect in scrollRects)
        {
            // ScrollRect�� OnValueChanged �̺�Ʈ�� ���� ���
            scrollRect.onValueChanged.AddListener((value) => PlayScrollSound());
        }

        // ��Ÿ UI ��ҵ鿡 ���� �߰����� �̺�Ʈ ������ ���⿡ �߰��� �� �ֽ��ϴ�.
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void PlayButtonClickSound()
    {
        if (!string.IsNullOrEmpty(buttonClickSound))
        {
            SoundManager.instance.PlayEffectSound(buttonClickSound, 1f);
        }
    }

    // ��� ���� �� ȣ��Ǵ� �޼���
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

    // �����̴� ���� �� ȣ��Ǵ� �޼���
    private void PlaySliderSound()
    {
        if (!string.IsNullOrEmpty(sliderMoveSound))
        {
            SoundManager.instance.PlayEffectSound(sliderMoveSound, 0.5f); // �����̴��� ���� ����ǹǷ� ������ ����
        }
    }

    // ��ũ�� ���� �� ȣ��Ǵ� �޼���
    private void PlayScrollSound()
    {
        if (!string.IsNullOrEmpty(scrollSound))
        {
            SoundManager.instance.PlayEffectSound(scrollSound, 0.5f); // ��ũ�ѵ� ���� ����ǹǷ� ������ ����
        }
    }

    // �ʿ信 ���� �߰����� UI ��ȣ�ۿ� �̺�Ʈ �ڵ鷯�� ������ �� �ֽ��ϴ�.
}