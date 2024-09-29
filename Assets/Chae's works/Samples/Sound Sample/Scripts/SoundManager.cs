using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource effectsSource;
    public AudioSource musicSource;

    public bool isBgmMute;
    public bool isSEMute;

    void Awake()
    {
        // Singleton 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 단발성 사운드 재생 메서드 (SE 디렉토리)
    public void PlayEffectSound(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/SE/" + clipName);
        if (clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound effect clip not found: " + clipName);
        }
    }

    // 배경음악 재생 메서드 (BGM 디렉토리)
    public void PlayMusic(string clipName)
    {
        string path = "Sound/BGM/" + clipName;
        Debug.Log("Trying to load music clip from path: " + path);

        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            Debug.Log("Successfully loaded music clip: " + clipName);
            musicSource.clip = clip;
            musicSource.loop = true; // 반복 재생 설정
            musicSource.volume = Mathf.Clamp(musicSource.volume, 0f, 0.3f); // 볼륨 제한
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found at path: " + path);
        }
    }


    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void StopEffects()
    {
        effectsSource.Stop();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

