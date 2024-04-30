using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
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
        SwordStreamEdge_DamageCounting = 1;
        SwordStatic_DamageCounting = 1;
        SwordSliver_DamageCounting = 1;
        SwordDemacia_DamageCounting = 50;
        FantasyAxe_DamageCounting = 100;
        
        SwordStreamEdge_Skill_DamageCounting = 1;
        SwordStatic_Passive_DamageCounting = 1;
        SwordStatic_Skill_DamageCounting = 1;
        SwordSliver_Skill_DamageCounting = 1;
        SwordDemacia_Skill_DamageCounting = 50;
        FantasyAxe_Skill_DamageCounting = 1;
        
        FlameGun_DamageCounting = 50;
        MachineGun_DamageCounting = 1;
        GrenadeLauncher_DamageCounting = 1;
        Sniper_DamageCounting = 1;
        Rifle_DamageCounting = 1;
        ShotGun_DamageCounting = 1;

        
        Bomber_Skill_DamageCounting = 1;
        Turret_Skill_DamageCounting = 1;
        Helicopter_Skill_DamageCounting = 1;
        GunSpire_Skill_DamageCounting = 1;
        
        Bomber_Skill_WarheadKind = 4;
        Bomber_Skill_WarheadColor = 0;
        Turret_Skill_BulletColor = 0;
    }
}
