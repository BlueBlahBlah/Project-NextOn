using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireGunFlame : MonoBehaviour
{
    //[SerializeField] private Collider thisCollider;
    public float raycastLength = 7f; // 레이의 최대 길이

    public bool active;
    // Start is called before the first frame update
    void Start() 
    {
        Debug.Log("화염방사기 시작");
        //thisCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
            {
                Collider collider = hit.collider;
                if (collider != null && collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().curHealth--;
                    Debug.Log("화염방사기 공격");
                }
            }

            // 레이캐스트의 시작점과 끝점을 연결하여 디버그 라인 그리기
            Debug.DrawLine(transform.position, transform.position + transform.forward * raycastLength, Color.red);
        }
    }

    /*void OnTriggerStay(Collider collider)
    {
        Debug.Log("화염방사기 출력");
        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy>().curHealth--;
            Debug.Log("화염방사기 공격");
        }
    }*/
}