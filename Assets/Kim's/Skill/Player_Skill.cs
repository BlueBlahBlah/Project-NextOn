using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    private int playerHp = 0;               //�ӽ� Ȯ�ο� HP
    private Transform PlayerTr;             //�÷��̾� ��ġ Ȯ�ο�
    private Vector3 ForwardPlayer;          //�÷��̾��� ���� Ȯ�ο�
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

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) P_Skill_WindHeal();       //Z �Է½�
        else if (Input.GetKeyDown(KeyCode.X)) P_Skill_Storm();      //X �Է½�
        else if (Input.GetKeyDown(KeyCode.C)) P_Skill_Skill_FireMine();     //C �Է½�
        else if (Input.GetKeyDown(KeyCode.V)) P_Skill_FireGuard();  //V �Է½� �ڷ�ƾ ����(�� �ʰ� �����)
    }
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
        Destroy(particleSystemObject);
    }
}
