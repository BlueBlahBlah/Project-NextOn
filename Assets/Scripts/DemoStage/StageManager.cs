using System;
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
    public List<GameObject> Wave2_Monsters;
    [SerializeField] private GameObject[] Wave2_Monsters_Spawner;
    [SerializeField] private GameObject[] Wave3_Monsters;
    
    // ------ Wave trigger Collider------ //
    [SerializeField] private BoxCollider Area1;
    [SerializeField] private BoxCollider Area2;
    [SerializeField] private bool Area3;        //Wave3의 경우 해당 변수 true && Area2 일시 진행
    [SerializeField] private bool Wave2MonsterClear;        //wave2몬스터를 모두 잡았는지
    
    
    //스택 몬스터
    [SerializeField] private GameObject[] Stack;
    [SerializeField] private int StackIndex;        //스택 내 몬스터의 개수,, top
    public int Gauge;                               //스택 몬스터를 잡는 게이지
    [SerializeField] private GameObject wave2Gauge;
   
    
    // Start is called before the first frame update
    void Start()
    {
        SwordStreamEdge_DamageCounting = 1;
        SwordStatic_DamageCounting = 1;
        SwordSliver_DamageCounting = 1;
        SwordDemacia_DamageCounting = 1;
        FantasyAxe_DamageCounting = 100;
        
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
        foreach (GameObject g in Wave2_Monsters)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in Wave2_Monsters_Spawner)
        {
            g.SetActive(false);
        }

        Stack = new GameObject[10];
        StackIndex = 0;
        Gauge = 0;
        Wave2MonsterClear = false;
        wave2Gauge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //스택몬스터 20 게이지 채우면 모두 삭제
        if (Gauge >= 20 && Wave2MonsterClear == false)
            Clear_Wave2_Monsters();
        
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
    public void Area2Function()
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

    public void Clear_Wave2_Monsters()
    {
        Wave2MonsterClear = true;
        //모든 스포너 생성중단
        foreach (GameObject g in Wave2_Monsters_Spawner)
        {
            g.GetComponent<Wave2StackMonsterSpawner>().Active = false;
            g.SetActive(false);
        }
        wave2Gauge.SetActive(false);
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
    }

    public void AddStackMonster_In_Array(GameObject m)
    {
        Wave2_Monsters.Add(m);
    }
    
    
}
