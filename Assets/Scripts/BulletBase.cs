using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private int damage;
    private bool isInitialized = false;

    // GunBase에서 발사될 때 호출하여 데미지 등 데이터를 주입
    public void Initialize(int damageValue)
    {
        damage = damageValue;
        isInitialized = true;
        
        // --- 오브젝트 풀링을 위한 물리 속도 초기화 ---
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 1초 후 자동 반환 예약
        Invoke(nameof(ReturnToPool), 1f);
    }

    private void ReturnToPool()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // 1. 비활성화될 때 예약된 Invoke 취소 (중복 실행 방지)
        CancelInvoke(nameof(ReturnToPool));

        // 2. 비활성화되는 순간 트레일 잔상 지우기
        TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }

        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInitialized) return;

        // 적에게 닿았을 때 데미지 처리
        if (other.CompareTag("Enemy"))
        {
            // Enemy 클래스가 있다면 데미지 처리
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.curHealth -= damage;
            }
            // 적에게 타격하면 즉시 풀로 반환
            gameObject.SetActive(false);
        }
    }
}
