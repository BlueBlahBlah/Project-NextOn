using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    private int playerHp = 0;               //임시 확인용 HP
    private Transform PlayerTr;             //플레이어 위치 확인용
    private Vector3 PlayerForward;          //플레이어의 정면 확인용
    private float Skill_Round = 2.0f;       //스킬 실행 원길이
    GameObject particleSystemObject;        //스킬 프리팹 대용

    [Header("SKILL")]
    public GameObject Skill_WindHeal;
    public GameObject Skill_Storm;
    public GameObject Skill_Meteor;
    public GameObject Skill_FireGuard;
    public GameObject Skill_FireMine;

    void Start()
    {
        PlayerTr = GetComponent<Transform>();
        Skill_Storm.GetComponent<CapsuleCollider>().radius = Skill_Round;
        Skill_Storm.GetComponent<CapsuleCollider>().height = Skill_Round;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) P_Skill_WindHeal();                //Z 입력시
        else if (Input.GetKeyDown(KeyCode.X)) P_Skill_Storm();              //X 입력시
        else if (Input.GetKeyDown(KeyCode.C)) P_Skill_Skill_FireMine();     //C 입력시
        else if (Input.GetKeyDown(KeyCode.V)) P_Skill_FireGuard();          //V 입력시
    }
    /*  
        1.힐 스킬
    플레이어 캐릭터 주변으로 초록색 광선이 수직으로 생성됨
    -플레이어의 체력 증가
        2.주변 피해 스킬
    플레이어 캐릭터 주변으로 충격파를 생성해 피해를 줌
    -해당 범위(skill_Round)이내 존재하는 모든 적에게 피격 데미지 부가
         3.지뢰 설치 스킬
    플레이어가 존재하는 위치에 지뢰 설치후 일정 시간이 지나면 폭발 데미지 부가
    -일정시간은 3f 로 고정함
        4. 
     */

    private void P_Skill_WindHeal()
    {
        particleSystemObject = Instantiate(Skill_WindHeal, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;

        playerHp += 10;
        Debug.Log($"After Hp :{playerHp}");
    }

    private void P_Skill_Storm()
    {
        particleSystemObject = Instantiate(Skill_Storm, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;

        Collider[] colliders = Physics.OverlapSphere(transform.position, Skill_Round);

        foreach (Collider collider in colliders) // 가져온 Collider 배열을 순회하며 원하는 동작 수행
        {
            //collider.hp -=10;
            Debug.Log($"enemy hp: collider.hp");
        }

    }

    private void P_Skill_Skill_FireMine()
    {
        particleSystemObject = Instantiate(Skill_FireMine, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, particleSystemObject.transform.localScale.y, Skill_Round);
        particleSystemObject.transform.GetChild(1).transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
    }

    private void P_Skill_FireGuard()
    {
        particleSystemObject = Instantiate(Skill_FireGuard, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;
    }
}
