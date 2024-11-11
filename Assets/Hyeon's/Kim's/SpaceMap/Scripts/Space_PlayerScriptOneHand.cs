using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Space_PlayerScriptOneHand : MonoBehaviour
{


    public float moveSpeed = 0f;
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    [SerializeField] private int Damage;

    public Button attackBtn;

    [SerializeField] private BoxCollider DamageZone;
    //public Button RollBtn;

    [SerializeField] private SwordStreamOfEdge SwordStreamOfEdge;
    [SerializeField] private SwordStatic SwordStatic;
    [SerializeField] private SwordSilver SwordSilver;
    [SerializeField] private SwordDemacia SwordDemacia;
    [SerializeField] private FantasyAxe FantasyAxe;

    private bool temporary_death;           //�ӽ� ���� ����ġ - Update ����
    private bool stop_update;               //update ����

    //[SerializeField] private DamageManager DamageManager;

    void Start()
    {
        // �ʱ� ��ġ ����
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        attackBtn.onClick.AddListener(OnAttackButtonClick);
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //�������ư

        try
        {
            WeaponSynchronization();
        }
        catch (NullReferenceException e)
        {

        }
        temporary_death = false;
        stop_update = false;
    }

    public void WeaponSynchronization()
    {
        /*SwordStreamOfEdge = GameObject.Find("SwordStreamOfEgde").GetComponent<SwordStreamOfEdge>();
        SwordStatic = GameObject.Find("SwordStatic").GetComponent<SwordStatic>();
        SwordSilver = GameObject.Find("SwordSilver").GetComponent<SwordSilver>();
        SwordDemacia = GameObject.Find("SwordDemacia").GetComponent<SwordDemacia>();
        FantasyAxe = GameObject.Find("FantasyAxe_Unity").GetComponent<FantasyAxe>();*/
    }

    void Update()
    {
        if (stop_update == false)
        {
            if (PlayerManager.Instance.Health <= 0)        //ü���� �� ���� ���
            {
                GetComponentInParent<NewPlayer_Locomotion>().enabled = false;
                Anim.applyRootMotion = true;
                Anim.SetTrigger("Death");
                stop_update = true;
            }
            else
            {

                // ���� ��ġ�� ���� ��ġ ��
                if (transform.position != lastPosition)
                {
                    walking = true;
                    // ��ġ�� ����Ǿ��� ���� �Ʒ� �ڵ� ����

                    // �̵� ���� ����
                    Vector3 moveDirection = (transform.position - lastPosition).normalized;

                    // ������ ó��
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                    // ȸ�� ó��
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);

                    // ���� ��ġ�� ���� ��ġ�� ������Ʈ
                    lastPosition = transform.position;
                }
                else
                {
                    walking = false;
                }
                Anim.SetBool("walk", walking);
            }
        }


    }

    public void OnAttackButtonClick()
    {
        Anim.SetTrigger("attack");
        //ColliderAttack();         //�ִϸ��̼ǿ� �̺�Ʈ�� �־��
    }

    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    void ColliderAttack()       //�ִϸ��̼� ȣ��
    {
        Damage = 0;
        if (SwordStreamOfEdge != null && SwordStreamOfEdge.gameObject.activeSelf)
        {
            Damage = SwordStreamOfEdge.GetComponent<SwordStreamOfEdge>().Damage;
            Damage *= DamageManager.Instance.SwordStreamEdge_DamageCounting;
        }
        else if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
        {
            Damage = SwordStatic.GetComponent<SwordStatic>().Damage;
            Damage *= DamageManager.Instance.SwordStatic_DamageCounting;
        }
        else if (SwordSilver != null && SwordSilver.gameObject.activeSelf)
        {
            Damage = SwordSilver.GetComponent<SwordSilver>().Damage;
            Damage *= DamageManager.Instance.SwordSliver_DamageCounting;
        }
        else if (SwordDemacia != null && SwordDemacia.gameObject.activeSelf)
        {
            Damage = SwordDemacia.GetComponent<SwordDemacia>().Damage;
            Damage *= DamageManager.Instance.SwordDemacia_DamageCounting;
        }
        else if (FantasyAxe != null && FantasyAxe.gameObject.activeSelf)
        {
            Damage = FantasyAxe.GetComponent<FantasyAxe>().Damage;
            Damage *= DamageManager.Instance.FantasyAxe_DamageCounting;
        }

        Collider[] hitColliders = Physics.OverlapBox(DamageZone.bounds.center, DamageZone.bounds.extents,
            DamageZone.transform.rotation);
        PlayerSoundManager.Instance.Close_Attack_1_Sound();
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // ������ = ��� * ���� ������
                col.GetComponent<Enemy>().curHealth -= Damage;         //��� �߰�
                if (SwordStatic != null && SwordStatic.gameObject.activeSelf)
                {
                    SwordStatic.GetComponent<SwordStatic>().attackNum++;        //����ƽ�� ��� ���� �߰�
                }
            }
        }
    }

    private void after_Death_Animation()
    {
        //���� ��Ȱ�� �����ִٸ�
        if (PlayerManager.Instance.revive >= 1)
        {
            temporary_death = true;
            Anim.SetTrigger("Revive");        //�ִϸ��̼� Ʈ����
            PlayerSoundManager.Instance.revive_Sound();
        }
    }

    private void start_Landing_Animation()
    {
        //���� �о�� �ڵ�

        float pushRadius = 10f;  // �ݰ� 10
        float pushForce = 5f;    // �о�� ��
        float pushDuration = 1f; // �о�� �ð� (��)


        // Ư�� �ݰ� ���� ��� �ݶ��̴��� ã��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (Collider collider in hitColliders)
        {
            // Enemy �±׸� ���� ������Ʈ���� Ȯ��
            if (collider.CompareTag("Enemy"))
            {
                // ������Ʈ�� NavMeshAgent ������
                NavMeshAgent agent = collider.GetComponent<NavMeshAgent>();

                if (agent != null)
                {
                    // ���� ���: �÷��̾�� �������� ����
                    Vector3 pushDirection = collider.transform.position - transform.position;
                    pushDirection.y = 0; // y�����δ� ���� �������� y�� 0���� ����

                    // NavMeshAgent�� �̵� �ӵ��� �� ���� (����� �ӵ� ����)
                    Vector3 pushVelocity = pushDirection.normalized * pushForce;

                    // ���� ���� �ð� ���� �о�� �ڷ�ƾ ����
                    StartCoroutine(PushEnemy(agent, pushVelocity));
                }
            }
        }
    }

    // ���� ���� �ð� ���� �о�� �ڷ�ƾ
    System.Collections.IEnumerator PushEnemy(NavMeshAgent agent, Vector3 pushVelocity)
    {
        float pushDuration = 1f; // �о�� �ð� (��)
        float elapsedTime = 0;

        // �о�� ���� NavMeshAgent�� ��θ� ���� �������� �ʵ��� ��Ȱ��ȭ
        agent.isStopped = true;

        // pushDuration ���� �о��
        while (elapsedTime < pushDuration)
        {
            agent.Move(pushVelocity * Time.deltaTime); // NavMeshAgent�� Move()�� ���� �̵� ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �о �� NavMeshAgent ��Ȱ��ȭ
        agent.isStopped = false;
    }

    private void after_Landing_Animation()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponentInParent<NewPlayer_Locomotion>().enabled = true;
        Anim.applyRootMotion = false;
        temporary_death = false;
        stop_update = false;
        //PlayerManager.Instance.Health = 100;
    }

}
