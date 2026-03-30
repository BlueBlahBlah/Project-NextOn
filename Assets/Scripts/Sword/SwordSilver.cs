using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSilver : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Effect;
    [SerializeField] private float attackRadius;  
    [SerializeField] private Button Btn;
    public int Damage;
    
    public float ThisCoolTime;            
    public float SkillCoolTime;           
    [SerializeField] private float SkillCoolTimeRate;       
    
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        attackRadius = 20f;
        Damage = 1;
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        
        ThisCoolTime = 0;
        Btn.interactable = true;        
    }
    
    private void OnEnable()
    {
        Btn.onClick.AddListener(SpawnRock);
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        
        ThisCoolTime = 0;
        Btn.interactable = true;        
    }

    void Update()
    {
        if (ThisCoolTime > 0)                  
        {
            ThisCoolTime -= Time.deltaTime;     
        }
        else if (ThisCoolTime <= 0)           
        {
            Btn.interactable = true;         
        }
    }
    
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
            }
        }

        if (enemyList.Count == 0)       
        {
            return Vector3.zero;
        }

        int i = Random.Range(0, enemyList.Count);
        Vector3 enemyPosition = enemyList[i].gameObject.transform.position;
        directionToEnemy = (enemyPosition - transform.position).normalized;

        return directionToEnemy;
    }

    void SpawnRock()
    {
        Vector3 directionToEnemy = findNearEnemy();
        directionToEnemy.y = 0f;
        if (directionToEnemy != Vector3.zero)
        {
            GameObject effectInstance = Instantiate(Effect, transform.position, Quaternion.identity);
            effectInstance.transform.forward = directionToEnemy;
        }
        
        Btn.interactable = false;       
        ThisCoolTime = SkillCoolTime;   
    }
}
