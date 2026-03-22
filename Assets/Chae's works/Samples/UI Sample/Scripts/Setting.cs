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
        // 씬 로드 또는 UI 활성화 시 스크롤바의 초기 위치를 설정
        InitializeScrollBars();
    }

    void OnEnable()
    {
        // UI가 다시 활성화될 때 스크롤바의 초기 위치를 설정
        InitializeScrollBars();
    }

    // BGM 볼륨 설정 메서드
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

    // BGM 음소거 토글
    public void SetBgmMute()
    {
        if (!SoundManager.instance.isBgmMute)
        {
            BgmScrollBar.interactable = false;
            SoundManager.instance.SetMusicVolume(0f);  // 음소거 시 볼륨을 0으로 설정
            SoundManager.instance.isBgmMute = true;
        }
        else
        {
            BgmScrollBar.interactable = true;

            // 음소거 해제 후 볼륨을 스크롤바 값으로 설정
            float currentVolume = BgmScrollBar.value;  // 스크롤바 값 저장
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

    // SE 볼륨 설정 메서드
    public void SetSEVolume()
    {
        // SE 볼륨을 Scrollbar 값에 따라 설정
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

    // SE 음소거 토글
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

            // SE 음소거 해제 후 볼륨을 스크롤바 값으로 설정
            float currentVolume = SEScrollBar.value;  // 스크롤바 값 저장
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

    // 스크롤바 초기화 메서드
    private void InitializeScrollBars()
    {
        // BGM Scrollbar 초기화

        if (SoundManager.instance.isBgmMute)
        {
            BgmScrollBar.interactable = false;
            BgmScrollBar.value = 0f;  // 음소거 상태에서는 스크롤바 값을 0으로 설정
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


        // SE Scrollbar 초기화

        if (SoundManager.instance.isSEMute)
        {
            SEScrollBar.interactable = false;
            SEScrollBar.value = 0f;  // 음소거 상태에서는 스크롤바 값을 0으로 설정
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
