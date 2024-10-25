using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSilver : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject Effect;
    //[SerializeField] private int attackNum;     //유효타횟수
    [SerializeField] private float attackRadius;  //바위 범위
    [SerializeField] private Button Btn;
    public int Damage;
    
    public float ThisCoolTime;            //현재 무기의 돌아가고 있는 쿨타임
    public float SkillCoolTime;           //현재 무기 스킬의 총 쿨타임
    [SerializeField] private float SkillCoolTimeRate;       //PlayerManager에서 가져오는 쿨타임 감소율
    
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        //attackNum = 0;
        attackRadius = 20f;
        Damage = 1;
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //현재 무기의 쿨타임을 10초로 초기화 
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //쿨타임은 감소율을 적용한 값으로 
        ThisCoolTime = 0;
        Btn.interactable = true;        //처음에는(먹자마자) 스킬 사용가능
    }
    
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
        Btn.onClick.AddListener(SpawnRock);
        
        SkillCoolTimeRate = PlayerManager.Instance.SkillCoolTimeRate;
        SkillCoolTime = 10f;            //현재 무기의 쿨타임을 10초로 초기화 
        SkillCoolTime = SkillCoolTime - (SkillCoolTime * SkillCoolTimeRate);        //쿨타임은 감소율을 적용한 값으로 
        ThisCoolTime = 0;
        Btn.interactable = true;        //처음에는(먹자마자) 스킬 사용가능
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisCoolTime > 0)                  //쿨타임이 0보다 클때 (쿨이 남아있는 경우)
        {
            ThisCoolTime -= Time.deltaTime;     //쿨타임 감소
        }
        else if (ThisCoolTime <= 0)           //쿨타임이 0일때 
        {
            Btn.interactable = true;         //스킬 사용 가능
        }
    }
    
    

    //범위내 무작위 적의 방향
    private Vector3 findNearEnemy()
    {
        Vector3 directionToEnemy = Vector3.zero;
    
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);
        List<GameObject> enemyList = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemyList.Add(collider.gameObject);
                /*// 선택된 적의 위치
                Vector3 enemyPosition = collider.gameObject.transform.position;

                // 현재 오브젝트에서 적으로 향하는 방향 벡터 계산
                directionToEnemy = (enemyPosition - transform.position).normalized;
                break;*/
            }
        }

        if (enemyList.Count == 0)       //근처에 적이 없는 경우
        {
            return Vector3.zero;
        }

        int i = Random.Range(0, enemyList.Count);
        //적중에 랜덤한 적을 선택
        Vector3 enemyPosition = enemyList[i].gameObject.transform.position;

        // 현재 오브젝트에서 적으로 향하는 방향 벡터 계산
        directionToEnemy = (enemyPosition - transform.position).normalized;

        return directionToEnemy;
    }

    void SpawnRock()
    {
        Vector3 directionToEnemy = findNearEnemy();
        directionToEnemy.y = 0f;
        // 방향 벡터가 유효한지 확인
        if (directionToEnemy != Vector3.zero)
        {
            // Effect 오브젝트 생성
            GameObject effectInstance = Instantiate(Effect, transform.position, Quaternion.identity);

            // 생성된 Effect 오브젝트를 방향으로 회전시킴
            effectInstance.transform.forward = directionToEnemy;
        }
        
        Btn.interactable = false;       //스킬 사용후 다음 쿨타임까지 버튼 잠금
        ThisCoolTime = SkillCoolTime;   //쿨타임 생김
    }
    
}
