using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{

    private int playerHp = 0;               //�ӽ� Ȯ�ο� HP
    private Transform PlayerTr;             //�÷��̾� ��ġ Ȯ�ο�
    private Vector3 PlayerForward;          //�÷��̾��� ���� Ȯ�ο�
    private float Skill_Round = 2.0f;       //��ų ���� ������
    GameObject particleSystemObject;        //��ų ������ ���
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
        if (Input.GetKeyDown(KeyCode.Z)) P_Skill_WindHeal();                //Z �Է½� Heal
        else if (Input.GetKeyDown(KeyCode.X)) P_Skill_Storm();              //X �Է½� Splash Skill
        else if (Input.GetKeyDown(KeyCode.C)) P_Skill_FireMine();           //C �Է½� Mine Skill
        else if (Input.GetKeyDown(KeyCode.V)) P_Skill_FireGuard();          //V �Է½� Guard Skill
        else if (Input.GetKeyDown(KeyCode.B)) P_Skill_Spark();              //B �Է½� Delay Skill
        else if (Input.GetKeyDown(KeyCode.N)) P_Skill_Stun();               //N �Է½� Stun Skill
    }

    void OnDrawGizmos() //����Ȯ�ο� �����
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
         * �÷��̾� ü��ȸ��
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

        foreach (Collider collider in colliders) //collider �� ����� �ΰ�
        {
            /*
                * �ش� collider ���鿡�� ����� �ΰ�
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
                     * �ش� collider ���鿡�� ����� �ΰ�
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
                     * �ش� collider ���鿡�� ����� �ΰ�
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
