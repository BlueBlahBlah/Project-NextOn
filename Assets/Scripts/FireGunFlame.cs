using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireGunFlame : MonoBehaviour
{
    public float raycastLength = 7f; // 레이의 최대 길이
    private float TickTime;        //데미지를 주는 틱 간격
    public int Damage;
    
    public bool active;
    
    void Start() 
    {
        TickTime = 0;
        Damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        TickTime += Time.deltaTime;
        if (active && TickTime >= 0.25)
        {
            TickTime = 0;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
            {
                int TempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().FlameGun_DamageCounting * Damage;
                Collider collider = hit.collider;
                if (collider != null && collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().curHealth -= TempDamage;
                }
            }

            // 레이캐스트의 시작점과 끝점을 연결하여 디버그 라인 그리기
            Debug.DrawLine(transform.position, transform.position + transform.forward * raycastLength, Color.red);
        }
    }
    
}