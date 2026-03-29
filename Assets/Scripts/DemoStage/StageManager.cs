
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /*//DamageManager ?´ì‹?„ë£Œ
    /#1#/ ------ ê³µê²© ê³„ìˆ˜ ------ //
    //ê·¼ì ‘ ë¬´ê¸°
    [Header("Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //ê·¼ì ‘ ë¬´ê¸° ?¤í‚¬
    [Header("Skill")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
    public int SwordStatic_Skill_DamageCounting;
    public int SwordSliver_Skill_DamageCounting;
    public int SwordDemacia_Skill_DamageCounting;
    public int FantasyAxe_Skill_DamageCounting;
    
    //ì´ê¸°ë¥?    public int FlameGun_DamageCounting;
    public int MachineGun_DamageCounting;
    public int GrenadeLauncher_DamageCounting;
    public int Sniper_DamageCounting;
    public int Rifle_DamageCounting;
    public int ShotGun_DamageCounting;
    
    //ì´ê¸°ë¥??¤í‚¬
    public int Bomber_Skill_DamageCounting;             //??ƒ„???°ë?ì§€
    public int Bomber_Skill_WarheadKind;                //??ƒ„???¬ê¸°
    public int Bomber_Skill_WarheadColor;                //??ƒ„???‰ìƒ  0:?Œìƒ‰ 1:ë¹¨ê°•, 2:ì´ˆë¡, 3:?Œë‘, 4:?¸ë‘
    public int Turret_Skill_BulletColor;                //ë¯¸ì‚¬?¼ì˜ ?‰ìƒ  0:?°ìƒ‰ 1:ë¹¨ê°•, 2:ì´ˆë¡, 3:?Œë‘, 4:?¸ë‘ 5:?¬ì„ê¸?    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;#1#
    
    //?˜ì´ì§€ ê°œë… ?¤ì‹œ ?ê°
    // ------ Waveë³?ëª¬ìŠ¤??ë°?ê¸°í? ?¬ë¬¼------ //
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
    //public bool Area3;        //Wave3??ê²½ìš° ?´ë‹¹ ë³€??true && Area2 ?¼ì‹œ ì§„í–‰   //EventManager ?´ì‹?„ë£Œ
    [SerializeField] private bool Peiz3Start;  
    [SerializeField] private bool Peiz3Monster2UpdateControl;   
    //public bool Wave2MonsterClear;        //wave2ëª¬ìŠ¤?°ë? ëª¨ë‘ ?¡ì•˜?”ì?     //EventManager ?´ì‹?„ë£Œ
    [SerializeField] private GameObject WaveArea3Scrit;
    [SerializeField] private GameObject WaveArea3Barrier;
    
    
    //MonsterManager ?´ì‹?„ë£Œ
    //[SerializeField] private GameObject[] Stack;
    //[SerializeField] private int StackIndex;        //?¤íƒ ??ëª¬ìŠ¤?°ì˜ ê°œìˆ˜,, top
    //public int Gauge;                               //?¤íƒ ëª¬ìŠ¤?°ë? ?¡ëŠ” ê²Œì´ì§€
    //[SerializeField] private GameObject wave2Gauge;
    //wave3Gauge???˜ì´ì§€ê°œë… ?¤ì‹œ ?ê°?˜ê¸°?„í•´ ?¼ë‹¨ ë³´ë¥˜
    //[SerializeField] private GameObject wave3Gauge;
    
    
    //?¨ë„?€ ?¼ë‹¨ ?ê°?ˆí•˜ê³??˜ê¸°ë¡?    /#1#/?œí† ë¦¬ì–¼ ?¨ë„??    [SerializeField] private GameObject WelcomePanel;
    [SerializeField] private Button WelcomPanel_Btn;
    [SerializeField] private GameObject[] WelcomePanel_Text;
    [SerializeField] private int WelcomPanel_Text_Number;
    
    //?˜ì´ì¦? ?œì‘???¨ë„
    [SerializeField] private GameObject Peiz2StartPanel;
    [SerializeField] private Button Peiz2StartPanel_Btn;
    [SerializeField] private GameObject[] Peiz2StartPanel_Text;
    [SerializeField] private int Peiz2StartPanel_Text_Number;
    
    //?˜ì´ì¦? ì¢…ë£Œ???¨ë„
    [SerializeField] private GameObject Peiz2EndPanel;
    [SerializeField] private Button Peiz2EndPanel_Btn;
    [SerializeField] private GameObject[] Peiz2EndPanel_Text;
    [SerializeField] private int Peiz2EndPanel_Text_Number;
    
    //ì»´íŒŒ?¼ëŸ¬ ê³ ì¹œ?¤ìŒ ?¨ë„ (ì»´íŒŒ?¼ëŸ¬ ê³ ì¹˜???™ì•ˆ ?”íœ??
    [SerializeField] private GameObject Start3PeizPanel ;
    [SerializeField] private Button Start3PeizPanel_Btn;
    [SerializeField] private GameObject[] Start3PeizPanel_Text;
    [SerializeField] private int Start3PeizPanel_Text_Number;

   
    
    //ê°œë°œ????ê¸ˆì? ?¨ë„ 3?˜ì´ì¦?Big monster?±ì¥
    [SerializeField] private GameObject AfterCompilerPanel ;
    [SerializeField] private Button AfterCompilerPanel_Btn;
    [SerializeField] private GameObject[] AfterCompilerPanel_Text;
    [SerializeField] private int AfterCompilerPanel_Text_Number;#1#

    [SerializeField] private bool isPause;  //?„ì¬ ê²Œì„ ?œê°„??ë©ˆì·„?”ì?

    [Header("Enemies")]
    public GameObject[] enemies;  //?„ì¬ ?¤í…Œ?´ì???ëª¬ìŠ¤?? Lengthë¡?ê°œìˆ˜ë¥?êµ¬í•  ???ˆìŒ

    
    // Start is called before the first frame update
    void Start()
    {
        // ë¬´ê¸° ê³„ìˆ˜ ê³±í•˜ê¸????°ë?ì§€ ?¤ì •
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
        
        //?¨ë„ ë¹„í™œ?±í™”
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
        //?¤íƒëª¬ìŠ¤??20 ê²Œì´ì§€ ì±„ìš°ë©?ëª¨ë‘ ?? œ
        if (Gauge >= 2 && Wave2MonsterClear == false)
            Clear_Wave2_Monsters();

        //Area3??Peiz3Gauge?ì„œ Trueë¡?ë³€ê²?        if (Area3 == true && Peiz3Start == false)
        {
            Peiz3Start = true;
            Peiz3Monster_1.SetActive(true);  //3?˜ì´ì¦?ëª¬ìŠ¤?? ?±ì¥
            //Start_AfterCompilerPanel();    //ì»´íŒŒ?¼ëŸ¬ ê³ ì¹œ ???¨ë„ ?±ì¥
            Start_Panel(AfterCompilerPanel,AfterCompilerPanel_Btn,AfterCompilerPanel_Text,AfterCompilerPanel_Text_Number,false);
        }
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("?œê·¸ê°€ 'Enemy'??ê²Œì„ ?¤ë¸Œ?íŠ¸??ê°œìˆ˜: " + enemies.Length);
    }

    //1?˜ì´ì¦??œì‘
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
        //2?˜ì´ì¦??œì‘
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

            //Start_Peiz2StartPanel();  //2?˜ì´ì¦??¨ë„ ?±ì¥
            Start_Panel(Peiz2StartPanel, Peiz2StartPanel_Btn, Peiz2StartPanel_Text, Peiz2StartPanel_Text_Number, true);
        }
        else        //3?˜ì´ì¦??œì‘
        {
            Debug.LogError("3?˜ì´ì¦??œì‘");
            //?¤ë¦¬ë¥??°ê²°
            foreach (GameObject g in Before3Peiz)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in After3Peiz)
            {
                g.SetActive(true);
            }
            //ê¸¸ëª© ?œê±°
            WaveArea3Scrit.SetActive(false);
            WaveArea3Barrier.SetActive(false);
            //EventBtn ë¹„í™œ?±í™”
            EventBtn.SetActive(false);
            Peiz3Monster_2.SetActive(true); 
        }
        
        
    }

    //?ì„ ì£½ì¸ ê²½ìš°
    public void AddStackMonster(GameObject g)
    {
        //ì²˜ìŒ ?¤ì–´??ëª¬ìŠ¤?°ì¸ê²½ìš°
        if (StackIndex == 0)
        {
            Stack[StackIndex] = g;
            StackIndex++;
            return;
        }
        else if (StackIndex >= 10) //?¤íƒ??ê½?ì°¼ëŠ”??ëª¬ìŠ¤?°ê? ì£½ì? ê²½ìš°
        {
            if (g.GetComponent<Parenthesis>().identity == Stack[9].GetComponent<Parenthesis>().identity)
            {
                Stack[9].GetComponent<Parenthesis>().HitTheMonster(); //ëª¬ìŠ¤???? œ
                Stack[9] = null; //?¤íƒ pop
                StackIndex = 9; //?¸ë±??ì¤„ì´ê¸?            }
            else
            {
                g.GetComponent<Parenthesis>().NotDeath();
            }
        }
        else
        {
            Stack[StackIndex] = g;
            StackIndex++;
            if (CheckParenthesis()) //ê´„í˜¸ê°€ ë§ì•„ ?¨ì–´ì§?ê²½ìš°
            {
                for (int i = 0; i < 2; i++)
                {
                    Stack[StackIndex - 1].GetComponentInChildren<Parenthesis>().HitTheMonster(); //?¤íƒ?ì„œ ëª¬ìŠ¤???? œ
                    Stack[StackIndex - 1] = null; //?¤íƒ pop
                    StackIndex--; //?¸ë±??ì¤„ì´ê¸?                }

                Gauge++; //?¤íƒ ê²Œì´ì§€ì¦ê?
                wave2Gauge.GetComponent<HealthBar>().SetHealth(Gauge);
            }
        }
        
       
    }

    //ê´„í˜¸??? íš¨??ê²€??    private bool CheckParenthesis()
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

    //2?˜ì´ì¦???    public void Clear_Wave2_Monsters()
    {
        Wave2MonsterClear = true;
        //ëª¨ë“  ?¤í¬???ì„±ì¤‘ë‹¨
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

        //Start_Peiz2EndPanel(); //2?˜ì´ì¦??ë‚œ ?¨ë„ ?±ì¥
        Start_Panel(Peiz2EndPanel,Peiz2EndPanel_Btn,Peiz2EndPanel_Text,Peiz2EndPanel_Text_Number,false);
    }

    public void AddStackMonster_In_Array(GameObject m)
    {
        Wave2_Monsters.Add(m);
    }

    //?˜ì´ì§€ ê°œë… ?¤ì‹œ ?ê°
    //3?˜ì´ì¦?ë°©í–¥
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
            Time.timeScale = 0;    //ê²Œì„ ?¼ì‹œ?•ì?
            isPause = true;
        }
        btn.onClick.AddListener(() => NextText(panal, text, ref number));
        panal.SetActive(true);    //?¨ë„?±ì¥
        text[number].SetActive(true);      //ì²??¨ë„ ë©”ì„¸ì§€ ?±ì¥
    }

    //?¤í…Œ?´ì? ì²??¨ë„ ?±ì¥ ?¨ìˆ˜
    private void Start_WelcomePanel()
    {
        Time.timeScale = 0;    //ê²Œì„ ?¼ì‹œ?•ì?
        isPause = true;
        WelcomPanel_Btn.onClick.AddListener(() => NextText(WelcomePanel,WelcomePanel_Text, ref WelcomPanel_Text_Number));
        WelcomePanel.SetActive(true);    //?¨ë„?±ì¥
        WelcomePanel_Text[WelcomPanel_Text_Number].SetActive(true);      //ì²??¨ë„ ë©”ì„¸ì§€ ?±ì¥
    }
    
    /*private void Start_Peiz2StartPanel()
    {
        Time.timeScale = 0;    //ê²Œì„ ?¼ì‹œ?•ì?
        isPause = true;
        Peiz2StartPanel_Btn.onClick.AddListener(() => NextText(Peiz2StartPanel_Text, ref Peiz2StartPanel_Text_Number));
        Peiz2StartPanel.SetActive(true);    //?¨ë„?±ì¥
        Peiz2StartPanel_Text[Peiz2StartPanel_Text_Number].SetActive(true);      //ì²??¨ë„ ë©”ì„¸ì§€ ?±ì¥
    }
    
    private void Start_Peiz2EndPanel()
    {
        Peiz2EndPanel_Btn.onClick.AddListener(() => NextText(Peiz2EndPanel_Text, ref Peiz2EndPanel_Text_Number));
        Peiz2EndPanel.SetActive(true);    //?¨ë„?±ì¥
        Peiz2EndPanel_Text[Peiz2EndPanel_Text_Number].SetActive(true);      //ì²??¨ë„ ë©”ì„¸ì§€ ?±ì¥
    }
    
    private void Start_AfterCompilerPanel()
    {
        AfterCompilerPanel_Btn.onClick.AddListener(() => NextText(AfterCompilerPanel_Text, ref AfterCompilerPanel_Text_Number));
        AfterCompilerPanel.SetActive(true);    //?¨ë„?±ì¥
        AfterCompilerPanel_Text[AfterCompilerPanel_Text_Number].SetActive(true);      //ì²??¨ë„ ë©”ì„¸ì§€ ?±ì¥
    }#1#

    //?¤í…Œ?´ì? ì²??¨ë„???ìŠ¤?¸ë? ?˜ê¸°???¨ìˆ˜ 
    private void NextText(GameObject panal,GameObject[] TextArray, ref int TextIndex)
    {
        if (TextArray[TextIndex].activeSelf)  //?¤ìŒ ?¨ë„ ë©”ì„¸ì§€ ?±ì¥
        {
            TextArray[TextIndex].SetActive(false);
            TextIndex++;
            if (TextIndex >= TextArray.Length)     //ë§ˆì?ë§??¨ë„ ë©”ì„¸ì§€?¼ë©´
            {
                panal.SetActive(false);
                if (isPause == true)    //?œê°„??ë©ˆì·„?¤ë©´ 
                {
                    Time.timeScale = 1;  //?œê°„?˜ëŒë¦¬ê¸°
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

