using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class FlockLightBomb : MonoBehaviour
{
    public float moveDistance = 3.0f; 
    public float moveDuration = 2.0f; 
    [SerializeField] private GameObject Effect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveUpAndAttackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void attack()
    {
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            Vector3 enemyPosition = nearestEnemy.transform.position;
            Vector3 direction = (enemyPosition - transform.position).normalized;
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
        yield return StartCoroutine(MoveUpCoroutine());
        attack();
    }

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

        transform.position = targetPosition;
    }

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
            Destroy(gameObject, 0.5f);
        }
    }
}
