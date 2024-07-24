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
        CreateEffectAtTarget(remController.REM_HealTarget, remController.Target);
        
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
        ActivateEffect(remController.REM_Shield);

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
        // ���� �ڵ� �ۼ�
        Debug.Log("Attacking the enemy!");
        yield return new WaitForSeconds(1f);

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

    void CreateEffectAtTarget(GameObject effectPrefab, GameObject target)
    {
        if (target != null)
        {
            // ����Ʈ�� Ÿ���� �ڽ����� ����
            GameObject effectInstance = Instantiate(effectPrefab, target.transform.position, Quaternion.identity, target.transform);
            effectInstance.SetActive(true);

            // ���� �ð� �Ŀ� ����
            Destroy(effectInstance, 2.0f);
        }
    }
}
