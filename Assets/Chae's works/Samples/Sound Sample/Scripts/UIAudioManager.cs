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
                    Debug.Log("Button ���� ��� �Ϸ�");
                }
                else
                {
                    Debug.LogWarning("Button ������Ʈ�� �����ϴ�.");
                }
                break;
            case 1: // Toggle
                Toggle toggle = GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("Toggle ���� ��� �Ϸ�");
                }
                else
                {
                    Debug.LogWarning("Toggle ������Ʈ�� �����ϴ�.");
                }
                break;
            case 2: // Slider
                Slider slider = GetComponent<Slider>();
                if (slider != null)
                {
                    slider.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("Slider ���� ��� �Ϸ�");
                }
                else
                {
                    Debug.LogWarning("Slider ������Ʈ�� �����ϴ�.");
                }
                break;
            case 3: // ScrollRect
                ScrollRect scrollRect = GetComponent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.onValueChanged.AddListener((value) => PlaySound(soundName));
                    Debug.Log("ScrollRect ���� ��� �Ϸ�");
                }
                else
                {
                    Debug.LogWarning("ScrollRect ������Ʈ�� �����ϴ�.");
                }
                break;
            default:
                Debug.LogError("��ȿ���� ���� uiType�Դϴ�.");
                break;
        }
    }

    private void PlaySound(string soundFileName)
    {
        Debug.Log($"UI Sound Name : {soundFileName} ");
        SoundManager.instance.PlayEffectSound("UISound/" + soundFileName, 1f);
    }



    /*
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
            Debug.Log("Add Buttons");
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
            Debug.Log("Click Sound Trigger");
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
    */
}