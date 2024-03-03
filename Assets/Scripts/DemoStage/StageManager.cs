using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ------ 공격 계수 ------ //
    //근접 무기
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;
    
    //근접 무기 스킬
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //총기류
    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //총기류 스킬
    public int Bomber_Skill_DamageCounting;
    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;
    
    // ------ Wave별 몬스터 및 기타 사물------ //
    [SerializeField] private GameObject[] Wave1_Monsters;
    [SerializeField] private GameObject[] Wave1_Directions;
    [SerializeField] private GameObject[] Wave2_Monsters;
    [SerializeField] private GameObject[] Wave3_Monsters;
    
    // ------ Wave trigger Collider------ //
    [SerializeField] private BoxCollider Area1;
    [SerializeField] private BoxCollider Area2;
    [SerializeField] private bool Area3;        //Wave3의 경우 해당 변수 true && Area2 일시 진행
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SwordStreamEdge_DamageCounting = 1;
        SwordStatic_DamageCounting = 1;
        SwordSliver_DamageCounting = 1;
        SwordDemacia_DamageCounting = 1;
        FantasyAxe_DamageCounting = 1;
        
        SwordStreamEdge_Skill_DamageCounting = 1;
        SwordStatic_Skill_DamageCounting = 1;
        SwordSliver_Skill_DamageCounting = 1;
        SwordDemacia_Skill_DamageCounting = 1;
        FantasyAxe_Skill_DamageCounting = 1;
        
        FlameGun_DamageCounting = 1;
        MachineGun_DamageCounting = 1;
        GrenadeLauncher_DamageCounting = 1;
        Sniper_DamageCounting = 1;
        Rifle_DamageCounting = 1;
        ShotGun_DamageCounting = 1;
        
        Bomber_Skill_DamageCounting = 1;
        Turret_Skill_DamageCounting = 1;
        Helicopter_Skill_DamageCounting = 1;
        GunSpire_Skill_DamageCounting = 1;

        foreach (GameObject g in Wave1_Monsters)
        {
            //g.GetComponent<Enemy>().stopNav();
            g.SetActive(false);
        }
        foreach (GameObject g in Wave1_Directions)
        {
            g.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Area1Function()
    {
        foreach (GameObject g in Wave1_Monsters)
        {
            g.SetActive(true);
            g.GetComponent<Enemy>().startNav();
        }
        foreach (GameObject d in Wave1_Directions)
        {
            d.SetActive(true);
        }
    }
    
    
}
