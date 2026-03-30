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
            //???대옒???몄뒪?댁뒪媛 ?꾩깮?덉쓣 ???꾩뿭蹂??instance??寃뚯엫留ㅻ땲? ?몄뒪?댁뒪媛 ?닿꺼?덉? ?딅떎硫? ?먯떊???ｌ뼱以??
            instance = this;

            //???꾪솚???섎뜑?쇰룄 ?뚭눼?섏? ?딄쾶 ?쒕떎.
            //gameObject留뚯쑝濡쒕룄 ???ㅽ겕由쏀듃媛 而댄룷?뚰듃濡쒖꽌 遺숈뼱?덈뒗 Hierarchy?곸쓽 寃뚯엫?ㅻ툕?앺듃?쇰뒗 ?살씠吏留? 
            //?섎뒗 ?룰컝由?諛⑹?瑜??꾪빐 this瑜?遺숈뿬二쇨린???쒕떎.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //留뚯빟 ???대룞???섏뿀?붾뜲 洹??ъ뿉??Hierarchy??GameMgr??議댁옱???섎룄 ?덈떎.
            //洹몃윺 寃쎌슦???댁쟾 ?ъ뿉???ъ슜?섎뜕 ?몄뒪?댁뒪瑜?怨꾩냽 ?ъ슜?댁＜??寃쎌슦媛 留롮? 寃?媛숇떎.
            //洹몃옒???대? ?꾩뿭蹂?섏씤 instance???몄뒪?댁뒪媛 議댁옱?쒕떎硫??먯떊(?덈줈???ъ쓽 GameMgr)????젣?댁???
            Destroy(this.gameObject);
        }
    }
    //寃뚯엫 留ㅻ땲? ?몄뒪?댁뒪???묎렐?????덈뒗 ?꾨줈?쇳떚. static?대?濡??ㅻⅨ ?대옒?ㅼ뿉??留섍퍘 ?몄텧?????덈떎.
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
    
    // ------ 怨듦꺽 怨꾩닔 ------ //
    //洹쇱젒 臾닿린
    [Header("Sword Attack Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //洹쇱젒 臾닿린 ?ㅽ궗
    [Header("Sword Skill Damage Counting")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //珥앷린瑜?    [Header("Gun Attack Damage Counting")]
    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //珥앷린瑜??ㅽ궗
    [Header("Gun Skill Damage Counting")]
    public int Bomber_Skill_DamageCounting;             //??깂???곕?吏
    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;
    
    //珥앷린瑜??ㅽ궗
    [Header("Gun Skill Kind")]
    public int Bomber_Skill_WarheadKind;                //??깂???ш린
    public int Bomber_Skill_WarheadColor;                //??깂???됱긽  0:?뚯깋 1:鍮④컯, 2:珥덈줉, 3:?뚮옉, 4:?몃옉
    public int Turret_Skill_BulletColor;                //誘몄궗?쇱쓽 ?됱긽  0:?곗깋 1:鍮④컯, 2:珥덈줉, 3:?뚮옉, 4:?몃옉 5:?ъ꽍湲?
    private void Start()
    {
        // 臾닿린 怨꾩닔 怨깊븯湲????곕?吏 ?ㅼ젙
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
