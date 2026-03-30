
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /*//DamageManager ?댁떇?꾨즺
    /#1#/ ------ 怨듦꺽 怨꾩닔 ------ //
    //洹쇱젒 臾닿린
    [Header("Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //洹쇱젒 臾닿린 ?ㅽ궗
    [Header("Skill")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //珥앷린瑜?    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //珥앷린瑜??ㅽ궗
    public int Bomber_Skill_DamageCounting;             //??깂???곕?吏
    public int Bomber_Skill_WarheadKind;                //??깂???ш린
    public int Bomber_Skill_WarheadColor;                //??깂???됱긽  0:?뚯깋 1:鍮④컯, 2:珥덈줉, 3:?뚮옉, 4:?몃옉
    public int Turret_Skill_BulletColor;                //誘몄궗?쇱쓽 ?됱긽  0:?곗깋 1:鍮④컯, 2:珥덈줉, 3:?뚮옉, 4:?몃옉 5:?ъ꽍湲?    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;#1#
    
    //?섏씠吏 媛쒕뀗 ?ㅼ떆 ?앷컖
    // ------ Wave蹂?紐ъ뒪??諛?湲고? ?щЪ------ //
    /*[SerializeField] private GameObject[] Wave1_Monsters;
    [SerializeField] private GameObject[] Wave1_Directions;
    [SerializeField] private GameObject[] Wave3_Directions;
    public List<GameObject> Wave2_Monsters;
    [SerializeField] private GameObject[] Wave2_Monsters_Spawner;
    [SerializeField] private GameObject[] Wave3_Monsters_Spawner;
    [SerializeField] private GameObject[] Before3Peiz;
    [SerializeField] private GameObject[] After3Peiz;

    [SerializeField] private GameObject Peiz3Monster_1;
    public GameObject Peiz3Monster_2;
    [SerializeField] private GameObject Wave3_Block_Directions;
    [SerializeField] private GameObject EventBtn;#1#
    
    // ------ Wave trigger Collider------ //
    [SerializeField] private BoxCollider Area1;
    [SerializeField] private BoxCollider Area2;
    //public bool Area3;        //Wave3??寃쎌슦 ?대떦 蹂??true && Area2 ?쇱떆 吏꾪뻾   //EventManager ?댁떇?꾨즺
    [SerializeField] private bool Peiz3Start;  
    [SerializeField] private bool Peiz3Monster2UpdateControl;   
    //public bool Wave2MonsterClear;        //wave2紐ъ뒪?곕? 紐⑤몢 ?≪븯?붿?     //EventManager ?댁떇?꾨즺
    [SerializeField] private GameObject WaveArea3Scrit;
    [SerializeField] private GameObject WaveArea3Barrier;
    
    
    //MonsterManager ?댁떇?꾨즺
    //[SerializeField] private GameObject[] Stack;
    //[SerializeField] private int StackIndex;        //?ㅽ깮 ??紐ъ뒪?곗쓽 媛쒖닔,, top
    //public int Gauge;                               //?ㅽ깮 紐ъ뒪?곕? ?〓뒗 寃뚯씠吏
    //[SerializeField] private GameObject wave2Gauge;
    //wave3Gauge???섏씠吏媛쒕뀗 ?ㅼ떆 ?앷컖?섍린?꾪빐 ?쇰떒 蹂대쪟
    //[SerializeField] private GameObject wave3Gauge;
    
    
    //?⑤꼸? ?쇰떒 ?앷컖?덊븯怨??섍린濡?    /#1#/?쒗넗由ъ뼹 ?⑤꼸??    [SerializeField] private GameObject WelcomePanel;
    [SerializeField] private Button WelcomPanel_Btn;
    [SerializeField] private GameObject[] WelcomePanel_Text;
    [SerializeField] private int WelcomPanel_Text_Number;
    
    //?섏씠利? ?쒖옉???⑤꼸
    [SerializeField] private GameObject Peiz2StartPanel;
    [SerializeField] private Button Peiz2StartPanel_Btn;
    [SerializeField] private GameObject[] Peiz2StartPanel_Text;
    [SerializeField] private int Peiz2StartPanel_Text_Number;
    
    //?섏씠利? 醫낅즺???⑤꼸
    [SerializeField] private GameObject Peiz2EndPanel;
    [SerializeField] private Button Peiz2EndPanel_Btn;
    [SerializeField] private GameObject[] Peiz2EndPanel_Text;
    [SerializeField] private int Peiz2EndPanel_Text_Number;
    
    //而댄뙆?쇰윭 怨좎튇?ㅼ쓬 ?⑤꼸 (而댄뙆?쇰윭 怨좎튂???숈븞 ?뷀렂??
    [SerializeField] private GameObject Start3PeizPanel ;
    [SerializeField] private Button Start3PeizPanel_Btn;
    [SerializeField] private GameObject[] Start3PeizPanel_Text;
    [SerializeField] private int Start3PeizPanel_Text_Number;

   
    
    //媛쒕컻????湲덉? ?⑤꼸 3?섏씠利?Big monster?깆옣
    [SerializeField] private GameObject AfterCompilerPanel ;
    [SerializeField] private Button AfterCompilerPanel_Btn;
    [SerializeField] private GameObject[] AfterCompilerPanel_Text;
    [SerializeField] private int AfterCompilerPanel_Text_Number;#1#

    [SerializeField] private bool isPause;  //?꾩옱 寃뚯엫 ?쒓컙??硫덉톬?붿?

    [Header("Enemies")]
    public GameObject[] enemies;  //?꾩옱 ?ㅽ뀒?댁???紐ъ뒪?? Length濡?媛쒖닔瑜?援ы븷 ???덉쓬

    
    // Start is called before the first frame update
    void Start()
    {
        // 臾닿린 怨꾩닔 怨깊븯湲????곕?吏 ?ㅼ젙
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

        Bomber_Skill_WarheadKind = 4;
        Bomber_Skill_WarheadColor = 0;
        Bomber_Skill_DamageCounting = 1;
        Turret_Skill_BulletColor = 0;
        Turret_Skill_DamageCounting = 1;
        Helicopter_Skill_DamageCounting = 1;
        GunSpire_Skill_DamageCounting = 1;

        

        foreach (GameObject g in Wave1_Monsters)
        {
            //g.GetComponent<Enemy>().StopNav();
            g.SetActive(false);
        }
        foreach (GameObject g in Wave1_Directions)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Wave2_Monsters)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Wave2_Monsters_Spawner)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Wave3_Monsters_Spawner)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Before3Peiz)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in After3Peiz)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Wave3_Directions)
        {
            g.SetActive(false);
        }
        Wave3_Block_Directions.SetActive(true);

        Stack = new GameObject[10];
        StackIndex = 0;
        Gauge = 0;
        Area3 = false;
        Wave2MonsterClear = false;
        wave2Gauge.SetActive(false);
        wave3Gauge.SetActive(false);
        Peiz3Start = false;
        Peiz3Monster_1.SetActive(false);
        Peiz3Monster_2.SetActive(false);
        Peiz3Monster2UpdateControl = false;
        isPause = false;
        
        //?⑤꼸 鍮꾪솢?깊솕
        WelcomePanel.SetActive(false);
        WelcomPanel_Text_Number = 0;
        foreach (GameObject g in WelcomePanel_Text)
        {
            g.SetActive(false);
        }
        
        Peiz2StartPanel.SetActive(false);
        Peiz2StartPanel_Text_Number = 0;
        foreach (GameObject g in Peiz2StartPanel_Text)
        {
            g.SetActive(false);
        }
        
        Peiz2EndPanel.SetActive(false);
        Peiz2EndPanel_Text_Number = 0;
        foreach (GameObject g in Peiz2EndPanel_Text)
        {
            g.SetActive(false);
        }
        
        Start3PeizPanel.SetActive(false);
        Start3PeizPanel_Text_Number = 0;
        foreach (GameObject g in Start3PeizPanel_Text)
        {
            g.SetActive(false);
        }
        
        AfterCompilerPanel.SetActive(false);
        AfterCompilerPanel_Text_Number = 0;
        foreach (GameObject g in AfterCompilerPanel_Text)
        {
            g.SetActive(false);
        }
        
        
        
        
        Invoke("Start_WelcomePanel",1);
    }

    // Update is called once per frame
    void Update()
    {
        //?ㅽ깮紐ъ뒪??20 寃뚯씠吏 梨꾩슦硫?紐⑤몢 ??젣
        if (Gauge >= 2 && Wave2MonsterClear == false)
            Clear_Wave2_Monsters();

        //Area3??Peiz3Gauge?먯꽌 True濡?蹂寃?        if (Area3 == true && Peiz3Start == false)
        {
            Peiz3Start = true;
            Peiz3Monster_1.SetActive(true);  //3?섏씠利?紐ъ뒪?? ?깆옣
            //Start_AfterCompilerPanel();    //而댄뙆?쇰윭 怨좎튇 ???⑤꼸 ?깆옣
            Start_Panel(AfterCompilerPanel,AfterCompilerPanel_Btn,AfterCompilerPanel_Text,AfterCompilerPanel_Text_Number,false);
        }
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("?쒓렇媛 'Enemy'??寃뚯엫 ?ㅻ툕?앺듃??媛쒖닔: " + enemies.Length);
    }

    //1?섏씠利??쒖옉
    public void Area1Function()
    {
        foreach (GameObject g in Wave1_Monsters)
        {
            g.SetActive(true);
            g.GetComponent<Enemy>().StartNav();
        }
        foreach (GameObject d in Wave1_Directions)
        {
            d.SetActive(true);
        }
    }
    public void Area2Function()
    {
        //2?섏씠利??쒖옉
        if (Area3 == false)
        {
            foreach (GameObject d in Wave1_Directions)
            {
                d.SetActive(false);
            }
            foreach (GameObject g in Wave2_Monsters)
            {
                g.SetActive(true);
                g.GetComponent<Enemy>().StartNav();
            }
            foreach (GameObject g in Wave2_Monsters_Spawner)
            {
                g.SetActive(true);
            }
            wave2Gauge.SetActive(true);

            //Start_Peiz2StartPanel();  //2?섏씠利??⑤꼸 ?깆옣
            Start_Panel(Peiz2StartPanel, Peiz2StartPanel_Btn, Peiz2StartPanel_Text, Peiz2StartPanel_Text_Number, true);
        }
        else        //3?섏씠利??쒖옉
        {
            Debug.LogError("3?섏씠利??쒖옉");
            //?ㅻ━瑜??곌껐
            foreach (GameObject g in Before3Peiz)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in After3Peiz)
            {
                g.SetActive(true);
            }
            //湲몃ぉ ?쒓굅
            WaveArea3Scrit.SetActive(false);
            WaveArea3Barrier.SetActive(false);
            //EventBtn 鍮꾪솢?깊솕
            EventBtn.SetActive(false);
            Peiz3Monster_2.SetActive(true); 
        }
        
        
    }

    //?곸쓣 二쎌씤 寃쎌슦
    public void AddStackMonster(GameObject g)
    {
        //泥섏쓬 ?ㅼ뼱??紐ъ뒪?곗씤寃쎌슦
        if (StackIndex == 0)
        {
            Stack[StackIndex] = g;
            StackIndex++;
            return;
        }
        else if (StackIndex >= 10) //?ㅽ깮??苑?李쇰뒗??紐ъ뒪?곌? 二쎌? 寃쎌슦
        {
            if (g.GetComponent<Parenthesis>().identity == Stack[9].GetComponent<Parenthesis>().identity)
            {
                Stack[9].GetComponent<Parenthesis>().HitTheMonster(); //紐ъ뒪????젣
                Stack[9] = null; //?ㅽ깮 pop
                StackIndex = 9; //?몃뜳??以꾩씠湲?            }
            else
            {
                g.GetComponent<Parenthesis>().NotDeath();
            }
        }
        else
        {
            Stack[StackIndex] = g;
            StackIndex++;
            if (CheckParenthesis()) //愿꾪샇媛 留욎븘 ?⑥뼱吏?寃쎌슦
            {
                for (int i = 0; i < 2; i++)
                {
                    Stack[StackIndex - 1].GetComponentInChildren<Parenthesis>().HitTheMonster(); //?ㅽ깮?먯꽌 紐ъ뒪????젣
                    Stack[StackIndex - 1] = null; //?ㅽ깮 pop
                    StackIndex--; //?몃뜳??以꾩씠湲?                }

                Gauge++; //?ㅽ깮 寃뚯씠吏利앷?
                wave2Gauge.GetComponent<HealthBar>().SetHealth(Gauge);
            }
        }
        
       
    }

    //愿꾪샇???좏슚??寃??    private bool CheckParenthesis()
    {
        if (Stack[StackIndex - 1].GetComponent<Parenthesis>().identity ==
            Stack[StackIndex - 2].GetComponent<Parenthesis>().identity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //2?섏씠利???    public void Clear_Wave2_Monsters()
    {
        Wave2MonsterClear = true;
        //紐⑤뱺 ?ㅽ룷???앹꽦以묐떒
        foreach (GameObject g in Wave2_Monsters_Spawner)
        {
            g.GetComponent<Wave2StackMonsterSpawner>().Active = false;
            g.SetActive(false);
        }

        wave2Gauge.GetComponent<HealthBar>().ClearWave2();
        Invoke("ClearWave2MonsterInvoke",3);
    }

    private void ClearWave2MonsterInvoke()
    {
        for (int i = Wave2_Monsters.Count-1; i >= 0; i--)
        {
            try
            {
                if(Wave2_Monsters[i] != null)
                    Wave2_Monsters[i].GetComponentInChildren<Parenthesis>().ClearTheMonster();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.LogError(i);
                throw;
            }
        }
        wave2Gauge.SetActive(false);

        //Start_Peiz2EndPanel(); //2?섏씠利??앸궃 ?⑤꼸 ?깆옣
        Start_Panel(Peiz2EndPanel,Peiz2EndPanel_Btn,Peiz2EndPanel_Text,Peiz2EndPanel_Text_Number,false);
    }

    public void AddStackMonster_In_Array(GameObject m)
    {
        Wave2_Monsters.Add(m);
    }

    //?섏씠吏 媛쒕뀗 ?ㅼ떆 ?앷컖
    //3?섏씠利?諛⑺뼢
    /*public void OnWave3Direction()
    {
        foreach (GameObject g in Wave3_Directions)
        {
            g.SetActive(true);
        }
        Wave3_Block_Directions.SetActive(false);
    }#1#
    
    private void Start_Panel(GameObject panal, Button btn, GameObject[] text, int number, bool timestop)
    {
        if (timestop == true)
        {
            Time.timeScale = 0;    //寃뚯엫 ?쇱떆?뺤?
            isPause = true;
        }
        btn.onClick.AddListener(() => NextText(panal, text, ref number));
        panal.SetActive(true);    //?⑤꼸?깆옣
        text[number].SetActive(true);      //泥??⑤꼸 硫붿꽭吏 ?깆옣
    }

    //?ㅽ뀒?댁? 泥??⑤꼸 ?깆옣 ?⑥닔
    private void Start_WelcomePanel()
    {
        Time.timeScale = 0;    //寃뚯엫 ?쇱떆?뺤?
        isPause = true;
        WelcomPanel_Btn.onClick.AddListener(() => NextText(WelcomePanel,WelcomePanel_Text, ref WelcomPanel_Text_Number));
        WelcomePanel.SetActive(true);    //?⑤꼸?깆옣
        WelcomePanel_Text[WelcomPanel_Text_Number].SetActive(true);      //泥??⑤꼸 硫붿꽭吏 ?깆옣
    }
    
    /*private void Start_Peiz2StartPanel()
    {
        Time.timeScale = 0;    //寃뚯엫 ?쇱떆?뺤?
        isPause = true;
        Peiz2StartPanel_Btn.onClick.AddListener(() => NextText(Peiz2StartPanel_Text, ref Peiz2StartPanel_Text_Number));
        Peiz2StartPanel.SetActive(true);    //?⑤꼸?깆옣
        Peiz2StartPanel_Text[Peiz2StartPanel_Text_Number].SetActive(true);      //泥??⑤꼸 硫붿꽭吏 ?깆옣
    }
    
    private void Start_Peiz2EndPanel()
    {
        Peiz2EndPanel_Btn.onClick.AddListener(() => NextText(Peiz2EndPanel_Text, ref Peiz2EndPanel_Text_Number));
        Peiz2EndPanel.SetActive(true);    //?⑤꼸?깆옣
        Peiz2EndPanel_Text[Peiz2EndPanel_Text_Number].SetActive(true);      //泥??⑤꼸 硫붿꽭吏 ?깆옣
    }
    
    private void Start_AfterCompilerPanel()
    {
        AfterCompilerPanel_Btn.onClick.AddListener(() => NextText(AfterCompilerPanel_Text, ref AfterCompilerPanel_Text_Number));
        AfterCompilerPanel.SetActive(true);    //?⑤꼸?깆옣
        AfterCompilerPanel_Text[AfterCompilerPanel_Text_Number].SetActive(true);      //泥??⑤꼸 硫붿꽭吏 ?깆옣
    }#1#

    //?ㅽ뀒?댁? 泥??⑤꼸???띿뒪?몃? ?섍린???⑥닔 
    private void NextText(GameObject panal,GameObject[] TextArray, ref int TextIndex)
    {
        if (TextArray[TextIndex].activeSelf)  //?ㅼ쓬 ?⑤꼸 硫붿꽭吏 ?깆옣
        {
            TextArray[TextIndex].SetActive(false);
            TextIndex++;
            if (TextIndex >= TextArray.Length)     //留덉?留??⑤꼸 硫붿꽭吏?쇰㈃
            {
                panal.SetActive(false);
                if (isPause == true)    //?쒓컙??硫덉톬?ㅻ㈃ 
                {
                    Time.timeScale = 1;  //?쒓컙?섎룎由ш린
                    isPause = false;
                }
                
            }
            else
            {
                TextArray[TextIndex].SetActive(true);
            }
        }
    }

    public void Peiz3MonsterSpawn()
    {
        foreach (GameObject g in Wave3_Monsters_Spawner)
        {
            g.SetActive(true);
        }
    }

    public void StartPeiz3Pannel()
    {
        Start_Panel(Start3PeizPanel,Start3PeizPanel_Btn,Start3PeizPanel_Text,Start3PeizPanel_Text_Number,true);
    }
*/    
}

