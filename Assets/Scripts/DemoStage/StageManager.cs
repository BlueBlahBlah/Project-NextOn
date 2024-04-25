using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // ------ 공격 계수 ------ //
    //근접 무기
    [Header("Damage Counting")]
    public int SwordStreamEdge_DamageCounting;
    public int SwordStatic_DamageCounting;
    public int SwordSliver_DamageCounting;
    public int SwordDemacia_DamageCounting;
    public int FantasyAxe_DamageCounting;

    //근접 무기 스킬
    [Header("Skill")]
    public int SwordStreamEdge_Skill_DamageCounting;
    public int SwordStatic_Passive_DamageCounting;
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
    public int Bomber_Skill_DamageCounting;             //폭탄의 데미지
    public int Bomber_Skill_WarheadKind;                //폭탄의 크기
    public int Bomber_Skill_WarheadColor;                //폭탄의 색상  0:회색 1:빨강, 2:초록, 3:파랑, 4:노랑
    public int Turret_Skill_BulletColor;                //미사일의 색상  0:흰색 1:빨강, 2:초록, 3:파랑, 4:노랑 5:투석기
    public int Turret_Skill_DamageCounting;
    public int Helicopter_Skill_DamageCounting;
    public int GunSpire_Skill_DamageCounting;
    
    // ------ Wave별 몬스터 및 기타 사물------ //
    [SerializeField] private GameObject[] Wave1_Monsters;
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
    [SerializeField] private GameObject EventBtn;
    
    // ------ Wave trigger Collider------ //
    [SerializeField] private BoxCollider Area1;
    [SerializeField] private BoxCollider Area2;
    public bool Area3;        //Wave3의 경우 해당 변수 true && Area2 일시 진행
    [SerializeField] private bool Peiz3Start;  
    [SerializeField] private bool Peiz3Monster2UpdateControl;   
    public bool Wave2MonsterClear;        //wave2몬스터를 모두 잡았는지
    [SerializeField] private GameObject WaveArea3Scrit;
    [SerializeField] private GameObject WaveArea3Barrier;
    
    
    //스택 몬스터
    [SerializeField] private GameObject[] Stack;
    [SerializeField] private int StackIndex;        //스택 내 몬스터의 개수,, top
    public int Gauge;                               //스택 몬스터를 잡는 게이지
    [SerializeField] private GameObject wave2Gauge;
    [SerializeField] private GameObject wave3Gauge;
    
    
    //튜토리얼 패널들
    [SerializeField] private GameObject WelcomePanel;
    [SerializeField] private Button WelcomPanel_Btn;
    [SerializeField] private GameObject[] WelcomePanel_Text;
    [SerializeField] private int WelcomPanel_Text_Number;
    
    //페이즈2 시작시 패널
    [SerializeField] private GameObject Peiz2StartPanel;
    [SerializeField] private Button Peiz2StartPanel_Btn;
    [SerializeField] private GameObject[] Peiz2StartPanel_Text;
    [SerializeField] private int Peiz2StartPanel_Text_Number;
    
    //페이즈2 종료시 패널
    [SerializeField] private GameObject Peiz2EndPanel;
    [SerializeField] private Button Peiz2EndPanel_Btn;
    [SerializeField] private GameObject[] Peiz2EndPanel_Text;
    [SerializeField] private int Peiz2EndPanel_Text_Number;
    
    //컴파일러 고친다음 패널 (컴파일러 고치는 동안 디펜스)
    [SerializeField] private GameObject Start3PeizPanel ;
    [SerializeField] private Button Start3PeizPanel_Btn;
    [SerializeField] private GameObject[] Start3PeizPanel_Text;
    [SerializeField] private int Start3PeizPanel_Text_Number;

   
    
    //개발자 어 금지 패널 3페이즈 Big monster등장
    [SerializeField] private GameObject AfterCompilerPanel ;
    [SerializeField] private Button AfterCompilerPanel_Btn;
    [SerializeField] private GameObject[] AfterCompilerPanel_Text;
    [SerializeField] private int AfterCompilerPanel_Text_Number;

    [SerializeField] private bool isPause;  //현재 게임 시간이 멈췄는지

    [Header("Enemies")]
    public GameObject[] enemies;  //현재 스테이지의 몬스터, Length로 개수를 구할 수 있음

    
    // Start is called before the first frame update
    void Start()
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

        Bomber_Skill_WarheadKind = 4;
        Bomber_Skill_WarheadColor = 0;
        Bomber_Skill_DamageCounting = 1;
        Turret_Skill_BulletColor = 0;
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
        
        //패널 비활성화
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
        //스택몬스터 20 게이지 채우면 모두 삭제
        if (Gauge >= 2 && Wave2MonsterClear == false)
            Clear_Wave2_Monsters();

        //Area3는 Peiz3Gauge에서 True로 변경
        if (Area3 == true && Peiz3Start == false)
        {
            Peiz3Start = true;
            Peiz3Monster_1.SetActive(true);  //3페이즈 몬스터1 등장
            //Start_AfterCompilerPanel();    //컴파일러 고친 뒤 패널 등장
            Start_Panel(AfterCompilerPanel,AfterCompilerPanel_Btn,AfterCompilerPanel_Text,AfterCompilerPanel_Text_Number,false);
        }
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("태그가 'Enemy'인 게임 오브젝트의 개수: " + enemies.Length);
    }

    //1페이즈 시작
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
    public void Area2Function()
    {
        //2페이즈 시작
        if (Area3 == false)
        {
            foreach (GameObject d in Wave1_Directions)
            {
                d.SetActive(false);
            }
            foreach (GameObject g in Wave2_Monsters)
            {
                g.SetActive(true);
                g.GetComponent<Enemy>().startNav();
            }
            foreach (GameObject g in Wave2_Monsters_Spawner)
            {
                g.SetActive(true);
            }
            wave2Gauge.SetActive(true);

            //Start_Peiz2StartPanel();  //2페이즈 패널 등장
            Start_Panel(Peiz2StartPanel, Peiz2StartPanel_Btn, Peiz2StartPanel_Text, Peiz2StartPanel_Text_Number, true);
        }
        else        //3페이즈 시작
        {
            Debug.LogError("3페이즈 시작");
            //다리를 연결
            foreach (GameObject g in Before3Peiz)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in After3Peiz)
            {
                g.SetActive(true);
            }
            //길목 제거
            WaveArea3Scrit.SetActive(false);
            WaveArea3Barrier.SetActive(false);
            //EventBtn 비활성화
            EventBtn.SetActive(false);
            Peiz3Monster_2.SetActive(true); 
        }
        
        
    }

    //적을 죽인 경우
    public void AddStackMonster(GameObject g)
    {
        //처음 들어온 몬스터인경우
        if (StackIndex == 0)
        {
            Stack[StackIndex] = g;
            StackIndex++;
            return;
        }
        else if (StackIndex >= 10) //스택이 꽉 찼는데 몬스터가 죽은 경우
        {
            if (g.GetComponent<Parenthesis>().identity == Stack[9].GetComponent<Parenthesis>().identity)
            {
                Stack[9].GetComponent<Parenthesis>().HitTheMonster(); //몬스터 삭제
                Stack[9] = null; //스택 pop
                StackIndex = 9; //인덱스 줄이기
            }
            else
            {
                g.GetComponent<Parenthesis>().NotDeath();
            }
        }
        else
        {
            Stack[StackIndex] = g;
            StackIndex++;
            if (CheckParenthesis()) //괄호가 맞아 떨어진 경우
            {
                for (int i = 0; i < 2; i++)
                {
                    Stack[StackIndex - 1].GetComponentInChildren<Parenthesis>().HitTheMonster(); //스택에서 몬스터 삭제
                    Stack[StackIndex - 1] = null; //스택 pop
                    StackIndex--; //인덱스 줄이기
                }

                Gauge++; //스택 게이지증가
                wave2Gauge.GetComponent<HealthBar>().SetHealth(Gauge);
            }
        }
        
       
    }

    //괄호의 유효성 검사
    private bool CheckParenthesis()
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

    //2페이즈 끝
    public void Clear_Wave2_Monsters()
    {
        Wave2MonsterClear = true;
        //모든 스포너 생성중단
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

        //Start_Peiz2EndPanel(); //2페이즈 끝난 패널 등장
        Start_Panel(Peiz2EndPanel,Peiz2EndPanel_Btn,Peiz2EndPanel_Text,Peiz2EndPanel_Text_Number,false);
    }

    public void AddStackMonster_In_Array(GameObject m)
    {
        Wave2_Monsters.Add(m);
    }

    //3페이즈 방향
    public void OnWave3Direction()
    {
        foreach (GameObject g in Wave3_Directions)
        {
            g.SetActive(true);
        }
        Wave3_Block_Directions.SetActive(false);
    }
    
    private void Start_Panel(GameObject panal, Button btn, GameObject[] text, int number, bool timestop)
    {
        if (timestop == true)
        {
            Time.timeScale = 0;    //게임 일시정지
            isPause = true;
        }
        btn.onClick.AddListener(() => NextText(panal, text, ref number));
        panal.SetActive(true);    //패널등장
        text[number].SetActive(true);      //첫 패널 메세지 등장
    }

    //스테이지 첫 패널 등장 함수
    private void Start_WelcomePanel()
    {
        Time.timeScale = 0;    //게임 일시정지
        isPause = true;
        WelcomPanel_Btn.onClick.AddListener(() => NextText(WelcomePanel,WelcomePanel_Text, ref WelcomPanel_Text_Number));
        WelcomePanel.SetActive(true);    //패널등장
        WelcomePanel_Text[WelcomPanel_Text_Number].SetActive(true);      //첫 패널 메세지 등장
    }
    
    /*private void Start_Peiz2StartPanel()
    {
        Time.timeScale = 0;    //게임 일시정지
        isPause = true;
        Peiz2StartPanel_Btn.onClick.AddListener(() => NextText(Peiz2StartPanel_Text, ref Peiz2StartPanel_Text_Number));
        Peiz2StartPanel.SetActive(true);    //패널등장
        Peiz2StartPanel_Text[Peiz2StartPanel_Text_Number].SetActive(true);      //첫 패널 메세지 등장
    }
    
    private void Start_Peiz2EndPanel()
    {
        Peiz2EndPanel_Btn.onClick.AddListener(() => NextText(Peiz2EndPanel_Text, ref Peiz2EndPanel_Text_Number));
        Peiz2EndPanel.SetActive(true);    //패널등장
        Peiz2EndPanel_Text[Peiz2EndPanel_Text_Number].SetActive(true);      //첫 패널 메세지 등장
    }
    
    private void Start_AfterCompilerPanel()
    {
        AfterCompilerPanel_Btn.onClick.AddListener(() => NextText(AfterCompilerPanel_Text, ref AfterCompilerPanel_Text_Number));
        AfterCompilerPanel.SetActive(true);    //패널등장
        AfterCompilerPanel_Text[AfterCompilerPanel_Text_Number].SetActive(true);      //첫 패널 메세지 등장
    }*/

    //스테이지 첫 패널의 텍스트를 넘기는 함수 
    private void NextText(GameObject panal,GameObject[] TextArray, ref int TextIndex)
    {
        if (TextArray[TextIndex].activeSelf)  //다음 패널 메세지 등장
        {
            TextArray[TextIndex].SetActive(false);
            TextIndex++;
            if (TextIndex >= TextArray.Length)     //마지막 패널 메세지라면
            {
                panal.SetActive(false);
                if (isPause == true)    //시간이 멈췄다면 
                {
                    Time.timeScale = 1;  //시간되돌리기
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
    
}
