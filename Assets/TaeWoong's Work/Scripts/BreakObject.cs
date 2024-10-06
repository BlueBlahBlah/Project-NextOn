using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BreakObject : Enemy
{
    // public int maxHealth; // 최대 체력
    // public int curHealth; // 현재 체력
    private int health; // 현재 스크립트에서 관리하는 체력

    Rigidbody _rigid;
    BoxCollider _boxCollider;
    NavMeshAgent _nav;
    public GameObject rotationObj; // 회전할 물체
    public GameObject showNormalizationObj; // 정상화 표시
    public RepairArea repairArea_1st;
    public RepairArea repairArea_2nd;
    public RepairArea repairArea_3rd;
    public GameObject[] objectsToDisable; // 비활성화할 오브젝트 배열
    public GameObject[] objectsToAble; // 활성화할 오브젝트 배열
    [SerializeField] private TextMeshPro damaged; // 데미지 표시용 텍스트

    public bool isDestroyed = false; // 파괴 여부
    public bool isActive = false; // 활성화 여부
    public bool isNormalization = false; // 정상화 여부
    public bool isEnterArea3 = false; // Area 3 오픈 여부
    public float rotationSpeed = 50f; // 회전 속도

    void Awake() 
    {
        _rigid = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>(); 
        _nav = GetComponent<NavMeshAgent>();
        this.maxHealth = GetComponent<Enemy>().maxHealth;
        this.curHealth = this.maxHealth;
        isChase = false;

        damaged.SetText("");  // 데미지를 입은 경우에만 표시
    }

    // 피격 로직을 담을 코루틴 작성
    void Update() 
    {
        if (isActive)
        {
            // Rotate 함수로 회전효과 
            rotationObj.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // 수리 영역 활성화
            // 활성화할 오브젝트들 활성화
            foreach (GameObject obj in objectsToAble)
            {
                obj.SetActive(true);
            }
        }

        if (repairArea_1st.isRepaired && repairArea_2nd.isRepaired && repairArea_3rd.isRepaired) // 데미지를 받을 수 있는 상태인지 확인
        {
            // 체력 관련 처리
            if (isNormalization)  // 죽은 경우
            {
                Normalization();
            }
            else    // 살아있는 경우
            {
                // 체력 관련
                this.curHealth = GetComponent<Enemy>().curHealth;
                if (curHealth < health)
                {
                    if (curHealth <= 0)
                    {
                        int DamageDone = health - curHealth; // 입은 데미지.
                        isNormalization = true; // 정상화됨
                        Normalization();
                    }
                    else
                    {
                        int DamageDone = health - curHealth; // 입은 데미지.
                        ShowDamage(DamageDone);
                        health = curHealth;
                    }
                }
            } 
        }
    }

    private void ShowDamage(int d)
    {
        TextMeshPro tempDamage = Instantiate(damaged, transform.position + new Vector3(0, 3.5f, 0), Quaternion.identity);
        tempDamage.SetText(d.ToString());
    }

    void Normalization()
    {
        // if (isNormalization) return; // 이미 죽은 경우 추가 처리 방지

        isNormalization = true; // 사망 상태로 변경
        isEnterArea3 = true; // Area 3 오픈

        showNormalizationObj.SetActive(true); // 정상화 표시

        // 활성화할 오브젝트들 활성화
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        // Rigidbody의 물리적 힘 비활성화
        _rigid.isKinematic = true; // 물리적 상호작용 중지

        if (GetComponent<Enemy>().isChase == true) //update안에서 오류가 너무 많이 발생하지 않도록하기 위함
        {
            GetComponent<Enemy>().stopNav();
            GetComponent<Enemy>().Death_Collider_False(); //콜라이더 비활성화
        }
        GetComponent<Enemy>().isChase = false;
    }

    public void Activate()
    {
        isActive = true; // 외부에서 활성화 시 이용
    }

    // 플레이어의 공격을 받을 수 있는지 확인하는 메서드
    private bool CanTakeDamage()
    {
        // 모든 RepairArea의 isRepaired 상태를 체크
        return repairArea_1st.isRepaired && repairArea_2nd.isRepaired && repairArea_3rd.isRepaired;
    }
}
