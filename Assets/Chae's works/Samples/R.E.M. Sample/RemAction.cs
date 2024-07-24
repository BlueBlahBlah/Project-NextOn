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
        CreateEffectAtTarget(remController.REM_HealTarget, remController.Target);
        
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
        ActivateEffect(remController.REM_Shield);

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
        // 공격 코드 작성
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

        StartCoroutine(DeactivateEffect(effect, 2.0f)); // 2초 후 이펙트 비활성화
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
            // 이펙트를 타겟의 자식으로 생성
            GameObject effectInstance = Instantiate(effectPrefab, target.transform.position, Quaternion.identity, target.transform);
            effectInstance.SetActive(true);

            // 일정 시간 후에 삭제
            Destroy(effectInstance, 2.0f);
        }
    }
}
