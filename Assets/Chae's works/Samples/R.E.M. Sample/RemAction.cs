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

        // ����Ʈ
        ActivateEffect(remController.REM_Heal);
        CreateEffectAtTarget(remController.REM_HealTarget, remController.Target, 2.0f);
        
        // ġ�� �ڵ� �ۼ�


        PlayerManager.instance.Health = Mathf.Min(PlayerManager.instance.Health + 20, PlayerManager.instance.TotalHealth); // 20��ŭ ü�� ȸ��, Player�� �ִ�ü�¸�ŭ ����
        Debug.Log("Healed to: " + PlayerManager.instance.Health);
        yield return new WaitForSeconds(2f);

        EndAction();
        yield return null;
    }

    IEnumerator DoDeployShield()
    {
        isMovable = false;
        isActioning = true;

        // ����Ʈ
        CreateEffectAtTarget(remController.REM_Shield, remController.Target, 4.0f);

        // �� ���� �ڵ� �ۼ�
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

        // 1. �ڷ���Ʈ ����Ʈ ���� �� �����̵�
        Vector3 teleportPosition = new Vector3(target.position.x, remController.transform.position.y, target.position.z);
        GameObject teleportInstance = GameObject.Instantiate(teleportEffect, remController.transform.position, Quaternion.identity, remController.EffectContainer.transform);
        teleportInstance.SetActive(true);
        yield return new WaitForSeconds(0.1f); // ��� ���
        remController.transform.position = teleportPosition;
        remController.REM_Mesh.SetActive(true);
        GameObject chargeInstance = GameObject.Instantiate(chargeEffect, remController.transform.position, Quaternion.identity, remController.EffectContainer.transform);
        chargeInstance.SetActive(true);
        yield return new WaitForSeconds(0.9f); // 0.5�� ���
        StopParticleSystem(teleportInstance);
        StopParticleSystem(chargeInstance);
        // ������ ����Ʈ ���� �� ���� ȸ��
        GameObject laserInstance = GameObject.Instantiate(laserEffect, remController.transform.position, remController.transform.rotation, remController.EffectContainer.transform);
        laserInstance.SetActive(true);

        float rotateDuration = 2.5f; // 2.5�� ���� ȸ��
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            float rotationSpeed = 120f / rotateDuration * Time.deltaTime; // 60���� ������ ȸ��
            remController.transform.Rotate(0, rotationSpeed, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ����Ʈ ����
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

        StopParticleSystem(effect, delay); // 2�� �� ����Ʈ ��Ȱ��ȭ
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
            // ����Ʈ�� Ÿ���� �ڽ����� ����
            GameObject effectInstance = Instantiate(effectPrefab, target.transform.position, Quaternion.identity, target.transform);
            effectInstance.SetActive(true);

            // ���� �ð� �Ŀ� ����
            Destroy(effectInstance, destroyTime);
        }
    }
}
