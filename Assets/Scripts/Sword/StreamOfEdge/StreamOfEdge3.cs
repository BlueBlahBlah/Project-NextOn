using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamOfEdge3 : MonoBehaviour
{
    [SerializeField] private float degreePerSecond = 20; //회전속도
    private Transform[] currentTarget; // 현재 목표 지점
    private float TickTime;       //데미지를 주는 틱 간격
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
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
        transform.Rotate(Vector3.up * Time.deltaTime * degreePerSecond);      //자제 회전
        TickTime += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && TickTime >= 0.25f)
        {
            //스킬계수추가
            int TempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().SwordStatic_Skill_DamageCounting * Damage;   
            other.GetComponent<Enemy>().curHealth -= TempDamage;
            TickTime = 0;
        }
    }

    
}
