using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemController : MonoBehaviour
{
    public GameObject Target; // Ÿ�� ��ü
    public float speed = 2.0f; // �̵� �ӵ�
    public float floatAmplitude = 0.5f; // �����ϴ� ���� ����
    public float floatFrequency = 1.0f; // �����ϴ� �ӵ�

    private List<UtilityAction> actions = new List<UtilityAction>();
    private float timeSinceLastAction = 0.0f; // ���� �ൿ ���� ����� �ð�
    private const float actionInterval = 5.0f; // 5�� ����

    private float initialRotationX;
    private float timeElapsed = 0.0f; // ��� �ð�

    void Start()
    {
        initialRotationX = transform.eulerAngles.x;

        // �ൿ ���ǿ� �߰�
        actions.Add(new UtilityAction(
            "Heal",
            () => RemTestManager.instance.hp < 30 ? 1.0f / RemTestManager.instance.hp : 0, // ü���� �������� ��ƿ��Ƽ ����
            () => RemAction.Heal() // RemAction�� ���� ġ�� �ൿ
        ));

        actions.Add(new UtilityAction(
            "DeployShield",
            () => RemTestManager.instance.hp < 50 ? (100 - RemTestManager.instance.hp) / 50.0f : 0, // ü���� 50% ������ �� ��ƿ��Ƽ ����
            () => RemAction.DeployShield() // RemAction�� ���� �� ���� �ൿ
        ));

        actions.Add(new UtilityAction(
            "Attack",
            () => RemTestManager.instance.isNear ? 1.0f : 0, // ���� ����� �� ��ƿ��Ƽ ����
            () => RemAction.Attack() // RemAction�� ���� ���� �ൿ
        ));
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // 5�� �������� ����
        if (timeSinceLastAction >= actionInterval)
        {
            SelectAndExecuteBestAction();
            timeSinceLastAction = 0.0f; // �ð� �ʱ�ȭ
        }

        // Target���� �Ÿ� Ȯ�� �� �̵�
        if (Target != null)
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