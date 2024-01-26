using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSilver : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Effect;
    [SerializeField] private int attackNum;     //유효타횟수
    [SerializeField] private float attackRadius;  //바위 범위
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        attackNum = 0;
        attackRadius = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackNum >= 10)
        {
            attackNum = 0;
            // 범위내 무작위 적의 방향으로 바위 생성 (적이있을경우)
            Vector3 directionToEnemy = findNearEnemy();
            // 방향 벡터가 유효한지 확인
            if (directionToEnemy != Vector3.zero)
            {
                // Effect 오브젝트 생성
                GameObject effectInstance = Instantiate(Effect, transform.position, Quaternion.identity);

                // 생성된 Effect 오브젝트를 방향으로 회전시킴
                effectInstance.transform.forward = directionToEnemy;
            }
            
        }
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            attackNum++;
            
        }
    }

    //범위내 무작위 적의 방향
    private Vector3 findNearEnemy()
    {
        Vector3 directionToEnemy = Vector3.zero;
    
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // 선택된 적의 위치
                Vector3 enemyPosition = collider.gameObject.transform.position;

                // 현재 오브젝트에서 적으로 향하는 방향 벡터 계산
                directionToEnemy = (enemyPosition - transform.position).normalized;
                break;
            }
        }

        return directionToEnemy;
    }
    
    
    
}
