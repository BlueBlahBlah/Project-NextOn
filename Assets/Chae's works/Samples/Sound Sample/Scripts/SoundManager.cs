using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource;

    public bool isBgmMute;
    public bool isSEMute;

    [SerializeField]
    private float volumeBGM = 0.3f;
    [SerializeField]
    private float volumeSE = 1f;

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
            effectSource.volume = volume * volumeSE; // 입력된 volume과 volumeSE의 곱으로 볼륨 설정
            effectSource.Play();

            activeEffectsSources.Add(effectSource);
            originalVolumes[effectSource] = volume;  // 원래의 입력 volume 값을 저장

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
        volumeSE = Mathf.Clamp(scale, 0f, 1f); // volumeSE 값을 업데이트

        // 이미 재생 중인 효과음들의 볼륨을 갱신
        foreach (AudioSource source in activeEffectsSources)
        {
            if (originalVolumes.TryGetValue(source, out float originalVolume))
            {
                source.volume = originalVolume * volumeSE; // 원래 volume과 새로운 volumeSE의 곱으로 설정
            }
        }
    }

    public float GetEffectsVolume()
    {
        return this.volumeSE;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        volumeBGM = volume;
    }

    public float GetMusicVolume()
    {
        return this.volumeBGM;
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