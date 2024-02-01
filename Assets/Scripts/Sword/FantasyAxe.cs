using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FantasyAxe : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Skill;
    [SerializeField] private Button Btn;
    
    [SerializeField] private int Damage;
    
    // Start is called before the first frame update
    void Start()
    {
        Btn.onClick.AddListener(SkillSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SkillSpawn()
    {
        
        Instantiate(Skill, transform.position, Quaternion.identity);
        
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            Debug.Log("판타지도끼 공격");
        }
    }
}
