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
            //???´ë˜???¸ìŠ¤?´ìŠ¤ê°€ ?„ìƒ?ˆì„ ???„ì—­ë³€??instance??ê²Œì„ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤ê°€ ?´ê²¨?ˆì? ?Šë‹¤ë©? ?ì‹ ???£ì–´ì¤€??
            instance = this;

            //???„í™˜???˜ë”?¼ë„ ?Œê´´?˜ì? ?Šê²Œ ?œë‹¤.
            //gameObjectë§Œìœ¼ë¡œë„ ???¤í¬ë¦½íŠ¸ê°€ ì»´í¬?ŒíŠ¸ë¡œì„œ ë¶™ì–´?ˆëŠ” Hierarchy?ì˜ ê²Œì„?¤ë¸Œ?íŠ¸?¼ëŠ” ?»ì´ì§€ë§? 
            //?˜ëŠ” ?·ê°ˆë¦?ë°©ì?ë¥??„í•´ thisë¥?ë¶™ì—¬ì£¼ê¸°???œë‹¤.
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //ë§Œì•½ ???´ë™???˜ì—ˆ?”ë° ê·??¬ì—??Hierarchy??GameMgr??ì¡´ì¬???˜ë„ ?ˆë‹¤.
            //ê·¸ëŸ´ ê²½ìš°???´ì „ ?¬ì—???¬ìš©?˜ë˜ ?¸ìŠ¤?´ìŠ¤ë¥?ê³„ì† ?¬ìš©?´ì£¼??ê²½ìš°ê°€ ë§ì? ê²?ê°™ë‹¤.
            //ê·¸ë˜???´ë? ?„ì—­ë³€?˜ì¸ instance???¸ìŠ¤?´ìŠ¤ê°€ ì¡´ì¬?œë‹¤ë©??ì‹ (?ˆë¡œ???¬ì˜ GameMgr)???? œ?´ì???
            //Destroy(this.gameObject);
        }
    }

    //ê²Œì„ ë§¤ë‹ˆ?€ ?¸ìŠ¤?´ìŠ¤???‘ê·¼?????ˆëŠ” ?„ë¡œ?¼í‹°. static?´ë?ë¡??¤ë¥¸ ?´ë˜?¤ì—??ë§˜ê» ?¸ì¶œ?????ˆë‹¤.
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

    [Header("StageMonster Number")] //?¤í…Œ?´ì? ëª¬ìŠ¤??ì´?ê°œìˆ˜
    GameObject[] enemies; //?„ì¬ ?¤í…Œ?´ì???ëª¬ìŠ¤?? Lengthë¡?ê°œìˆ˜ë¥?êµ¬í•  ???ˆìŒ

    public int TotalMonsters;


    [Header("Stack")] //?¤íƒ ëª¬ìŠ¤??    //[SerializeField] private GameObject[] Stack;
    //[SerializeField] private int StackIndex;        //?¤íƒ ??ëª¬ìŠ¤?°ì˜ ê°œìˆ˜,, top
    public int Gauge; //?¤íƒ ëª¬ìŠ¤?°ë? ?¡ëŠ” ê²Œì´ì§€

    [SerializeField] private GameObject ParenthesisGauge;

    [Header("First_Monster")] //ì²?ì¡°ìš° ëª¬ìŠ¤??ê´€??    public List<GameObject> First_Monsters;

    public bool First_Monsters_Clear; //ì²?ì¡°ìš° ëª¬ìŠ¤??ëª¨ë‘ ì²˜ì¹˜?˜ì—ˆ?”ì?

    [Header("Second_Monster")] //?ë²ˆì§?ì¡°ìš° ëª¬ìŠ¤??ê´€??    public List<GameObject> Second_Monsters;

    public bool Second_Monsters_Clear; //?ë²ˆì§?ì¡°ìš° ëª¬ìŠ¤??ëª¨ë‘ ì²˜ì¹˜?˜ì—ˆ?”ì?

    [Header("Third_Monster")] //?¸ë²ˆì§?ê·¼ì ‘ë¬´ê¸° ë¨¹ê³ ???? ì¡°ìš° ëª¬ìŠ¤??ê´€??    public List<GameObject> Third_Monsters;

    public bool Third_Monsters_Clear; //?¸ë²ˆì§?ê·¼ì ‘ë¬´ê¸° ë¨¹ê³ ???? ì¡°ìš° ëª¬ìŠ¤??ëª¨ë‘ ì²˜ì¹˜?˜ì—ˆ?”ì?

    [Header("Parenthesis_Monster")] //ê´„í˜¸ ëª¬ìŠ¤??ê´€??    public List<GameObject> Parenthesis_Monster_Spawner; //ê´„í˜¸ëª¬ìŠ¤???¤í¬??
    public List<GameObject> Parenthesis_Monsters; //?„ë“œ???ˆëŠ” ê´„í˜¸ ëª¬ìŠ¤?°ë“¤
    [SerializeField] private GameObject Small_Parenthesis_Monster;
    [SerializeField] private GameObject Medium_Parenthesis_Monster;
    [SerializeField] private GameObject Big_Parenthesis_Monster;

    [SerializeField] private List<GameObject> Semicolon_Monsters;

    public List<GameObject> Semicolon_Monster_Spawner; //?¸ë?ì½œë¡ ëª¬ìŠ¤???¤í¬??    public bool FinalPeiz; //ë§ˆì?ë§??˜ì´ì¦??´ë‹¹ ë³€?˜ê? Trueë©?ê³„ì† ëª¬ìŠ¤???ì„±

    // Start is called before the first frame update
    void Start()
    {
        TotalMonsters = 0;
        //Stack = new GameObject[10];
        //StackIndex = 0;
        Gauge = 0;
        First_Monsters_Clear = false;
        Second_Monsters_Clear = false;
        Third_Monsters_Clear = false;

        //ì²?ì¡°ìš° ëª¬ìŠ¤??ë¹„í™œ?±í™”
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

    // Update is called once per frame
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
                EventManager.Instance.PrintMSG(); //?¤ìŒ?€?”ë¡œ
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
                EventManager.Instance.PrintMSG();      //?¤ìŒ?€?”ë¡œ
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

    //?„ì¬ ?„ë“œ ëª¬ìŠ¤?°ë“¤???™ì‘??ë©ˆì¶”???¨ìˆ˜
    public void MonsterTimeStop()
    {
        foreach (GameObject m in enemies)
        {
            Animator animator = m.GetComponent<Animator>();
            Enemy enemy = m.GetComponent<Enemy>();
            if (animator != null) //? ë‹ˆë©”ì´??ë©ˆì¶”ê³?            {
                animator.speed = 0;
            }

            if (enemy != null) //?´ë™??ë©ˆì¶”ê¸?            {
                enemy.StopNav();
            }
        }

    }

    //?„ì¬ ?„ë“œ ëª¬ìŠ¤?°ë“¤???™ì‘???¬ê°œ?˜ëŠ” ?¨ìˆ˜
    public void MonsterTimeResume()
    {
        foreach (GameObject m in enemies)
        {
            Animator animator = m.GetComponent<Animator>();
            Enemy enemy = m.GetComponent<Enemy>();
            if (animator != null) //? ë‹ˆë©”ì´??ë©ˆì¶”ê³?            {
                animator.speed = 1;
            }

            if (enemy != null) //?´ë™??ë©ˆì¶”ê¸?            {
                enemy.StartNav();
            }
        }

    }

    //?ì„ ì£½ì¸ ê²½ìš°(?¤íƒ??ì¶”ê?)
    /*public void AddStackMonster(GameObject g)
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
                ParenthesisGauge.GetComponent<HealthBar>().SetHealth(Gauge);
            }
        }

    }*/

    //ê´„í˜¸??? íš¨??ê²€??    /*private bool CheckParenthesis()
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
    }*/

    //ê´„í˜¸ ëª¬ìŠ¤??ê²Œì´ì§€ ì±„ì›Œ??ëª¨ë“  ê´„í˜¸ ëª¬ìŠ¤??ì²˜ì¹˜
    /*public void Clear_Wave2_Monsters()
    {
        //Wave2MonsterClear = true;
        //ëª¨ë“  ?¤í¬???ì„±ì¤‘ë‹¨
        foreach (GameObject g in Parenthesis_Monster_Spawner)
        {
            g.GetComponent<Wave2StackMonsterSpawner>().Active = false;
            g.SetActive(false);
        }

        ParenthesisGauge.GetComponent<HealthBar>().ClearWave2();
        Invoke("ClearWave2MonsterInvoke",3);
    }*/

    //2?˜ì´ì¦??ë‚œ ??ëª¨ë“  ?¤íƒëª¬ìŠ¤???? œ
    /*private void ClearWave2MonsterInvoke()
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

    }*/

    //?„ì¬ ëª¬ìŠ¤?°ëª©ë¡ì— ì¶”ê?
    /*public void AddStackMonster_In_Array(GameObject m)
    {
        Parenthesis_Monsters.Add(m);
    }*/

    //ì²?ì¡°ìš° ëª¬ìŠ¤??ì¡°ì‘ ?¨ìˆ˜
    public void Appearance_First_Monster()
    {
        //?œì„±?????€ì§ì„
        foreach (GameObject E in First_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    //?ë²ˆì§?ì¡°ìš° ëª¬ìŠ¤??ì¡°ì‘ ?¨ìˆ˜
    public void Appearance_Second_Monster()
    {
        //?œì„±?????€ì§ì„
        foreach (GameObject E in Second_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    //?¸ë²ˆì§?ì¡°ìš° ëª¬ìŠ¤??ì¡°ì‘ ?¨ìˆ˜
    public void Appearance_Third_Monster()
    {
        //?œì„±?????€ì§ì„
        foreach (GameObject E in Third_Monsters)
        {
            E.SetActive(true);
            E.GetComponent<Enemy>().SetNavSpeed(3.5f);
        }
    }

    //?¸ë²ˆì§?ì¡°ìš° ëª¬ìŠ¤??ì¡°ì‘ ?¨ìˆ˜
    public void Appearance_Parenthesis_Monster()
    {
        //?œì„±?????€ì§ì„
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

    //?¨ì–´ì§€??ëª¬ìŠ¤?°ì— Navë¥?start?˜ëŠ”  LastPeizSpawnMonsterMeshControl ë¥?ì¶”ê??˜ëŠ” ?¨ìˆ˜
    private T InitComponent<T>(GameObject gameObject) where T : MonoBehaviour
    {
        return gameObject.AddComponent<T>();
    }

    //ê´„í˜¸ëª¬ìŠ¤?°ë? ?ì„±?˜ëŠ” ì½”ë“œ
    public void Spawn_Parenthesis()
    {
        //ê´„í˜¸ëª¬ìŠ¤?°ë? ?ì„±?????¤í¬???œë¤ì§€??        int spawner1 = Random.Range(0, Parenthesis_Monster_Spawner.Count),
            spawner2 = Random.Range(0, Parenthesis_Monster_Spawner.Count);
        //?ì„±??ê´„í˜¸ëª¬ìŠ¤??ì¢…ë¥˜
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

        //?ì„±
        GameObject Monster1 = Instantiate(Spawn_Monster, Parenthesis_Monster_Spawner[spawner1].transform.position,
            Quaternion.identity);
        GameObject Monster2 = Instantiate(Spawn_Monster, Parenthesis_Monster_Spawner[spawner2].transform.position,
            Quaternion.identity);

        //?°ê²°
        Monster1.GetComponent<Parenthesis>().Set_Mate_Monster(Monster2);
        Monster2.GetComponent<Parenthesis>().Set_Mate_Monster(Monster1);

        //LastPeizMosnterNavOff(Monster1);
        //LastPeizMosnterNavOff(Monster2);

        //TODO
        //?´í™???°ê²°?˜ê¸°

        if (FinalPeiz == true)
        {
            //?¬ê??¸ì¶œ
            Invoke("Spawn_Parenthesis", 10f);
        }

    }

    public void Spawn_Semicolon()
    {
        //5ë§ˆë¦¬ ?™ì‹œ ?ì„±
        for (int i = 0; i < 3; i++)
        {
            int spawnerNum = Random.Range(0, Semicolon_Monster_Spawner.Count);
            int monsterNum = Random.Range(0, Semicolon_Monsters.Count);

            GameObject Monster = Instantiate(Semicolon_Monsters[monsterNum],
                Semicolon_Monster_Spawner[spawnerNum].transform.position,
                Quaternion.identity);
            //LastPeizMosnterNavOff(Monster);
        }
        

        if (FinalPeiz == true)
        {
            //?¬ê??¸ì¶œ
            Invoke("Spawn_Semicolon", 10f);
        }
    }

    //ë§ˆì?ë§??˜ì´ì¦?ëª¬ìŠ¤??ê³µì¤‘ë¶€??ë°©ì?q
    private void LastPeizMosnterNavOff(GameObject m)
    {
        m.GetComponent<Enemy>().StopNav();                      //?¤ë¹„ê²Œì´??ì¢…ë£Œ
        InitComponent<LastMonsterNavCont>(m);                   //ì°©ì? ?ì • ?¤í¬ë¦½íŠ¸ ì¶”ê?
        m.GetComponent<NavMeshAgent>().enabled = false;         //NavMeshAgentë¹„í™œ?±í™”
    }

}
