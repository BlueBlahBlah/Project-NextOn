using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class FlockLightBomb : MonoBehaviour
{
    public float moveDistance = 3.0f; // ?�직일 거리
    public float moveDuration = 2.0f; // ?�직이????걸리???�간
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
        // "Enemy" ?�그�?가�?가??가까운 게임 ?�브?�트�?찾음
        GameObject nearestEnemy = FindNearestEnemy();

        // ?�이 발견?�면
        if (nearestEnemy != null)
        {
            // 가??가까운 ?�의 ?�치�??�음
            Vector3 enemyPosition = nearestEnemy.transform.position;
            // 방향??구함
            Vector3 direction = (enemyPosition - transform.position).normalized;

            // ?�당 방향?�로 ?�브?�트�??�동?�킴
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
        // MoveUpCoroutine ?�작
        yield return StartCoroutine(MoveUpCoroutine());

        // MoveUpCoroutine ???�난 ??attack() ?�행
        attack();
    }

    // ?�로 ?�직이??코루??    private IEnumerator MoveUpCoroutine()
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

        // ?�니메이?�이 ?�난 ???�직임??초기??        transform.position = targetPosition;
    }

    // ??방향?�로 ?�직이??코루??    private IEnumerator MoveTowardsEnemyCoroutine(Vector3 direction)
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
            //collider.damage--; //collider??체력???�는 메커?�즘
            Destroy(gameObject, 0.5f);
        }
    }
}
