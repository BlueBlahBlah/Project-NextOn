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


        RemTestManager.instance.hp = Mathf.Min(RemTestManager.instance.hp + 20, 100); // 20��ŭ ü�� ȸ��, �ִ� ü�� 100 ����
        Debug.Log("Healed to: " + RemTestManager.instance.hp);
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

        GameObject teleportEffect = remController.REM_Teleport;
        GameObject laserEffect = remController.REM_AttackLaser;
        Transform target = remController.Target.transform;

        // 1. �ڷ���Ʈ ����Ʈ ���� �� �����̵�
        Vector3 teleportPosition = new Vector3(target.position.x, remController.transform.position.y, target.position.z);
        GameObject teleportInstance = GameObject.Instantiate(teleportEffect, remController.transform.position, Quaternion.identity);
        teleportInstance.SetActive(true);
        yield return new WaitForSeconds(0.1f); // ��� ���
        remController.transform.position = teleportPosition;
        yield return new WaitForSeconds(0.5f); // 0.5�� ���

        // 2. �ڷ���Ʈ ����Ʈ ����
        GameObject.Destroy(teleportInstance);

        // ������ ����Ʈ ���� �� ���� ȸ��
        GameObject laserInstance = GameObject.Instantiate(laserEffect, remController.transform.position, remController.transform.rotation, remController.EffectContainer.transform);
        laserInstance.SetActive(true);

        float rotateDuration = 2.5f; // 2.5�� ���� ȸ��
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            float rotationSpeed = 60f / rotateDuration * Time.deltaTime; // 60���� ������ ȸ��
            remController.transform.Rotate(0, rotationSpeed, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ����Ʈ ����
        GameObject.Destroy(laserInstance);

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

    private void ActivateEffect(GameObject effect)
    {
        if (effect == null) return;
        effect.SetActive(true);

        StartCoroutine(DeactivateEffect(effect, 2.0f)); // 2�� �� ����Ʈ ��Ȱ��ȭ
    }

    IEnumerator DeactivateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
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
