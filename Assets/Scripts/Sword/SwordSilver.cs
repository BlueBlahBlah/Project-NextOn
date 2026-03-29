using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSilver : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Effect;
    //[SerializeField] private int attackNum;     //? íš¨?€?Ÿìˆ˜
    [SerializeField] private float attackRadius;  //ë°”ìœ„ ë²”ìœ„
    [SerializeField] private Button Btn;
    public int Damage;
    
    public float ThisCoolTime;            //?„ì¬ ë¬´ê¸°???Œì•„ê°€ê³??ˆëŠ” ì¿¨í???    public float SkillCoolTime;           //?„ì¬ ë¬´ê¸° ?¤í‚¬??ì´?ì¿¨í???    [SerializeField] private float SkillCoolTimeRate;       //PlayerManager?ì„œ ê°€?¸ì˜¤??ì¿¨í???ê°ì†Œ??    
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        //attackNum = 0;
        attackRadius = 20f;
        Damage = 1;
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //?„ì¬ ë¬´ê¸°??ì¿¨í??„ì„ 10ì´ˆë¡œ ì´ˆê¸°??
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;
        Btn.interactable = true;        //ì²˜ìŒ?ëŠ”(ë¨¹ìë§ˆì) ?¤í‚¬ ?¬ìš©ê°€??    }
    
    private void OnEnable()
    {
        // ë²„íŠ¼ ?´ë¦­ ?´ë²¤???±ë¡
        Btn.onClick.AddListener(SpawnRock);
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //?„ì¬ ë¬´ê¸°??ì¿¨í??„ì„ 10ì´ˆë¡œ ì´ˆê¸°??
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;
        Btn.interactable = true;        //ì²˜ìŒ?ëŠ”(ë¨¹ìë§ˆì) ?¤í‚¬ ?¬ìš©ê°€??    }

    // Update is called once per frame
    void Update()
    {
        if (ThisCoolTime > 0)                  //ì¿¨í??„ì´ 0ë³´ë‹¤ ?´ë•Œ (ì¿¨ì´ ?¨ì•„?ˆëŠ” ê²½ìš°)
        {
            ThisCoolTime -= Time.deltaTime;     //ì¿¨í???ê°ì†Œ
        }
        else if (ThisCoolTime <= 0)           //ì¿¨í??„ì´ 0?¼ë•Œ 
        {
            Btn.interactable = true;         //?¤í‚¬ ?¬ìš© ê°€??        }
    }
    
    

    //ë²”ìœ„??ë¬´ì‘???ì˜ ë°©í–¥
    private Vector3 findNearEnemy()
    {
        Vector3 directionToEnemy = Vector3.zero;
    
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);
        List<GameObject> enemyList = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyList.Add(collider.gameObject);
                /*// ? íƒ???ì˜ ?„ì¹˜
                Vector3 enemyPosition = collider.gameObject.transform.position;

                // ?„ì¬ ?¤ë¸Œ?íŠ¸?ì„œ ?ìœ¼ë¡??¥í•˜??ë°©í–¥ ë²¡í„° ê³„ì‚°
                directionToEnemy = (enemyPosition - transform.position).normalized;
                break;*/
            }
        }

        if (enemyList.Count == 0)       //ê·¼ì²˜???ì´ ?†ëŠ” ê²½ìš°
        {
            return Vector3.zero;
        }

        int i = Random.Range(0, enemyList.Count);
        //?ì¤‘???œë¤???ì„ ? íƒ
        Vector3 enemyPosition = enemyList[i].gameObject.transform.position;

        // ?„ì¬ ?¤ë¸Œ?íŠ¸?ì„œ ?ìœ¼ë¡??¥í•˜??ë°©í–¥ ë²¡í„° ê³„ì‚°
        directionToEnemy = (enemyPosition - transform.position).normalized;

        return directionToEnemy;
    }

    void SpawnRock()
    {
        Vector3 directionToEnemy = findNearEnemy();
        directionToEnemy.y = 0f;
        // ë°©í–¥ ë²¡í„°ê°€ ? íš¨?œì? ?•ì¸
        if (directionToEnemy != Vector3.zero)
        {
            // Effect ?¤ë¸Œ?íŠ¸ ?ì„±
            GameObject effectInstance = Instantiate(Effect, transform.position, Quaternion.identity);

            // ?ì„±??Effect ?¤ë¸Œ?íŠ¸ë¥?ë°©í–¥?¼ë¡œ ?Œì „?œí‚´
            effectInstance.transform.forward = directionToEnemy;
        }
        
        Btn.interactable = false;       //?¤í‚¬ ?¬ìš©???¤ìŒ ì¿¨í??„ê¹Œì§€ ë²„íŠ¼ ? ê¸ˆ
        ThisCoolTime = SkillCoolTime;   //ì¿¨í????ê?
    }
    
}
