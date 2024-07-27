using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemController : MonoBehaviour
{
    [Header("Move")]
    public GameObject Target; // Ÿ�� ��ü
    public GameObject REM_Mesh; // REM�� �𵨸�
    public float speed = 2.0f; // �̵� �ӵ�
    public float floatAmplitude = 0.5f; // �����ϴ� ���� ����
    public float floatFrequency = 1.0f; // �����ϴ� �ӵ�

    private List<UtilityAction> actions = new List<UtilityAction>();

    [Header("System")]
    public float timeSinceLastAction = 0.0f; // ���� �ൿ ���� ����� �ð�
    [SerializeField]
    private float actionInterval = 5.0f; // �ǵ��� �ൿ ����
    [SerializeField]
    private float timeElapsed = 0.0f; // ��� �ð�

    private float initialRotationX;
    private RemAction remAction;

    [Header("Effects")]
    public GameObject EffectContainer;
    public GameObject REM_Heal;
    public GameObject REM_HealTarget;
    public GameObject REM_Shield;
    public GameObject REM_Teleport;
    public GameObject REM_AttackCharge;
    public GameObject REM_AttackLaser;
    public GameObject REM_Debuff;

    void Start()
    {
        remAction = this.gameObject.GetComponent<RemAction>();

        initialRotationX = transform.eulerAngles.x;

        // �ൿ ���ǿ� �߰�
        actions.Add(new UtilityAction(
            "Heal",
            () => 50, // RemTestManager.instance.hp < 50 ? 1.0f / RemTestManager.instance.hp : 0, // ü���� �������� ��ƿ��Ƽ ����
            () => remAction.Heal()
            ));

        actions.Add(new UtilityAction(
            "DeployShield",
            () => 50, // RemTestManager.instance.hp < 90 ? (100 - RemTestManager.instance.hp) / 50.0f : 0, // ü���� 50% ������ �� ��ƿ��Ƽ ����
            () => remAction.DeployShield()
            ));

        actions.Add(new UtilityAction(
            "Attack",
            () => 90, // (RemTestManager.instance.isNear && RemTestManager.instance.hp >= 90) ? 80.0f : 0, // ���� ����� �� ��ƿ��Ƽ ����
            () => remAction.Attack()
            ));

        actions.Add(new UtilityAction(
            "Debuff",
            () => 50, // (RemTestManager.instance.isNear && RemTestManager.instance.hp >= 90) ? 79.0f : 0,
            () => remAction.Debuff()
            ));
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // 5�� �������� ����
        if (timeSinceLastAction >= actionInterval && !remAction.isActioning)
        {
            SelectAndExecuteBestAction();
        }

        // Target���� �Ÿ� Ȯ�� �� �̵�
        if (Target != null && remAction.isMovable)
        {
            MoveTowardsTarget();
        }
    }

    void SelectAndExecuteBestAction()
    {
        UtilityAction bestAction = null;
        float highestUtility = float.MinValue;

        foreach (var action in actions)
        {
            float utility = action.CalculateUtility();
            if (utility > highestUtility)
            {
                highestUtility = utility;
                bestAction = action;
            }
        }

        bestAction?.Execute();
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = Target.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > 2.0f)
        {
            // Ÿ���� ���� �̵�
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            // ��ƼŬ�� ���� ����
        }
        else
        {
            // ��ƼŬ�� ���� ����
        }

        // Ÿ���� �ٶ󺸵��� ȸ��, x�� ȸ���� �ʱⰪ�� ����
        Vector3 lookDirection = Target.transform.position - transform.position;
        lookDirection.y = 0; // x�� ȸ�� ����, y�� �̵��� ���
        if (lookDirection != Vector3.zero) // zero vector Ȯ��
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(initialRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        // �����ϴ� y�� ���
        float floatOffset = Mathf.Sin(timeElapsed * floatFrequency) * floatAmplitude;
        Vector3 newPosition = transform.position;
        newPosition.y = Target.transform.position.y + 1.0f + floatOffset;
        transform.position = newPosition;
    }
}