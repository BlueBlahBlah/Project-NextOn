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
        if (Input.GetKeyDown(KeyCode.Z)) P_Skill_WindHeal();                //Z �Է½�
        else if (Input.GetKeyDown(KeyCode.X)) P_Skill_Storm();              //X �Է½�
        else if (Input.GetKeyDown(KeyCode.C)) P_Skill_Skill_FireMine();     //C �Է½�
        else if (Input.GetKeyDown(KeyCode.V)) P_Skill_FireGuard();          //V �Է½�
    }
    /*  
        1.�� ��ų
    �÷��̾� ĳ���� �ֺ����� �ʷϻ� ������ �������� ������
    -�÷��̾��� ü�� ����
        2.�ֺ� ���� ��ų
    �÷��̾� ĳ���� �ֺ����� ����ĸ� ������ ���ظ� ��
    -�ش� ����(skill_Round)�̳� �����ϴ� ��� ������ �ǰ� ������ �ΰ�
         3.���� ��ġ ��ų
    �÷��̾ �����ϴ� ��ġ�� ���� ��ġ�� ���� �ð��� ������ ���� ������ �ΰ�
    -�����ð��� 3f �� ������
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

        foreach (Collider collider in colliders) // ������ Collider �迭�� ��ȸ�ϸ� ���ϴ� ���� ����
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
