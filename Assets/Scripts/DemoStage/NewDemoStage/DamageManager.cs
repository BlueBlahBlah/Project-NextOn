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
            //???´ë˜???¸ìŠ¤?´ìŠ¤ê°€ ?„ìƒ?ˆì„ ???„ì—­ë³€??instance??ê²Œì„ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤ê°€ ?´ê²¨?ˆì? ?Šë‹¤ë©? ?ì‹ ???£ì–´ì¤€??
            instance = this;

            //???„í™˜???˜ë”?¼ë„ ?Œê´´?˜ì? ?Šê²Œ ?œë‹¤.
            //gameObjectë§Œìœ¼ë¡œë„ ???¤í¬ë¦½íŠ¸ê°€ ì»´í¬?ŒíŠ¸ë¡œì„œ ë¶™ì–´?ˆëŠ” Hierarchy?ì˜ ê²Œì„?¤ë¸Œ?íŠ¸?¼ëŠ” ?»ì´ì§€ë§? 
            //?˜ëŠ” ?·ê°ˆë¦?ë°©ì?ë¥??„í•´ thisë¥?ë¶™ì—¬ì£¼ê¸°???œë‹¤.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //ë§Œì•½ ???´ë™???˜ì—ˆ?”ë° ê·??¬ì—??Hierarchy??GameMgr??ì¡´ì¬???˜ë„ ?ˆë‹¤.
            //ê·¸ëŸ´ ê²½ìš°???´ì „ ?¬ì—???¬ìš©?˜ë˜ ?¸ìŠ¤?´ìŠ¤ë¥?ê³„ì† ?¬ìš©?´ì£¼??ê²½ìš°ê°€ ë§ì? ê²?ê°™ë‹¤.
            //ê·¸ë˜???´ë? ?„ì—­ë³€?˜ì¸ instance???¸ìŠ¤?´ìŠ¤ê°€ ì¡´ì¬?œë‹¤ë©??ì‹ (?ˆë¡œ???¬ì˜ GameMgr)???? œ?´ì???
            Destroy(this.gameObject);
        }
    }
    //ê²Œì„ ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤???‘ê·¼?????ˆëŠ” ?„ë¡œ?¼í‹°. static?´ë?ë¡??¤ë¥¸ ?´ë˜?¤ì—??ë§˜ê» ?¸ì¶œ?????ˆë‹¤.
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
    
    // ------ ê³µê²© ê³„ìˆ˜ ------ //
    //ê·¼ì ‘ ë¬´ê¸°
    [Header("Sword Attack Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //ê·¼ì ‘ ë¬´ê¸° ?¤í‚¬
    [Header("Sword Skill Damage Counting")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //ì´ê¸°ë¥?    [Header("Gun Attack Damage Counting")]
    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //ì´ê¸°ë¥??¤í‚¬
    [Header("Gun Skill Damage Counting")]
    public int Bomber_Skill_DamageCounting;             //??ƒ„???°ë?ì§€
    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;
    
    //ì´ê¸°ë¥??¤í‚¬
    [Header("Gun Skill Kind")]
    public int Bomber_Skill_WarheadKind;                //??ƒ„???¬ê¸°
    public int Bomber_Skill_WarheadColor;                //??ƒ„???‰ìƒ  0:?Œìƒ‰ 1:ë¹¨ê°•, 2:ì´ˆë¡, 3:?Œë‘, 4:?¸ë‘
    public int Turret_Skill_BulletColor;                //ë¯¸ì‚¬?¼ì˜ ?‰ìƒ  0:?°ìƒ‰ 1:ë¹¨ê°•, 2:ì´ˆë¡, 3:?Œë‘, 4:?¸ë‘ 5:?¬ì„ê¸?
    private void Start()
    {
        // ë¬´ê¸° ê³„ìˆ˜ ê³±í•˜ê¸????°ë?ì§€ ?¤ì •
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
        
        FlameGun_DamageCounting = 10;
        MachineGun_DamageCounting = 12;
        GrenadeLauncher_DamageCounting = 25;
        Sniper_DamageCounting = 45;
        Rifle_DamageCounting = 10;
        ShotGun_DamageCounting = 7;

        
        Bomber_Skill_DamageCounting = 50;
        Turret_Skill_DamageCounting = 15;
        Helicopter_Skill_DamageCounting = 15;
        GunSpire_Skill_DamageCounting = 1;
        
        Bomber_Skill_WarheadKind = 4;
        Bomber_Skill_WarheadColor = 0;
        Turret_Skill_BulletColor = 0;
    }
}
