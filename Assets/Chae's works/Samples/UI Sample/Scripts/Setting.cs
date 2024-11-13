using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Scrollbar BgmScrollBar;
    [SerializeField]
    private Scrollbar SEScrollBar;

    void Start()
    {
        // �� �ε� �Ǵ� UI Ȱ��ȭ �� ��ũ�ѹ��� �ʱ� ��ġ�� ����
        InitializeScrollBars();
    }

    void OnEnable()
    {
        // UI�� �ٽ� Ȱ��ȭ�� �� ��ũ�ѹ��� �ʱ� ��ġ�� ����
        InitializeScrollBars();
    }

    // BGM ���� ���� �޼���
    public void SetBgmVolume()
    {
        if (0 <= BgmScrollBar.value && BgmScrollBar.value <= 0.125f)
        {
            SoundManager.instance.SetMusicVolume(0.06f);
        }
        else if (BgmScrollBar.value <= 0.375f)
        {
            SoundManager.instance.SetMusicVolume(0.12f);
        }
        else if (BgmScrollBar.value <= 0.625f)
        {
            SoundManager.instance.SetMusicVolume(0.18f);
        }
        else if (BgmScrollBar.value <= 0.875f)
        {
            SoundManager.instance.SetMusicVolume(0.24f);
        }
        else
        {
            SoundManager.instance.SetMusicVolume(0.3f);
        }
    }

    // BGM ���Ұ� ���
    public void SetBgmMute()
    {
        if (!SoundManager.instance.isBgmMute)
        {
            BgmScrollBar.interactable = false;
            SoundManager.instance.SetMusicVolume(0f);  // ���Ұ� �� ������ 0���� ����
            SoundManager.instance.isBgmMute = true;
        }
        else
        {
            BgmScrollBar.interactable = true;

            // ���Ұ� ���� �� ������ ��ũ�ѹ� ������ ����
            float currentVolume = BgmScrollBar.value;  // ��ũ�ѹ� �� ����
            if (currentVolume <= 0.125f)
            {
                SoundManager.instance.SetMusicVolume(0.06f);
            }
            else if (currentVolume <= 0.375f)
            {
                SoundManager.instance.SetMusicVolume(0.12f);
            }
            else if (currentVolume <= 0.625f)
            {
                SoundManager.instance.SetMusicVolume(0.18f);
            }
            else if (currentVolume <= 0.875f)
            {
                SoundManager.instance.SetMusicVolume(0.24f);
            }
            else
            {
                SoundManager.instance.SetMusicVolume(0.3f);
            }

            SoundManager.instance.isBgmMute = false;
        }
    }

    // SE ���� ���� �޼���
    public void SetSEVolume()
    {
        // SE ������ Scrollbar ���� ���� ����
        if (0 <= SEScrollBar.value && SEScrollBar.value <= 0.125f)
        {
            SoundManager.instance.SetEffectsVolume(0.2f);
        }
        else if (SEScrollBar.value <= 0.375f)
        {
            SoundManager.instance.SetEffectsVolume(0.4f);
        }
        else if (SEScrollBar.value <= 0.625f)
        {
            SoundManager.instance.SetEffectsVolume(0.6f);
        }
        else if (SEScrollBar.value <= 0.875f)
        {
            SoundManager.instance.SetEffectsVolume(0.8f);
        }
        else
        {
            SoundManager.instance.SetEffectsVolume(1f);
        }
    }

    // SE ���Ұ� ���
    public void SetSEMute()
    {
        if (!SoundManager.instance.isSEMute)
        {
            SEScrollBar.interactable = false;
            SoundManager.instance.SetEffectsVolume(0f);
            SoundManager.instance.isSEMute = true;
        }
        else
        {
            SEScrollBar.interactable = true;

            // SE ���Ұ� ���� �� ������ ��ũ�ѹ� ������ ����
            float currentVolume = SEScrollBar.value;  // ��ũ�ѹ� �� ����
            if (currentVolume <= 0.125f)
            {
                SoundManager.instance.SetEffectsVolume(0.2f);
            }
            else if (currentVolume <= 0.375f)
            {
                SoundManager.instance.SetEffectsVolume(0.4f);
            }
            else if (currentVolume <= 0.625f)
            {
                SoundManager.instance.SetEffectsVolume(0.6f);
            }
            else if (currentVolume <= 0.875f)
            {
                SoundManager.instance.SetEffectsVolume(0.8f);
            }
            else
            {
                SoundManager.instance.SetEffectsVolume(1f);
            }

            SoundManager.instance.isSEMute = false;
        }
    }

    // ��ũ�ѹ� �ʱ�ȭ �޼���
    private void InitializeScrollBars()
    {
        // BGM Scrollbar �ʱ�ȭ

        if (SoundManager.instance.isBgmMute)
        {
            BgmScrollBar.interactable = false;
            BgmScrollBar.value = 0f;  // ���Ұ� ���¿����� ��ũ�ѹ� ���� 0���� ����
            SoundManager.instance.SetMusicVolume(0f);
        }
        else
        {
            BgmScrollBar.interactable = true;
            float bgmVolume = SoundManager.instance.GetMusicVolume();
            if (bgmVolume <= 0.06f)
            {
                BgmScrollBar.value = 0.0f;
            }
            else if (bgmVolume <= 0.12f)
            {
                BgmScrollBar.value = 0.25f;
            }
            else if (bgmVolume <= 0.18f)
            {
                BgmScrollBar.value = 0.5f;
            }
            else if (bgmVolume <= 0.24f)
            {
                BgmScrollBar.value = 0.75f;
            }
            else
            {
                BgmScrollBar.value = 1.0f;
            }
        }


        // SE Scrollbar �ʱ�ȭ

        if (SoundManager.instance.isSEMute)
        {
            SEScrollBar.interactable = false;
            SEScrollBar.value = 0f;  // ���Ұ� ���¿����� ��ũ�ѹ� ���� 0���� ����
            SoundManager.instance.SetEffectsVolume(0f);
        }
        else
        {
            SEScrollBar.interactable = true;
            float seVolume = SoundManager.instance.GetEffectsVolume();
            if (seVolume <= 0.2f)
            {
                SEScrollBar.value = 0.0f;
            }
            else if (seVolume <= 0.4f)
            {
                SEScrollBar.value = 0.25f;
            }
            else if (seVolume <= 0.6f)
            {
                SEScrollBar.value = 0.5f;
            }
            else if (seVolume <= 0.8f)
            {
                SEScrollBar.value = 0.75f;
            }
            else
            {
                SEScrollBar.value = 1.0f;
            }
        }
        
    }
}
