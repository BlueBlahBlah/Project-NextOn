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
    public float ThisCoolTime;            
    public float SkillCoolTime;           
    [SerializeField] private float SkillCoolTimeRate;       
    
    void Start()
    {
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
        Instantiate(Skill, transform.position, Quaternion.identity);
        Btn.interactable = false;       
        ThisCoolTime = SkillCoolTime;   
    }
}
