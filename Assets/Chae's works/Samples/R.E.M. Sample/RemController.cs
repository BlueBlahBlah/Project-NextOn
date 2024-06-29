using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemController : MonoBehaviour
{
    public GameObject Target; // 타겟 객체
    public float speed = 2.0f; // 이동 속도
    public float floatAmplitude = 0.5f; // 부유하는 높이 진폭
    public float floatFrequency = 1.0f; // 부유하는 속도

    private List<UtilityAction> actions = new List<UtilityAction>();
    private float timeSinceLastAction = 0.0f; // 지난 행동 이후 경과된 시간
    private const float actionInterval = 5.0f; // 5초 간격

    private float initialRotationX;
    private float timeElapsed = 0.0f; // 경과 시간

    void Start()
    {
        initialRotationX = transform.eulerAngles.x;

        // 행동 정의와 추가
        actions.Add(new UtilityAction(
            "Heal",
            () => RemTestManager.instance.hp < 30 ? 1.0f / RemTestManager.instance.hp : 0, // 체력이 낮을수록 유틸리티 증가
            () => RemAction.Heal() // RemAction을 통한 치유 행동
        ));

        actions.Add(new UtilityAction(
            "DeployShield",
            () => RemTestManager.instance.hp < 50 ? (100 - RemTestManager.instance.hp) / 50.0f : 0, // 체력이 50% 이하일 때 유틸리티 증가
            () => RemAction.DeployShield() // RemAction을 통한 방어막 전개 행동
        ));

        actions.Add(new UtilityAction(
            "Attack",
            () => RemTestManager.instance.isNear ? 1.0f : 0, // 적이 가까울 때 유틸리티 증가
            () => RemAction.Attack() // RemAction을 통한 공격 행동
        ));
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // 5초 간격으로 실행
        if (timeSinceLastAction >= actionInterval)
        {
            SelectAndExecuteBestAction();
            timeSinceLastAction = 0.0f; // 시간 초기화
        }

        // Target과의 거리 확인 및 이동
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
            // 타겟을 향해 이동
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        // 타겟을 바라보도록 회전, x축 회전은 초기값을 유지
        Vector3 lookDirection = Target.transform.position - transform.position;
        lookDirection.y = 0; // x축 회전 유지, y축 이동만 고려
        if (lookDirection != Vector3.zero) // zero vector 확인
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(initialRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        // 부유하는 y값 계산
        float floatOffset = Mathf.Sin(timeElapsed * floatFrequency) * floatAmplitude;
        Vector3 newPosition = transform.position;
        newPosition.y = Target.transform.position.y + 1.0f + floatOffset;
        transform.position = newPosition;
    }
}