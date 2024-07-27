using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RemAction : MonoBehaviour
{
    private RemController remController;

    public bool isMovable = true;
    public bool isActioning;

    public void Heal() { StartCoroutine("DoHeal"); }
    public void DeployShield() { StartCoroutine("DoDeployShield"); }
    public void Attack() { StartCoroutine("DoAttack"); }
    public void Debuff() { StartCoroutine("DoDebuff"); }

    void Start()
    {
        remController = this.gameObject.GetComponent<RemController>();
    }

    // Coroutine
    #region
    IEnumerator DoHeal()
    {
        isMovable = false;
        isActioning = true;

        // 이펙트
        ActivateEffect(remController.REM_Heal);
        CreateEffectAtTarget(remController.REM_HealTarget, remController.Target, 2.0f);
        
        // 치유 코드 작성


        RemTestManager.instance.hp = Mathf.Min(RemTestManager.instance.hp + 20, 100); // 20만큼 체력 회복, 최대 체력 100 제한
        Debug.Log("Healed to: " + RemTestManager.instance.hp);
        yield return new WaitForSeconds(2f);

        EndAction();
        yield return null;
    }

    IEnumerator DoDeployShield()
    {
        isMovable = false;
        isActioning = true;

        // 이펙트
        CreateEffectAtTarget(remController.REM_Shield, remController.Target, 4.0f);

        // 방어막 전개 코드 작성
        Debug.Log("Shield deployed!");
        yield return new WaitForSeconds(3f);

        EndAction();
        yield return null;
    }

    IEnumerator DoAttack()
    {
        isMovable = false;
        isActioning = true;
        remController.REM_Mesh.SetActive(false);

        GameObject teleportEffect = remController.REM_Teleport;
        GameObject chargeEffect = remController.REM_AttackCharge;
        GameObject laserEffect = remController.REM_AttackLaser;
        Transform target = remController.Target.transform;

        // 1. 텔레포트 이펙트 생성 및 순간이동
        Vector3 teleportPosition = new Vector3(target.position.x, remController.transform.position.y, target.position.z);
        GameObject teleportInstance = GameObject.Instantiate(teleportEffect, remController.transform.position, Quaternion.identity, remController.EffectContainer.transform);
        teleportInstance.SetActive(true);
        yield return new WaitForSeconds(0.1f); // 잠시 대기
        remController.transform.position = teleportPosition;
        remController.REM_Mesh.SetActive(true);
        GameObject chargeInstance = GameObject.Instantiate(chargeEffect, remController.transform.position, Quaternion.identity, remController.EffectContainer.transform);
        chargeInstance.SetActive(true);
        yield return new WaitForSeconds(0.9f); // 0.5초 대기
        StopParticleSystem(teleportInstance);
        StopParticleSystem(chargeInstance);
        // 레이저 이펙트 생성 및 방향 회전
        GameObject laserInstance = GameObject.Instantiate(laserEffect, remController.transform.position, remController.transform.rotation, remController.EffectContainer.transform);
        laserInstance.SetActive(true);

        float rotateDuration = 2.5f; // 2.5초 동안 회전
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            float rotationSpeed = 120f / rotateDuration * Time.deltaTime; // 60도를 서서히 회전
            remController.transform.Rotate(0, rotationSpeed, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 레이저 이펙트 제거
        StopParticleSystem(laserInstance);
        yield return new WaitForSeconds(0.7f);

        EndAction();
        yield return null;
    }

    IEnumerator DoDebuff()
    {
        isMovable = false;
        isActioning = true;
        Debug.Log("Debuff the enemy!");
        yield return new WaitForSeconds(1f);

        EndAction();
        yield return null;
    }
    #endregion

    // Skill's move
    #region
    #endregion

    // Util
    private void EndAction()
    {
        remController.timeSinceLastAction = 0f;
        isMovable = true;
        isActioning = false;
    }

    private void ActivateEffect(GameObject effect, float delay = 2.0f)
    {
        if (effect == null) return;
        effect.SetActive(true);

        StopParticleSystem(effect, delay); // 2초 후 이펙트 비활성화
    }

    void StopParticleSystem(GameObject effect, float delay = 0f)
    {
        StartCoroutine(StopParticleSystemCoroutine(effect, delay));
    }

    IEnumerator StopParticleSystemCoroutine(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop();
            // Wait for the particles to complete before destroying the object
            float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
            yield return new WaitForSeconds(totalDuration);
        }

        Destroy(effect);
    }

    void CreateEffectAtTarget(GameObject effectPrefab, GameObject target, float destroyTime)
    {
        if (target != null)
        {
            // 이펙트를 타겟의 자식으로 생성
            GameObject effectInstance = Instantiate(effectPrefab, target.transform.position, Quaternion.identity, target.transform);
            effectInstance.SetActive(true);

            // 일정 시간 후에 삭제
            Destroy(effectInstance, destroyTime);
        }
    }
}
