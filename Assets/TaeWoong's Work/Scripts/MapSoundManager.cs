using System.Collections.Generic;
using UnityEngine;

public class MapSoundManager : MonoBehaviour
{
    public static MapSoundManager Instance = null;

    // 오디오 클립
    public AudioClip BGM_Sci_Fi_Map;
    public AudioClip StartProgress;
    public AudioClip EndProgress;
    public AudioClip Unlock;
    public AudioClip Die_Mob;
    public AudioClip Die_Boss;
    public AudioClip Golem_Attack;
    public AudioClip Spider_Attack;
    public AudioClip Turtle_Attack;
    public AudioClip Summon_Mob;
    public AudioClip Summon_Meteor;
    public AudioClip Summon_Boom;

    // 오디오 소스 관리용 딕셔너리
    private Dictionary<AudioClip, AudioSource> audioSources = new Dictionary<AudioClip, AudioSource>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 오디오 클립들을 로드합니다.
        LoadAudioClips();

        // 각 오디오 클립마다 AudioSource를 생성하여 저장
        CreateAudioSources();
    }

    private void LoadAudioClips()
    {
        BGM_Sci_Fi_Map = Resources.Load<AudioClip>("Sound/BGM/4 - Celestial Echoes (Loop)");
        StartProgress = Resources.Load<AudioClip>("Sound/SE/활성화Start");
        EndProgress = Resources.Load<AudioClip>("Sound/SE/활설화End");
        Die_Mob = Resources.Load<AudioClip>("Sound/SE/골렘거미터틀사망");
        Die_Boss = Resources.Load<AudioClip>("Sound/SE/보스사망");
        Unlock = Resources.Load<AudioClip>("Sound/SE/Area2_보안해제");
        Golem_Attack = Resources.Load<AudioClip>("Sound/SE/골렘공격");
        Spider_Attack = Resources.Load<AudioClip>("Sound/SE/거미공격");
        Turtle_Attack = Resources.Load<AudioClip>("Sound/SE/터틀공격");
        Summon_Mob = Resources.Load<AudioClip>("Sound/SE/몬스터 소환");
        Summon_Meteor = Resources.Load<AudioClip>("Sound/SE/보스메테오");
        Summon_Boom = Resources.Load<AudioClip>("Sound/SE/에너지폭발");
    }
    private void CreateAudioSources()
    {
        CreateAudioSource(BGM_Sci_Fi_Map);
        CreateAudioSource(StartProgress);
        CreateAudioSource(EndProgress);
        CreateAudioSource(Die_Mob);
        CreateAudioSource(Die_Boss);
        CreateAudioSource(Unlock);
        CreateAudioSource(Golem_Attack);       
        CreateAudioSource(Spider_Attack);
        CreateAudioSource(Turtle_Attack);
        CreateAudioSource(Summon_Mob);
        CreateAudioSource(Summon_Meteor);
        CreateAudioSource(Summon_Boom);
    }
    // 오디오 클립마다 AudioSource를 생성하는 메서드
    private void CreateAudioSource(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = clip;
            audioSources[clip] = newAudioSource;
        }
        else
        {
            Debug.LogError("오디오 클립이 존재하지 않습니다.");
        }
    }
    // 오디오 재생 메서드
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSources.ContainsKey(clip))
        {
            AudioSource source = audioSources[clip];
            source.Play();
        }
        else
        {
            Debug.LogError("사운드를 재생할 AudioSource가 없습니다.");
        }
    }

    // 오디오 반복 재생 메서드
    private void PlayLoopingSound(AudioClip clip)
    {
        if (clip != null && audioSources.ContainsKey(clip))
        {
            AudioSource source = audioSources[clip];
            source.loop = true;
            source.Play();
        }
        else
        {
            Debug.LogError("사운드를 반복 재생할 AudioSource가 없습니다.");
        }
    }
    // 오디오 중지 메서드
    public void StopSound(AudioClip clip)
    {
        if (clip != null && audioSources.ContainsKey(clip))
        {
            AudioSource source = audioSources[clip];
            source.loop = false;
            source.Stop();
        }
        else
        {
            Debug.LogError("사운드를 중지할 AudioSource가 없습니다.");
        }
    }

    // 각 오디오 클립별로 사용할 메서드들
    // 오디오 클립
    public void BGM_Sci_Fi_Map_Sound() => PlayLoopingSound(BGM_Sci_Fi_Map);
    public void StartProgress_Sound() => PlaySound(StartProgress);
    public void EndProgress_Sound() => PlaySound(EndProgress);
    public void Unlock_Sound() => PlaySound(Unlock);
    public void Die_Mob_Sound() => PlaySound(Die_Mob);
    public void Die_Boss_Sound() => PlaySound(Die_Boss);
    public void Golem_Attack_Sound() => PlaySound(Golem_Attack);
    public void Spider_Attack_Sound() => PlaySound(Spider_Attack);
    public void Turtle_Attack_Sound() => PlaySound(Turtle_Attack);
    public void Summon_Mob_Sound() => PlaySound(Summon_Mob);
    public void Summon_Meteor_Sound() => PlaySound(Summon_Meteor);
    public void Summon_Boom_Sound() => PlaySound(Summon_Boom);
}
