using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordStreamOfEdge : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Skill;
    [SerializeField] private Button Btn;
    [SerializeField] private GameObject Player;
    public int Damage;

    public float ThisCoolTime;            
    public float SkillCoolTime;           
    [SerializeField] private float SkillCoolTimeRate;       
    
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        collider.enabled = false;
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

    void SkillSpawn()
    {
        Vector3 direction = Player.transform.forward.normalized;
        Vector3 skillPosition = transform.position + (direction * 10f) + (Vector3.up * 5f);
        skillPosition.y = Player.transform.position.y + 1;

        Instantiate(Skill, skillPosition, Player.transform.rotation);

        Btn.interactable = false;       
        ThisCoolTime = SkillCoolTime;   

    }
}
