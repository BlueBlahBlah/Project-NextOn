using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwordStatic : MonoBehaviour
{
    //[SerializeField] private DamageManager DamageManager;
    [SerializeField] private Collider collider;
    public int attackNum;     //? íš¨?€?Ÿìˆ˜
    [SerializeField] private int SkillTime;     //?¤í‚¬?€??    [SerializeField] private float findDistance;     //?¤í‚¬ê±°ë¦¬
    [SerializeField] private GameObject Effect;     //?´í™??    public int Damage;
    
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Skill;
    
    public float ThisCoolTime;            //?„ì¬ ë¬´ê¸°???Œì•„ê°€ê³??ˆëŠ” ì¿¨í???    public float SkillCoolTime;           //?„ì¬ ë¬´ê¸° ?¤í‚¬??ì´?ì¿¨í???    [SerializeField] private float SkillCoolTimeRate;       //PlayerManager?ì„œ ê°€?¸ì˜¤??ì¿¨í???ê°ì†Œ??    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        SkillTime = 3;      //ê¸°ë³¸ 3ë²??•ê?
        findDistance = 5f;      //?¤í‚¬ë°˜ê²½ 5f
        Damage = 1;
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //?„ì¬ ë¬´ê¸°??ì¿¨í??„ì„ 10ì´ˆë¡œ ì´ˆê¸°??
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;
        Btn.interactable = true;        //ì²˜ìŒ?ëŠ”(ë¨¹ìë§ˆì) ?¤í‚¬ ?¬ìš©ê°€??    }
    
    private void OnEnable()
    {
        // ë²„íŠ¼ ?´ë¦­ ?´ë²¤???±ë¡
        Btn.onClick.AddListener(SkillSpawn);
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;               //ì¿¨í???ì´ˆê¸°??        
        Btn.interactable = true;        //ì²˜ìŒ?ëŠ”(ë¨¹ìë§ˆì) ?¤í‚¬ ?¬ìš©ê°€??    }
    
    void SkillSpawn()
    {
        // ?„ì¬ ?¤ë¸Œ?íŠ¸ê°€ ë°”ë¼ë³´ëŠ” ë°©í–¥???»ê¸° ?„í•´ transform.forward ?¬ìš©
        Vector3 direction = Player.transform.forward.normalized;

        // ?ˆë¡œ???„ì¹˜ë¥??„ì¬ ?„ì¹˜ + (ë°”ë¼ë³´ëŠ” ë°©í–¥ * ê±°ë¦¬) ë¡??¤ì •
        Vector3 skillPosition = transform.position + (direction * 15f) + (Vector3.up * 5f) + (Vector3.right * 5f);

        Instantiate(Skill, skillPosition, Quaternion.Euler(0,90,0));
        Btn.interactable = false;       //?¤í‚¬ ?¬ìš©???¤ìŒ ì¿¨í??„ê¹Œì§€ ë²„íŠ¼ ? ê¸ˆ
        ThisCoolTime = SkillCoolTime;   //ì¿¨í????ê?
    }

    // Update is called once per frame
    void Update()
    {
        if (attackNum >= 3)         //3?€ ?´ìƒ
        {
            attackNum = 0;          //?€??ì´ˆê¸°??            swordSkill();           //?¤í‚¬
        }
        
        if (ThisCoolTime > 0)                  //ì¿¨í??„ì´ 0ë³´ë‹¤ ?´ë•Œ (ì¿¨ì´ ?¨ì•„?ˆëŠ” ê²½ìš°)
        {
            ThisCoolTime -= Time.deltaTime;     //ì¿¨í???ê°ì†Œ
        }
        else if (ThisCoolTime <= 0)           //ì¿¨í??„ì´ 0?¼ë•Œ 
        {
            Btn.interactable = true;         //?¤í‚¬ ?¬ìš© ê°€??        }
        
    }

   

    private void swordSkill()
    {
        List<GameObject> nearEnemy = FindRandomEnemy();
        //?¤í‚¬ ê³„ìˆ˜ ì¶”ê?
        int TempDamage =  DamageManager.Instance.SwordStatic_Passive_DamageCounting * Damage;

        int numEnemNear = nearEnemy.Count;
        if (numEnemNear == 1)       //ì£¼ë????¤ë¥¸ ëª¬ìŠ¤?°ê? ?†ì„??        {
            Instantiate(Effect, nearEnemy[0].transform.position, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
        }
        else            //?¤ë¥¸ ëª¬ìŠ¤?°ê? ì£¼ë??????ˆì„??        {
            // ì²«ë²ˆì§??ê³¼ ?ê¸°?ì‹ ??ì¤‘ê°„ ì§€??ê³„ì‚° ???´í™???ì„±
            Vector3 middlePoint = (nearEnemy[0].transform.position + this.gameObject.transform.position) / 2f;
            Instantiate(Effect, middlePoint, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
            //?¤ë¥¸ ?ë“¤ ?¬ì´?ë„ ?´í™??            for (int i = 1; i < numEnemNear - 1; i++)
            {
                middlePoint = (nearEnemy[i - 1].transform.position + nearEnemy[i].transform.position) / 2f;
                Instantiate(Effect, middlePoint, Quaternion.identity);
                nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
            }
            nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
        }
    }
    
    private List<GameObject> FindRandomEnemy()
    {
        //?¤í‚¬ ë°˜ê²½??ì½œë¼?´ë” ê°€?¸ì˜´
        Collider[] colliders = Physics.OverlapSphere(transform.position, findDistance);
        
        // "Enemy" ?œê·¸ë¥?ê°€ì§??¤ë¸Œ?íŠ¸ë¥?ë°°ì—´ë¡??¤í‚¬?€??ë§Œí¼ ê°€?¸ì˜´
        //GameObject[] enemies = new GameObject[SkillTime]; //= GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemies = new List<GameObject>();
        int j = 0;
            
        foreach (Collider e in colliders)
        {
            if (e.CompareTag("Enemy"))
            {
                //enemies[j] = e.gameObject;
                enemies.Add(e.gameObject);
                j++;
                if(j >= SkillTime)
                    break;
            }
        }
        // ë§Œì•½ enemies ë°°ì—´??ë¹„ì–´ ?ˆë‹¤ë©?null??ë°˜í™˜
        if (enemies.Count == 0)
        {
            return null;
        }
        else
        {
            return enemies;
        }
    }
}
