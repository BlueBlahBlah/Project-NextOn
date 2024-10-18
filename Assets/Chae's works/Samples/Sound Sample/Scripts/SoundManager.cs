using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource;

    public bool isBgmMute;
    public bool isSEMute;

    private List<AudioSource> activeEffectsSources = new List<AudioSource>();
    private Dictionary<AudioSource, float> originalVolumes = new Dictionary<AudioSource, float>();

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

    // 단발성 효과음 (SE) 재생 메서드
    public void PlayEffectSound(string clipName, float volume = 1.0f)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/SE/" + clipName);
        if (clip != null)
        {
            AudioSource effectSource = gameObject.AddComponent<AudioSource>();
            effectSource.clip = clip;
            effectSource.volume = volume; 
            effectSource.Play();

            activeEffectsSources.Add(effectSource);
            originalVolumes[effectSource] = volume; 

            StartCoroutine(RemoveSourceWhenDone(effectSource));
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

    // 단발성 효과음 설정 메서드
    public void SetEffectsVolume(float scale)
    {
        scale = Mathf.Clamp(scale, 0f, 1f); // 볼륨의 범위 지정

        foreach (AudioSource source in activeEffectsSources)
        {
            if (originalVolumes.TryGetValue(source, out float originalVolume))
            {
                source.volume = originalVolume * scale;
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void StopEffects()
    {
        foreach (AudioSource source in activeEffectsSources)
        {
            source.Stop();
            Destroy(source);
        }
        activeEffectsSources.Clear();
        originalVolumes.Clear();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    private IEnumerator RemoveSourceWhenDone(AudioSource source)
    {
        yield return new WaitUntil(() => source == null || !source.isPlaying);

        if (source != null) 
        {
            activeEffectsSources.Remove(source);
            originalVolumes.Remove(source);
            Destroy(source);
        }
    }

}