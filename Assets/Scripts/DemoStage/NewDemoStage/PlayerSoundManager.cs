using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager Instance = null;
    
    // 오디오 클립들
    public AudioClip Rifle_Shoot;
    public AudioClip ShootGun_Shoot;
    public AudioClip MachineGun_Shoot;
    public AudioClip Snier_Shoot;
    public AudioClip Granade_Shoot;
    public AudioClip Granade_Explosion;
    public AudioClip Flame_Shoot;
    
    public AudioClip Close_Attack_1;
    public AudioClip Close_Attack_2;
    
    public AudioClip Demacia_Skill_Land;
    public AudioClip Fantasy_Skill;
    public AudioClip Sliver_Skill;
    public AudioClip Static_Skill_Land;
    public AudioClip StreamOfEdge_Skill;
    
    public AudioClip Bomb_Plane;
    public AudioClip Bomb_Expolosion;
    public AudioClip Heli_Plane;
    public AudioClip Heli_Shoot;
    public AudioClip Turret_Shoot;
    public AudioClip Turrent_Explosion;
    
    public AudioClip reload;
    public AudioClip revive;
    
    

    // 오디오 소스 관리용 딕셔너리
    private Dictionary<AudioClip, AudioSource> audioSources = new Dictionary<AudioClip, AudioSource>();

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
        //CreateAudioSources();
    }

    private void LoadAudioClips()
    {
        Rifle_Shoot = Resources.Load<AudioClip>("Sound/SE/라이플소리");
        ShootGun_Shoot = Resources.Load<AudioClip>("Sound/SE/샷건");
        MachineGun_Shoot = Resources.Load<AudioClip>("Sound/SE/머신건");
        Snier_Shoot = Resources.Load<AudioClip>("Sound/SE/sniper");
        Granade_Shoot = Resources.Load<AudioClip>("Sound/SE/유탄 발사소리");
        Granade_Explosion = Resources.Load<AudioClip>("Sound/SE/유탄");
        Flame_Shoot = Resources.Load<AudioClip>("Sound/SE/화염방사기");
        
        Close_Attack_1 = Resources.Load<AudioClip>("Sound/SE/근접 무기 소리 1");
        Close_Attack_2 = Resources.Load<AudioClip>("Sound/SE/근접 무기 소리 2");
        
        Demacia_Skill_Land = Resources.Load<AudioClip>("Sound/SE/데마시아 떨어진 소리");
        Fantasy_Skill = Resources.Load<AudioClip>("Sound/SE/fantasyAxe스킬소리");
        Sliver_Skill = Resources.Load<AudioClip>("Sound/SE/sliver스킬소리");
        Static_Skill_Land = Resources.Load<AudioClip>("Sound/SE/statict스킬 떨어지는 소리");
        StreamOfEdge_Skill = Resources.Load<AudioClip>("Sound/SE/streamofedge스킬소리");
        
        Bomb_Plane = Resources.Load<AudioClip>("Sound/SE/bomb폭격기지나가는 소리");
        Bomb_Expolosion = Resources.Load<AudioClip>("Sound/SE/Bomb소리");
        Heli_Plane = Resources.Load<AudioClip>("Sound/SE/헬기지나가는소리");
        Heli_Shoot = Resources.Load<AudioClip>("Sound/SE/헬기총소리");
        Turret_Shoot = Resources.Load<AudioClip>("Sound/SE/포탑 미사일 발사소리");
        Turrent_Explosion = Resources.Load<AudioClip>("Sound/SE/포탑 미사일 터지는 소리");
        
        reload = Resources.Load<AudioClip>("Sound/SE/재장전");
        revive = Resources.Load<AudioClip>("Sound/SE/부활");
        
        
    }

    // 각 오디오 클립마다 AudioSource를 생성하여 저장하는 메서드
    /*private void CreateAudioSources()
    {
        CreateAudioSource(Rifle_Shoot);
        CreateAudioSource(ShootGun_Shoot);
        CreateAudioSource(MachineGun_Shoot);
        CreateAudioSource(Snier_Shoot);
        CreateAudioSource(Granade_Shoot);
        CreateAudioSource(Granade_Explosion);
        CreateAudioSource(Flame_Shoot);
        
        CreateAudioSource(Close_Attack_1);
        CreateAudioSource(Close_Attack_2);
        
        CreateAudioSource(Demacia_Skill_Land);
        CreateAudioSource(Fantasy_Skill);
        CreateAudioSource(Sliver_Skill);
        CreateAudioSource(Static_Skill_Land);
        CreateAudioSource(StreamOfEdge_Skill);
        
        CreateAudioSource(Bomb_Plane);
        CreateAudioSource(Bomb_Expolosion);
        CreateAudioSource(Heli_Plane);
        CreateAudioSource(Heli_Shoot);
        CreateAudioSource(Turret_Shoot);
        CreateAudioSource(Turrent_Explosion);
        
        CreateAudioSource(reload);
        CreateAudioSource(revive);
        
        
        
    }*/

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
    //public void Rifle_Shoot_Sound() => PlaySound(Rifle_Shoot);
    public void Rifle_Shoot_Sound() => SoundManager.instance.PlayEffectSound("라이플소리", 0.5f);
    public void ShootGun_Shoot_Sound() => SoundManager.instance.PlayEffectSound("샷건", 0.5f);
    public void MachineGun_Shoot_Sound() => SoundManager.instance.PlayEffectSound("머신건", 0.5f);
    public void Sniper_Shoot_Sound() => SoundManager.instance.PlayEffectSound("sniper", 0.5f);
    public void Granade_Shoot_Sound() => SoundManager.instance.PlayEffectSound("유탄 발사소리", 0.5f);
    public void Granade_Explosion_Sound() => SoundManager.instance.PlayEffectSound("유탄", 0.5f);
    public void Flame_Shoot_Sound() => SoundManager.instance.PlayEffectSound("화염방사기", 0.5f);

    public void Close_Attack_1_Sound() => SoundManager.instance.PlayEffectSound("근접 무기 소리 1", 0.5f);
    public void Close_Attack_2_Sound() => SoundManager.instance.PlayEffectSound("근접 무기 소리 2", 0.5f);

    public void Demacia_Skill_Land_Sound() => SoundManager.instance.PlayEffectSound("데마시아 떨어진 소리", 0.5f);
    public void Fantasy_Skill_Sound() => SoundManager.instance.PlayEffectSound("fantasyAxe스킬소리", 0.5f);
    public void Sliver_Skill_Sound() => SoundManager.instance.PlayEffectSound("sliver스킬소리", 0.5f);
    public void Static_Skill_Land_Sound() => SoundManager.instance.PlayEffectSound("statict스킬 떨어지는 소리", 0.5f);
    public void StreamOfEdge_Skill_Sound() => SoundManager.instance.PlayEffectSound("streamofedge스킬소리", 0.5f);

    public void Bomb_Plane_Sound() => SoundManager.instance.PlayEffectSound("bomb폭격기지나가는 소리", 0.5f);
    public void Heli_Plane_Sound() => SoundManager.instance.PlayEffectSound("헬기지나가는소리", 0.5f);
    public void Bomb_Expolosion_Sound() => SoundManager.instance.PlayEffectSound("Bomb소리", 0.5f);
    public void Heli_Shoot_Sound() => SoundManager.instance.PlayEffectSound("헬기총소리", 0.5f);
    public void Turret_Shoot_Sound() => SoundManager.instance.PlayEffectSound("포탑 미사일 발사소리", 0.5f);
    public void Turrent_Explosion_Sound() => SoundManager.instance.PlayEffectSound("포탑 미사일 터지는 소리", 0.5f);

    public void reload_Sound() => PlaySound(reload);
    public void revive_Sound() => SoundManager.instance.PlayEffectSound("부활", 0.5f);

    public void reload_Sound_stop() => StopSound(reload);

    


    public void Stop_Heli_Plane_Sound() => StopSound(Heli_Plane);
    public void Stop_Heli_Shoot_Sound() => StopSound(Heli_Shoot);
    
    
}
