using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance = null;
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
    public static MonsterManager Instance
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

    [Header("StageMonster Number")]    //스테이지 몬스터 총 개수
    public int TotalMonsters;
    
    
    [Header("Stack")]//스택 몬스터
    [SerializeField] private GameObject[] Stack;
    [SerializeField] private int StackIndex;        //스택 내 몬스터의 개수,, top
    public int Gauge;                               //스택 몬스터를 잡는 게이지
    [SerializeField] private GameObject ParenthesisGauge;
    
    [Header("Parenthesis_Monsters")]    //스택 몬스터 관련
    public List<GameObject> Parenthesis_Monsters;
    [SerializeField] private GameObject[] Parenthesis_Monsters_Spawner ;
    
    [Header("First_Monster")]    //첫 조우 몬스터 관련
    public List<GameObject> First_Monsters;
    public bool First_Monsters_Clear;   //첫 조우 몬스터 모두 처치되었는지
    
    [Header("Second_Monster")]    //첫 조우 몬스터 관련
    public List<GameObject> Second_Monsters;
    public bool Second_Monsters_Clear;   //첫 조우 몬스터 모두 처치되었는지
    
    // Start is called before the first frame update
    void Start()
    {
        TotalMonsters = 0;
        Stack = new GameObject[10];
        StackIndex = 0;
        Gauge = 0;
        First_Monsters_Clear = false;
        Second_Monsters_Clear = false;
        
        //첫 조우 몬스터 비활성화
        foreach (GameObject E in First_Monsters)
        {
            E.SetActive(false);
        }
        foreach (GameObject E in Second_Monsters)
        {
            E.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
         GameObject[] enemies;  //현재 스테이지의 몬스터, Length로 개수를 구할 수 있음
         enemies = GameObject.FindGameObjectsWithTag("Enemy");
         TotalMonsters = enemies.Length;

         if (First_Monsters_Clear == false)
         {
             bool allMonstersDestroyed = true;
             foreach (GameObject monster in First_Monsters)
             {
                 if (monster != null)
                 {
                     allMonstersDestroyed = false;
                     break;
                 }
             }
             if (allMonstersDestroyed == true)
             {
                 First_Monsters_Clear = true;
                 EventManager.Instance.PrintMSG();      //다음대화로
             }
         }

         if (Second_Monsters_Clear == false)
         {
             bool allMonstersDestroyed = true;
             foreach (GameObject monster in Second_Monsters)
             {
                 if (monster != null)
                 {
                     allMonstersDestroyed = false;
                     break;
                 }
             }
             if (allMonstersDestroyed == true)
             {
                 Second_Monsters_Clear = true;
                 //EventManager.Instance.PrintMSG();      //다음대화로
                 //두번째 몬스터들(스킬사용부분)은 모두 처치해도 대화창 등장하지 않게
             }
         }
         
    }
    
    //적을 죽인 경우(스택에 추가)
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
                ParenthesisGauge.GetComponent<HealthBar>().SetHealth(Gauge);
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
        //Wave2MonsterClear = true;
        //모든 스포너 생성중단
        foreach (GameObject g in Parenthesis_Monsters_Spawner)
        {
            g.GetComponent<Wave2StackMonsterSpawner>().Active = false;
            g.SetActive(false);
        }

        ParenthesisGauge.GetComponent<HealthBar>().ClearWave2();
        Invoke("ClearWave2MonsterInvoke",3);
    }

    //2페이즈 끝난 후 모든 스택몬스터 삭제
    private void ClearWave2MonsterInvoke()
    {
        for (int i = Parenthesis_Monsters.Count-1; i >= 0; i--)
        {
            try
            {
                if(Parenthesis_Monsters[i] != null)
                    Parenthesis_Monsters[i].GetComponentInChildren<Parenthesis>().ClearTheMonster();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.LogError(i);
                throw;
            }
        }
        ParenthesisGauge.SetActive(false);
        
    }
    
    //현재 몬스터목록에 추가
    public void AddStackMonster_In_Array(GameObject m)
    {
        Parenthesis_Monsters.Add(m);
    }

    //첫 조우 몬스터 조작 함수
    public void Appearance_First_Monster()
    {
        //활성화 후 움직임
        foreach (GameObject E in First_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }
    
    //두번째 조우 몬스터 조작 함수
    public void Appearance_Seconde_Monster()
    {
        //활성화 후 움직임
        foreach (GameObject E in Second_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }
}
