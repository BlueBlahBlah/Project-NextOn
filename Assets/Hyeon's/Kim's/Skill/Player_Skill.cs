using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    private float skill_power = 10.0f;      //스킬 피해량
    private float skill_inrease = 1.0f;     //스킬 피해 증가량
    private float heal = 10.0f;             //회복량
    private float heal_increase = 1.0f;     //회복 증가량
    private float Skill_Round = 2.0f;       //스킬 실행 원길이


    private int playerHp = 0;               //임시 확인용 HP
    private Transform PlayerTr;             //플레이어 위치 확인용
    GameObject particleSystemObject;        //스킬 프리팹 대용
    GameObject zonePre;

    [Header("HealSkill")]
    public GameObject Skill_WindHeal;
    [Header("SplashSkill")]
    public GameObject Skill_Storm;
    [Header("GuardSkill")]
    public GameObject Skill_FireGuard_LV1;
    public GameObject Skill_FireGuard_LV2;
    public GameObject Skill_FireGuard_LV3;
    private GameObject FireGuard;
    [Header("MineSkill")]
    public GameObject Skill_FireMine_LV1;
    public GameObject Skill_FireMine_LV2;
    public GameObject Skill_FireMine_LV3;
    private GameObject FireMine;
    [Header("ElectDelaySkill")]
    public GameObject Skill_Spark;
    public GameObject Skill_Lightning;
    public GameObject Skill_DelayZone;
    [Header("StunSKill")]
    public GameObject Skill_Stun_LV1;
    public GameObject Skill_Stun_LV2;
    public GameObject Skill_Stun_LV3;
    private GameObject Stun;
    [Header("SkillZone")]
    public GameObject Skill_ZONE;

    void Start()
    {

        PlayerTr = GetComponent<Transform>();
        Skill_Storm.GetComponent<CapsuleCollider>().radius = Skill_Round;
        Skill_Storm.GetComponent<CapsuleCollider>().height = Skill_Round;

        zonePre =  Instantiate(Skill_ZONE);
        zonePre.GetComponent<Transform>().localPosition = transform.position;
        zonePre.GetComponent<Transform>().parent = this.transform;

        if(skill_inrease > 0 && skill_inrease < 100)
        {
            FireGuard = Skill_FireGuard_LV1;
            FireMine = Skill_FireMine_LV1;
            Stun = Skill_Stun_LV1;
        }
        else if(skill_inrease > 100)
        {
            FireGuard = Skill_FireGuard_LV2;
            FireMine = Skill_FireMine_LV2;
            Stun = Skill_Stun_LV2;
        }
        else
        {
            FireGuard = Skill_FireGuard_LV3;
            FireMine = Skill_FireMine_LV3;
            Stun = Skill_Stun_LV3;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) P_Skill_WindHeal();                //Z 입력시 Heal
        else if (Input.GetKeyDown(KeyCode.X)) P_Skill_Storm();              //X 입력시 Splash Skill
        else if (Input.GetKeyDown(KeyCode.C)) P_Skill_FireMine();           //C 입력시 Mine Skill
        else if (Input.GetKeyDown(KeyCode.V)) P_Skill_FireGuard();          //V 입력시 Guard Skill
        else if (Input.GetKeyDown(KeyCode.B)) P_Skill_Spark();              //B 입력시 Delay Skill
        else if (Input.GetKeyDown(KeyCode.N)) P_Skill_Stun();               //N 입력시 Stun Skill
    }

    void OnDrawGizmos() //범위확인용 기즈모
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward.normalized * Skill_Round);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Skill_Round*3);
    }
    private void P_Skill_WindHeal()
    {
        float Heal_round = Skill_Round * heal_increase;
        particleSystemObject = Instantiate(Skill_WindHeal, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Heal_round, Heal_round, Heal_round);
        particleSystemObject.transform.parent = this.transform;
        /*
         * 플레이어 체력회복
         * 총 회복량 = 체력 계수 * 회복량
         * 체력계수만큼 스킬 이펙트 크기가 증가함
         * player_HP += heal * heal_increase;
        */
    }

    private void P_Skill_Storm()
    {
        float Storm_round = Skill_Round * skill_inrease; 
        particleSystemObject = Instantiate(Skill_Storm, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Storm_round, Storm_round, Storm_round);
        particleSystemObject.transform.parent = this.transform;

        Collider[] colliders = Physics.OverlapSphere(transform.position, Storm_round);

        foreach (Collider collider in colliders) //collider 에 대미지 부가
        {
            /*
                * 해당 collider 적들에게 대미지 부가
                * 총 피해량 = 스킬계수 * 스킬피해량
                * colliders....
            */
        }

    }

    private void P_Skill_FireMine()
    {
        particleSystemObject = Instantiate(FireMine, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, particleSystemObject.transform.localScale.y, Skill_Round);
        /*
         * 스킬 사용시 지뢰 설치됨
         * -> 일정시간 지날시 폭발(Fire_Mine스크립트 발동함)
         */
    }

    private void P_Skill_FireGuard()
    {
        particleSystemObject = Instantiate(FireGuard, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;

        StartCoroutine(WaitAndDestroy(particleSystemObject, null, null,null,10f));
        /*
         * 스킬 사용시 일정 시간 지나면 자동으로 삭제됨
         * 콜라이더 접촉시 데미지부가
         */
    }

    private void P_Skill_Spark()
    {
        GameObject Lightning, Delay;
        particleSystemObject = Instantiate(Skill_Spark, new Vector3(PlayerTr.position.x, PlayerTr.position.y + 3f, PlayerTr.position.z), Quaternion.identity);
        particleSystemObject.transform.parent = this.transform;

        Collider[] colliders = Physics.OverlapSphere(PlayerTr.position, Skill_Round * 3);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Vector3 enemyPosition = collider.transform.position;
                    Lightning = Instantiate(Skill_Lightning, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), Quaternion.identity);
                    Delay = Instantiate(Skill_DelayZone, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), Quaternion.identity);
                    Delay.transform.parent = collider.transform;

                    /*
                     * 해당 collider 적들에게 대미지 부가
                     */

                    StartCoroutine(WaitAndDestroy(particleSystemObject, Lightning,Delay, collider,2f));
                }

            }
        }

    }

    private void P_Skill_Stun()
    {
        Collider[] colliders = Physics.OverlapSphere(PlayerTr.position, Skill_Round * 3);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Vector3 enemyPosition = collider.transform.position;
                    particleSystemObject = Instantiate(Stun, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), Quaternion.identity);

                    /*
                     * 해당 collider 적들에게 대미지 부가
                     */
                    StartCoroutine(WaitAndDestroy(particleSystemObject, null, null, collider, 3.5f));
                }

            }
            Debug.Log("STUN END");
        }
    }

    IEnumerator WaitAndDestroy(GameObject Main_obj,GameObject obj,GameObject Sub_obj,Collider col,float delay)
    {
        if(obj == null)
        {
            col.GetComponent<MOVE>().speed = 0f;        //need to check
            yield return new WaitForSeconds(delay);
            Destroy(Main_obj);
            Destroy(obj);
            Destroy(Sub_obj);
            col.GetComponent<MOVE>().speed = 1.0f;
            Debug.Log("Stun Destroy");
        }
        else
        {
            col.GetComponent<MOVE>().speed = 0.3f;
            yield return new WaitForSeconds(delay);
            Destroy(Main_obj);
            Destroy(obj);
            Destroy(Sub_obj);
            col.GetComponent<MOVE>().speed = 1.0f;
            Debug.Log("DElay Destroy");
        }
    }
}
