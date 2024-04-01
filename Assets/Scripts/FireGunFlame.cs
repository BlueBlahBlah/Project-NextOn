using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunFlame : MonoBehaviour
{
    public float raycastLength = 7f; // 레이의 최대 길이
    private float tickTime;        // 데미지를 주는 틱 간격
    public int damage;
    
    public bool active;
    
    void Start() 
    {
        tickTime = 0;
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        tickTime += Time.deltaTime;
        if (active && tickTime >= 0.25)
        {
            tickTime = 0;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raycastLength);
            foreach (RaycastHit hit in hits)
            {
                int tempDamage =  GameObject.Find("StageManager").GetComponent<StageManager>().FlameGun_DamageCounting * damage;
                Collider collider = hit.collider;
                if (collider != null && collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().curHealth -= tempDamage;
                }
            }

            // 레이캐스트의 시작점과 끝점을 연결하여 디버그 라인 그리기
            Debug.DrawLine(transform.position, transform.position + transform.forward * raycastLength, Color.red);
        }
    }
}