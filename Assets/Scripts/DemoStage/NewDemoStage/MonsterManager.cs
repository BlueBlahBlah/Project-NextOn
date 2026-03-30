using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

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

    [Header("StageMonster Number")] 
    GameObject[] enemies; 

    public int TotalMonsters;

    [Header("Stack")] 
    public int Gauge; 
    [SerializeField] private GameObject ParenthesisGauge;

    [Header("First_Monster")] 
    public List<GameObject> First_Monsters;
    public bool First_Monsters_Clear; 

    [Header("Second_Monster")] 
    public List<GameObject> Second_Monsters;
    public bool Second_Monsters_Clear; 

    [Header("Third_Monster")] 
    public List<GameObject> Third_Monsters;
    public bool Third_Monsters_Clear; 

    [Header("Parenthesis_Monster")] 
    public List<GameObject> Parenthesis_Monster_Spawner; 
    public List<GameObject> Parenthesis_Monsters; 
    [SerializeField] private GameObject Small_Parenthesis_Monster;
    [SerializeField] private GameObject Medium_Parenthesis_Monster;
    [SerializeField] private GameObject Big_Parenthesis_Monster;

    [SerializeField] private List<GameObject> Semicolon_Monsters;

    public List<GameObject> Semicolon_Monster_Spawner; 
    public bool FinalPeiz; 

    void Start()
    {
        TotalMonsters = 0;
        Gauge = 0;
        First_Monsters_Clear = false;
        Second_Monsters_Clear = false;
        Third_Monsters_Clear = false;

        foreach (GameObject E in First_Monsters)
        {
            E.SetActive(false);
        }

        foreach (GameObject E in Second_Monsters)
        {
            E.SetActive(false);
        }

        foreach (GameObject E in Third_Monsters)
        {
            E.SetActive(false);
        }

        foreach (GameObject E in Parenthesis_Monster_Spawner)
        {
            E.SetActive(false);
        }

        foreach (GameObject E in Semicolon_Monster_Spawner)
        {
            E.SetActive(false);
        }

        foreach (GameObject E in Parenthesis_Monsters)
        {
            E.SetActive(false);
        }

        FinalPeiz = false;
    }

    void Update()
    {

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
                EventManager.Instance.PrintMSG(); 
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
                EventManager.Instance.PrintMSG();      
            }
        }

        if (Third_Monsters_Clear == false)
        {
            bool allMonstersDestroyed = true;
            foreach (GameObject monster in Third_Monsters)
            {
                if (monster != null)
                {
                    allMonstersDestroyed = false;
                    break;
                }
            }

            if (allMonstersDestroyed == true)
            {
                Third_Monsters_Clear = true;
                EventManager.Instance.PrintMSG();   
            }
        }

    }

    public void MonsterTimeStop()
    {
        foreach (GameObject m in enemies)
        {
            Animator animator = m.GetComponent<Animator>();
            Enemy enemy = m.GetComponent<Enemy>();
            if (animator != null) 
            {
                animator.speed = 0;
            }

            if (enemy != null) 
            {
                enemy.StopNav();
            }
        }

    }

    public void MonsterTimeResume()
    {
        foreach (GameObject m in enemies)
        {
            Animator animator = m.GetComponent<Animator>();
            Enemy enemy = m.GetComponent<Enemy>();
            if (animator != null) 
            {
                animator.speed = 1;
            }

            if (enemy != null) 
            {
                enemy.StartNav();
            }
        }

    }

    public void Appearance_First_Monster()
    {
        foreach (GameObject E in First_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    public void Appearance_Second_Monster()
    {
        foreach (GameObject E in Second_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    public void Appearance_Third_Monster()
    {
        foreach (GameObject E in Third_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    public void Appearance_Parenthesis_Monster()
    {
        foreach (GameObject E in Parenthesis_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }

        foreach (GameObject E in Parenthesis_Monster_Spawner)
        {
            E.SetActive(true);
        }
    }

    private T InitComponent<T>(GameObject gameObject) where T : MonoBehaviour
    {
        return gameObject.AddComponent<T>();
    }

    public void Spawn_Parenthesis()
    {
        int spawner1 = Random.Range(0, Parenthesis_Monster_Spawner.Count);
        int spawner2 = Random.Range(0, Parenthesis_Monster_Spawner.Count);
        
        int Monster_Kind_Number = Random.Range(0, 3);
        GameObject Spawn_Monster;
        switch (Monster_Kind_Number)
        {
            case 0:
                Spawn_Monster = Small_Parenthesis_Monster;
                break;
            case 1:
                Spawn_Monster = Medium_Parenthesis_Monster;
                break;
            case 2:
                Spawn_Monster = Big_Parenthesis_Monster;
                break;
            default:
                Spawn_Monster = Small_Parenthesis_Monster;
                break;
        }

        GameObject Monster1 = Instantiate(Spawn_Monster, Parenthesis_Monster_Spawner[spawner1].transform.position,
            Quaternion.identity);
        GameObject Monster2 = Instantiate(Spawn_Monster, Parenthesis_Monster_Spawner[spawner2].transform.position,
            Quaternion.identity);

        Monster1.GetComponent<Parenthesis>().Set_Mate_Monster(Monster2);
        Monster2.GetComponent<Parenthesis>().Set_Mate_Monster(Monster1);

        if (FinalPeiz == true)
        {
            Invoke("Spawn_Parenthesis", 10f);
        }
    }

    public void Spawn_Semicolon()
    {
        for (int i = 0; i < 3; i++)
        {
            int spawnerNum = Random.Range(0, Semicolon_Monster_Spawner.Count);
            int monsterNum = Random.Range(0, Semicolon_Monsters.Count);

            GameObject Monster = Instantiate(Semicolon_Monsters[monsterNum],
                Semicolon_Monster_Spawner[spawnerNum].transform.position,
                Quaternion.identity);
        }
        
        if (FinalPeiz == true)
        {
            Invoke("Spawn_Semicolon", 10f);
        }
    }

    private void LastPeizMosnterNavOff(GameObject m)
    {
        m.GetComponent<Enemy>().StopNav();                      
        InitComponent<LastMonsterNavCont>(m);                   
        m.GetComponent<NavMeshAgent>().enabled = false;         
    }
}
