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
    
    // Start is called before the first frame update
    void Start()
    {
        Damage = 5;
    }
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
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
    
   
}
