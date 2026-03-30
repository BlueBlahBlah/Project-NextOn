using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwordStatic : MonoBehaviour
{
    [SerializeField] private Collider collider;
    public int attackNum;     
    [SerializeField] private int SkillTime;     
    [SerializeField] private float findDistance;     
    [SerializeField] private GameObject Effect;     
    public int Damage;
    
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Skill;
    
    public float ThisCoolTime;            
    public float SkillCoolTime;           
    [SerializeField] private float SkillCoolTimeRate;       
    
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        SkillTime = 3;      
        findDistance = 5f;      
        Damage = 1;
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        
        ThisCoolTime = 0;
        Btn.interactable = true;        
    }
    
    private void OnEnable()
    {
        Btn.onClick.AddListener(SkillSpawn);
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        
        ThisCoolTime = 0;               
        Btn.interactable = true;        
    }
    
    void SkillSpawn()
    {
        Vector3 direction = Player.transform.forward.normalized;
        Vector3 skillPosition = transform.position + (direction * 15f) + (Vector3.up * 5f) + (Vector3.right * 5f);
        Instantiate(Skill, skillPosition, Quaternion.Euler(0,90,0));
        Btn.interactable = false;       
        ThisCoolTime = SkillCoolTime;   
    }

    void Update()
    {
        if (attackNum >= 3)         
        {
            attackNum = 0;          
            swordSkill();           
        }
        
        if (ThisCoolTime > 0)                  
        {
            ThisCoolTime -= Time.deltaTime;     
        }
        else if (ThisCoolTime <= 0)           
        {
            Btn.interactable = true;         
        }
    }

    private void swordSkill()
    {
        List<GameObject> nearEnemy = FindRandomEnemy();
        int TempDamage =  DamageManager.Instance.SwordStatic_Passive_DamageCounting * Damage;

        if (nearEnemy == null || nearEnemy.Count == 0) return;

        int numEnemNear = nearEnemy.Count;
        if (numEnemNear == 1)       
        {
            Instantiate(Effect, nearEnemy[0].transform.position, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
        }
        else            
        {
            Vector3 middlePoint = (nearEnemy[0].transform.position + this.gameObject.transform.position) / 2f;
            Instantiate(Effect, middlePoint, Quaternion.identity);
            nearEnemy[0].GetComponent<Enemy>().CurHealth-= TempDamage;
            
            for (int i = 1; i < numEnemNear - 1; i++)
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, findDistance);
        
        List<GameObject> enemies = new List<GameObject>();
        int j = 0;
            
        foreach (Collider e in colliders)
        {
            if (e.CompareTag("Enemy"))
            {
                enemies.Add(e.gameObject);
                j++;
                if(j >= SkillTime)
                    break;
            }
        }
        
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
