using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamOfEdge2 : MonoBehaviour
{
    [SerializeField] private DamageManager DamageManager;
    private Transform[] currentTarget; // 현재 목표 지점
    private float TickTime;       //데미지를 주는 틱 간격
    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        DamageManager = GameObject.Find("DamageManager").GetComponent<DamageManager>();
        TickTime = 0;
        Destroy(gameObject, 10f);
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f,0f);
        // 초기 목표지점 설정
        //currentTarget = Sphere2.transform;
        Damage = 1;    //기본 스킬 데미지
        
    }

    // Update is called once per frame
    void Update()
    {
        TickTime += Time.deltaTime;
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && TickTime >= 0.25f)
        {
            //스킬계수추가
            int TempDamage = DamageManager.SwordStreamEdge_Skill_DamageCounting * Damage;   
            other.GetComponent<Enemy>().curHealth -= TempDamage;
            TickTime = 0;
        }
    }
    
    
}
