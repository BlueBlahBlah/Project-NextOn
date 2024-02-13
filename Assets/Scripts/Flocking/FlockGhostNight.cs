using System.Collections;
using UnityEngine;

public class FlockGhostNight : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    public float minInterval = 10f;
    public float maxInterval = 20f;
    public float attackRadius = 5f;
    public int damageAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        // 최초에 한 번은 즉시 호출
        //InvokeRepeating("ShowEffect", 0f, Random.Range(minInterval, maxInterval));
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    // Update is called once per frame
    void Update()
    {
        // 추가적인 로직이 필요하다면 여기에 작성
    }

    void ShowEffect()
    {
        // 이펙트를 보여주는 로직
        GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        // 예를 들어, 일정 시간이 지난 후에 이펙트를 제거하려면
        Destroy(effectInstance, 1.5f); // 1.5초 후에 이펙트를 제거하도록 설정 (원하는 시간으로 변경 가능)

        AttackEnemies(); //적에게 데미지
        
        // 다음 호출을 위한 랜덤한 시간 간격 설정
        Invoke("ShowEffect", Random.Range(minInterval, maxInterval));
    }

    void AttackEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Enemy 태그를 가진 적에게 데미지 주기
                //collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
                Debug.Log("백귀야행 공격");
            }
        }
    }
}