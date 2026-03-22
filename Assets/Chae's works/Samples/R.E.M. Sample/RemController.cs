using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemController : MonoBehaviour
{
    [Header("Move")]
    public GameObject Target; // 타겟 객체
    public GameObject REM_Mesh; // REM의 모델링
    public float speed = 2.0f; // 이동 속도
    public float floatAmplitude = 0.5f; // 부유하는 높이 진폭
    public float floatFrequency = 1.0f; // 부유하는 속도

    private List<UtilityAction> actions = new List<UtilityAction>();

    [Header("System")]
    public float timeSinceLastAction = 0.0f; // 지난 행동 이후 경과된 시간
    [SerializeField]
    private float actionInterval = 5.0f; // 의도한 행동 간격
    [SerializeField]
    private float timeElapsed = 0.0f; // 경과 시간

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

    [Header("EnemyDetect")]
    public float detectionRadius = 5f; // 감지할 범위의 반지름
    public LayerMask enemyLayerMask;   // 감지할 Enemy 레이어

    [Header("Adjustment")]
    [SerializeField] 
    private int healUsageCount = 0;
    [SerializeField] 
    private int shieldUsageCount = 0;
    [SerializeField] 
    private int attackUsageCount = 0;
    [SerializeField] 
    private int debuffUsageCount = 0;

    private const float maxAdjustment = 3.0f; // 보정값의 최대치

    void Start()
    {
        remAction = this.gameObject.GetComponent<RemAction>();

        initialRotationX = transform.eulerAngles.x;

        // 행동 정의와 추가

        ///////////////////////////////////////////////
        ///
        /// 아래의 RemTestManeger.instance 등을 PlayerManager.instance로 변경해야함 
        ///
        /////////////////////////////////////////////
        

        actions.Add(new UtilityAction(
            "Heal",
            () => CalculateHealUtility(),
            () => { healUsageCount++; remAction.Heal(); }
            ));

        actions.Add(new UtilityAction(
            "DeployShield",
            () => CalculateDeployShieldUtility(),
            () => { shieldUsageCount++; remAction.DeployShield(); }
            ));

        actions.Add(new UtilityAction(
            "Attack",
            () => CalculateDebuffUtility(),
            () => { attackUsageCount++; remAction.Attack(); }
            ));

        actions.Add(new UtilityAction(
            "Debuff",
            () => CalculateDebuffUtility(),
            () => { debuffUsageCount++; remAction.Debuff(); }
            ));
    }

    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // 5초 간격으로 실행
        if (timeSinceLastAction >= actionInterval && !remAction.isActioning)
        {
            SelectAndExecuteBestAction();
        }

        // Target과의 거리 확인 및 이동
        if (Target != null && remAction.isMovable)
        {
            MoveTowardsTarget();
        }

        DetectEnemies();
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

            // 파티클의 각도 변경
        }
        else
        {
            // 파티클의 각도 변경
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

    void DetectEnemies()
    {
        // LayerMask를 이용해 특정 레이어의 오브젝트만 검색
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);

        // 감지된 오브젝트의 수를 셈
        int enemyCount = hitColliders.Length;

        // 결과를 출력하거나 원하는 동작을 수행
        Debug.Log("Number of Enemies detected: " + enemyCount);
    }

    // 감지 범위를 시각적으로 표시 (선택 사항)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // 유틸리티 계산 함수들
    float CalculateHealUtility()
    {
        //float baseUtility = PlayerManager.instance.hp < 50 ? (1.0f / PlayerManager.instance.hp) * 100 : 0;
        float adjustment = Mathf.Min(0.1f * (healUsageCount * 0.2f) * (healUsageCount * 0.2f), maxAdjustment);// 사용 횟수에 비례한 보정값
        //return baseUtility + adjustment;

        Debug.Log(50 + adjustment);
        return 50 + adjustment; // 임시값
    }

    float CalculateDeployShieldUtility()
    {
        //float baseUtility = PlayerManager.instance.hp < 90 ? (100 - PlayerManager.instance.hp) / 50.0f * 100 : 0;
        float adjustment = Mathf.Min(0.1f * (shieldUsageCount * 0.2f) * (shieldUsageCount * 0.2f), maxAdjustment);// 사용 횟수에 비례한 보정값
        //return baseUtility + adjustment;

        Debug.Log(50 + adjustment);
        return 50 + adjustment; // 임시값
    }

    float CalculateAttackUtility()
    {
        //bool isNear = PlayerManager.instance.isNear;
        //float baseUtility = (isNear && PlayerManager.instance.hp >= 90) ? 80.0f : 0;
        float adjustment = Mathf.Min(0.1f * (attackUsageCount * 0.2f) * (attackUsageCount * 0.2f), maxAdjustment);// 사용 횟수에 비례한 보정값
        //return baseUtility + adjustment;

        Debug.Log(50 + adjustment);
        return 50 + adjustment; // 임시값
    }

    float CalculateDebuffUtility()
    {
        //bool isNear = PlayerManager.instance.isNear;
        //float baseUtility = (isNear && PlayerManager.instance.hp >= 90) ? 79.0f : 0;
        //float adjustment = Mathf.Min(debuffUsageCount * 0.1f, maxAdjustment); // 사용 횟수에 비례한 보정값
        //return baseUtility + adjustment;

        return 0; // 임시값
    }
}