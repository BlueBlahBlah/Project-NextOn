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

    // 단발성 사운드 재생 메서드 (SE 디렉토리) - 볼륨 설정 가능
    public void PlayEffectSound(string clipName, float volume = 1.0f)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/SE/" + clipName);
        if (clip != null)
        {
            AudioSource effectSource = gameObject.AddComponent<AudioSource>();
            effectSource.clip = clip;
            effectSource.volume = volume;  // 초기 볼륨 설정
            effectSource.Play();

            activeEffectsSources.Add(effectSource);
            originalVolumes[effectSource] = volume; // 원래 볼륨 저장

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

    // 효과음들의 볼륨을 비율에 따라 설정하는 메서드
    public void SetEffectsVolume(float scale)
    {
        scale = Mathf.Clamp(scale, 0f, 1f); // 비율 범위 제한 (0~1)

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
        // 오디오 소스가 더 이상 재생되지 않을 때까지 대기
        yield return new WaitUntil(() => source == null || !source.isPlaying);

        if (source != null) // 소스가 여전히 유효한지 확인
        {
            activeEffectsSources.Remove(source);
            originalVolumes.Remove(source);
            Destroy(source);
        }
    }

}