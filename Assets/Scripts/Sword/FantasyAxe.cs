using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FantasyAxe : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Skill;
    [SerializeField] private Button Btn;
    
    public int Damage;
    public float ThisCoolTime;            //?„ì¬ ë¬´ê¸°???Œì•„ê°€ê³??ˆëŠ” ì¿¨í???    public float SkillCoolTime;           //?„ì¬ ë¬´ê¸° ?¤í‚¬??ì´?ì¿¨í???    [SerializeField] private float SkillCoolTimeRate;       //PlayerManager?ì„œ ê°€?¸ì˜¤??ì¿¨í???ê°ì†Œ??    
    // Start is called before the first frame update
    void Start()
    {
        Damage = 1;
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //?„ì¬ ë¬´ê¸°??ì¿¨í??„ì„ 10ì´ˆë¡œ ì´ˆê¸°??
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;
        Btn.interactable = true;        //ì²˜ìŒ?ëŠ”(ë¨¹ìë§ˆì) ?¤í‚¬ ?¬ìš©ê°€??    }
    private void OnEnable()
    {
        // ë²„íŠ¼ ?´ë¦­ ?´ë²¤???±ë¡
        //Debug.LogError("?„ë¼ Enable");
        Btn.onClick.AddListener(SkillSpawn);
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //ì¿¨í??„ì? ê°ì†Œ?¨ì„ ?ìš©??ê°’ìœ¼ë¡?
        ThisCoolTime = 0;               //ì¿¨í???ì´ˆê¸°??        
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

    void SkillSpawn()
    {
        
        Instantiate(Skill, transform.position, Quaternion.identity);
        Btn.interactable = false;       //?¤í‚¬ ?¬ìš©???¤ìŒ ì¿¨í??„ê¹Œì§€ ë²„íŠ¼ ? ê¸ˆ
        ThisCoolTime = SkillCoolTime;   //ì¿¨í????ê?
        
    }
    
   
}
