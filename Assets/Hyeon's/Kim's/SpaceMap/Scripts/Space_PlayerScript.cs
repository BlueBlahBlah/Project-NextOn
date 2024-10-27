using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Space_PlayerScript : MonoBehaviour
{
    public float moveSpeed = 0f;
    public bool walking;
    private Vector3 lastPosition;
    Animator Anim;
    public Button RollBtn;

    private bool temporary_death;           //�ӽ� ���� ����ġ - Update ����
    private bool stop_update;               //update ����


    void Start()
    {
        // �ʱ� ��ġ ����
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        //RollBtn.onClick.AddListener(OnRollButtonClick);         //�������ư
        temporary_death = false;
        stop_update = false;
    }

    void Update()
    {
        if (stop_update == false)
        {
            //transform.position = new Vector3(0, 0, 0);
            if (PlayerManager.Instance.Health <= 0)        //ü���� �� ���� ���
            {
                GetComponentInParent<NewPlayer_Locomotion>().enabled = false;
                Anim.applyRootMotion = true;
                Anim.SetTrigger("Death");
                stop_update = true;
            }
            else
            {
                if (temporary_death == false)
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




            if (transform.position.y < -10)
            {
                this.transform.position = new Vector3(0, 0, 43);
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
        GetComponentInParent<CharacterLocomotion>().enabled = true;
        Anim.applyRootMotion = false;
        temporary_death = false;
        stop_update = false;
        //PlayerManager.Instance.Health = 100;
    }

    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");        //�ִϸ��̼� Ʈ����

    }


}
