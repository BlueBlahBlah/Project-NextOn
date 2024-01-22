using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class FlockLightBomb : MonoBehaviour
{
    public float moveDistance = 3.0f; // 움직일 거리
    public float moveDuration = 2.0f; // 움직이는 데 걸리는 시간
    [SerializeField] private GameObject Effect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveUpAndAttackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic, if needed
    }

    void attack()
    {
        // "Enemy" 태그를 가진 가장 가까운 게임 오브젝트를 찾음
        GameObject nearestEnemy = FindNearestEnemy();

        // 적이 발견되면
        if (nearestEnemy != null)
        {
            // 가장 가까운 적의 위치를 얻음
            Vector3 enemyPosition = nearestEnemy.transform.position;
            // 방향을 구함
            Vector3 direction = (enemyPosition - transform.position).normalized;

            // 해당 방향으로 오브젝트를 이동시킴
            StartCoroutine(MoveTowardsEnemyCoroutine(direction));

        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }

    private IEnumerator MoveUpAndAttackCoroutine()
    {
        // MoveUpCoroutine 시작
        yield return StartCoroutine(MoveUpCoroutine());

        // MoveUpCoroutine 이 끝난 후 attack() 실행
        attack();
    }

    // 위로 움직이는 코루틴
    private IEnumerator MoveUpCoroutine()
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * moveDistance;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 애니메이션이 끝난 후 움직임을 초기화
        transform.position = targetPosition;
    }

    // 적 방향으로 움직이는 코루틴
    private IEnumerator MoveTowardsEnemyCoroutine(Vector3 direction)
    {
        float elapsedTime = 0.0f;
        float moveSpeed = 10;

        while (elapsedTime < 5)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Effect.GetComponent<ParticleSystem>().Play();
            StopAllCoroutines();
            //collider.damage--; //collider의 체력이 닳는 메커니즘
            Destroy(gameObject, 0.5f);
        }
    }
}