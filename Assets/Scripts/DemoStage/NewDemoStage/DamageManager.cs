using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private static DamageManager instance = null;
    private void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }
    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static DamageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    
    // ------ 공격 계수 ------ //
    //근접 무기
    [Header("Sword Attack Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //근접 무기 스킬
    [Header("Sword Skill Damage Counting")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //총기류
    [Header("Gun Attack Damage Counting")]
    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //총기류 스킬
    [Header("Gun Skill Damage Counting")]
    public int Bomber_Skill_DamageCounting;             //폭탄의 데미지
    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;
    
    //총기류 스킬
    [Header("Gun Skill Kind")]
    public int Bomber_Skill_WarheadKind;                //폭탄의 크기
    public int Bomber_Skill_WarheadColor;                //폭탄의 색상  0:회색 1:빨강, 2:초록, 3:파랑, 4:노랑
    public int Turret_Skill_BulletColor;                //미사일의 색상  0:흰색 1:빨강, 2:초록, 3:파랑, 4:노랑 5:투석기

    private void Start()
    {
        // 무기 계수 곱하기 전 데미지 설정
        SwordStreamEdge_DamageCounting = 20;
        SwordStatic_DamageCounting = 20;
        SwordSliver_DamageCounting = 20;
        SwordDemacia_DamageCounting = 20;
        FantasyAxe_DamageCounting = 20;
        
        SwordStreamEdge_Skill_DamageCounting = 40;
        SwordStatic_Passive_DamageCounting = 15;
        SwordStatic_Skill_DamageCounting = 40;
        SwordSliver_Skill_DamageCounting = 40;
        SwordDemacia_Skill_DamageCounting = 40;
        FantasyAxe_Skill_DamageCounting = 100;
        
        FlameGun_DamageCounting = 2;
        MachineGun_DamageCounting = 1;
        GrenadeLauncher_DamageCounting = 3;
        Sniper_DamageCounting = 30;
        Rifle_DamageCounting = 1;
        ShotGun_DamageCounting = 1;

        
        Bomber_Skill_DamageCounting = 30;
        Turret_Skill_DamageCounting = 10;
        Helicopter_Skill_DamageCounting = 5;
        GunSpire_Skill_DamageCounting = 1;
        
        Bomber_Skill_WarheadKind = 4;
        Bomber_Skill_WarheadColor = 0;
        Turret_Skill_BulletColor = 0;
    }
}
