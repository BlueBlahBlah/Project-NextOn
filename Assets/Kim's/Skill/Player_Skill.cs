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
    GameObject zonePre;

    [Header("HealSkill")]
    public GameObject Skill_WindHeal;
    [Header("SplashSkill")]
    public GameObject Skill_Storm;
    [Header("ShootingSkill")]
    public GameObject Skill_Meteor;
    [Header("GuardSkill")]
    public GameObject Skill_FireGuard;
    [Header("MineSkill")]
    public GameObject Skill_FireMine;
    [Header("ElectDelaySkill")]
    public GameObject Skill_Spark;
    public GameObject Skill_Lightning;
    public GameObject Skill_DelayZone;
    [Header("StunSKill")]
    public GameObject Skill_Stun;
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
        particleSystemObject = Instantiate(Skill_WindHeal, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;
        /*
         * 플레이어 체력회복
         * playerHp += 10;
        */
        Debug.Log($"After Hp :{playerHp}");
    }

    private void P_Skill_Storm()
    {
        particleSystemObject = Instantiate(Skill_Storm, PlayerTr.position, Quaternion.identity);
        particleSystemObject.transform.localScale = new Vector3(Skill_Round, Skill_Round, Skill_Round);
        particleSystemObject.transform.parent = this.transform;

        Collider[] colliders = Physics.OverlapSphere(transform.position, Skill_Round);

        foreach (Collider collider in colliders) //collider 에 대미지 부가
        {
            /*
                * 해당 collider 적들에게 대미지 부가
            */
            Debug.Log($"enemy hp: collider.hp");
        }

    }

    private void P_Skill_FireMine()
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

        StartCoroutine(WaitAndDestroy(particleSystemObject, null, null,null,10f));
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
                    particleSystemObject = Instantiate(Skill_Stun, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), Quaternion.identity);

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
            col.GetComponent<MOVE>().speed = 0f;
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
