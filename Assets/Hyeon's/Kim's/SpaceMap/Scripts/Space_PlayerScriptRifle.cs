using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Space_PlayerScriptRifle : MonoBehaviour
{

    public float moveSpeed = 5f;
    public bool walking;
    public bool reloaing;
    private Vector3 lastPosition;
    Animator Anim;
    public Button RollBtn;
    public bool[] NowWeapon;

    public bool isMovingForward;
    public bool isMovingBackward;
    public bool isMovingRight;
    public bool isMovingLeft;
    [SerializeField] private NewPlayer_Locomotion playerMovingScript;
    [SerializeField] private Rifle rifle;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private Sniper sniper;
    [SerializeField] private GrenadeLauncher grenadeLauncher;
    [SerializeField] private MachineGun machineGun;
    [SerializeField] private FireGun fireGun;

    private bool temporary_death;           //�ӽ� ���� ����ġ - Update ����
    private bool stop_update;               //update ����

    void Start()
    {
        // �ʱ� ��ġ ����
        lastPosition = transform.position;
        walking = false;
        Anim = GetComponentInChildren<Animator>();
        RollBtn.onClick.AddListener(OnRollButtonClick);         //�������ư
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

    void Update()
    {
        if (stop_update == false)
        {
            if (PlayerManager.Instance.Health <= 0) //ü���� �� ���� ���
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

                    // �̵� ���� ����
                    Vector3 moveDirection = (transform.position - lastPosition).normalized;

                    // �̵� ������ �������� ��, ��, ������, ���� ���� �Ǵ�
                    float angle = Vector3.SignedAngle(moveDirection, transform.forward, Vector3.up);
                    //Debug.LogError(angle);
                    if (angle > 45f && angle < 135f)
                    {
                        isMovingLeft = true;
                        isMovingRight = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        playerMovingScript.walkSpeed = 5;

                    }
                    else if (angle < -45f && angle > -135f)
                    {
                        isMovingRight = true;
                        isMovingLeft = false;
                        isMovingForward = false;
                        isMovingBackward = false;
                        playerMovingScript.walkSpeed = 5;
                    }
                    else if (angle > 70 || angle < -70)
                    {
                        isMovingLeft = false;
                        isMovingRight = false;
                        if (Vector3.Dot(moveDirection, transform.forward) > 0)      //�Ⱦ��̴� �ڵ��ε�? forward�� �Ʒ� else��
                        {
                            isMovingForward = true;
                            isMovingBackward = false;
                            playerMovingScript.walkSpeed = 5;
                        }
                        else
                        {
                            isMovingForward = false;
                            isMovingBackward = true;
                            playerMovingScript.walkSpeed = 5;
                        }
                    }
                    else
                    {
                        isMovingForward = true;
                        isMovingBackward = false;
                        isMovingLeft = false;
                        isMovingRight = false;
                        playerMovingScript.walkSpeed = 5;
                    }
                    // ������ ó��
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                    // ȸ�� ó��
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 100f);

                    // ���� ��ġ�� ���� ��ġ�� ������Ʈ
                    lastPosition = transform.position;

                }
                else
                {
                    walking = false;
                    isMovingForward = false;
                    isMovingBackward = false;
                    isMovingRight = false;
                    isMovingLeft = false;
                }
                Anim.SetBool("walk", walking);
                Anim.SetBool("Front", isMovingForward);
                Anim.SetBool("Back", isMovingBackward);
                Anim.SetBool("Left", isMovingLeft);
                Anim.SetBool("Right", isMovingRight);


                if (reloaing == true)       //���������̶��
                {
                    PlayerSoundManager.Instance.reload_Sound();
                    Anim.SetBool("reload", true);       //������ �ִϸ��̼�
                    reloaing = false;
                    Invoke("reloadDone", 4);      //4���� ������ ��
                }
            }
        }


    }

    public void WeaponSynchronization()
    {
        /*rifle = GameObject.FindObjectOfType<Rifle>();
        shotgun = GameObject.FindObjectOfType<Shotgun>();
        sniper = GameObject.FindObjectOfType<Sniper>();
        grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
        machineGun = GameObject.FindObjectOfType<MachineGun>();
        fireGun = GameObject.FindObjectOfType<FireGun>();*/
    }

    private void reloadDone()
    {
        Anim.SetBool("reload", false);
        try
        {
            WeaponSynchronization();
        }
        catch (NullReferenceException e)
        {

        }
        if (rifle != null && rifle.gameObject.activeSelf)
        {
            if (rifle.maxBulletCount >= 30)
            {
                rifle.maxBulletCount -= 30;         //ź �ܷ� 30 ����
                rifle.bulletCount += 30; // 30�� �߰�
                rifle.nowReloading = false; // ���� ���� ��
            }
            else
            {
                rifle.bulletCount += rifle.maxBulletCount; //���� ź ������ ������
                rifle.maxBulletCount = 0;    //��ź �Ҹ�
                rifle.nowReloading = false; // ���� ���� ��
            }

        }
        if (shotgun != null && shotgun.gameObject.activeSelf)
        {
            if (shotgun.maxBulletCount >= 15)
            {
                shotgun.maxBulletCount -= 15;         //ź �ܷ� 15 ����
                shotgun.bulletCount += 15; // 15�� �߰�
                shotgun.nowReloading = false; // ���� ���� ��
            }
            else
            {
                shotgun.bulletCount += shotgun.maxBulletCount; //���� ź ������ ������
                shotgun.maxBulletCount = 0;    //��ź �Ҹ�
                shotgun.nowReloading = false; // ���� ���� ��
            }
        }
        if (sniper != null && sniper.gameObject.activeSelf)
        {
            if (sniper.maxBulletCount >= 15)
            {
                sniper.maxBulletCount -= 15;         //ź �ܷ� 15 ����
                sniper.bulletCount += 15; // 15�� �߰�
                sniper.nowReloading = false; // ���� ���� ��
            }
            else
            {
                sniper.bulletCount += sniper.maxBulletCount; //���� ź ������ ������
                sniper.maxBulletCount = 0;    //��ź �Ҹ�
                sniper.nowReloading = false; // ���� ���� ��
            }
        }
        if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
        {
            if (grenadeLauncher.maxBulletCount >= 30)
            {
                grenadeLauncher.maxBulletCount -= 30;         //ź �ܷ� 30 ����
                grenadeLauncher.bulletCount += 30; // 30�� �߰�
                grenadeLauncher.nowReloading = false; // ���� ���� ��
            }
            else
            {
                grenadeLauncher.bulletCount += grenadeLauncher.maxBulletCount; //���� ź ������ ������
                grenadeLauncher.maxBulletCount = 0;    //��ź �Ҹ�
                grenadeLauncher.nowReloading = false; // ���� ���� ��
            }
        }
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            if (machineGun.maxBulletCount >= 100)
            {
                machineGun.maxBulletCount -= 100;         //ź �ܷ� 100 ����
                machineGun.bulletCount += 100; // 100�� �߰�
                machineGun.nowReloading = false; // ���� ���� ��
            }
            else
            {
                machineGun.bulletCount += machineGun.maxBulletCount; //���� ź ������ ������
                machineGun.maxBulletCount = 0;    //��ź �Ҹ�
                machineGun.nowReloading = false; // ���� ���� ��
            }
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            if (fireGun.maxBulletCount >= 100)
            {
                fireGun.maxBulletCount -= 100;         //ź �ܷ� 50 ����
                fireGun.bulletCount += 100; // 50�� �߰�
                fireGun.nowReloading = false; // ���� ���� ��
            }
            else
            {
                fireGun.bulletCount += fireGun.maxBulletCount; //���� ź ������ ������
                fireGun.maxBulletCount = 0;    //��ź �Ҹ�
                fireGun.nowReloading = false; // ���� ���� ��
            }
        }

    }

    void OnRollButtonClick()
    {
        Anim.SetTrigger("roll");
    }

    //PlayerManager�� ���� ź �ܷ� ������ ����
    public void BulletInfo()
    {
        if (rifle != null && rifle.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = rifle.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = rifle.bulletCount;
            return;
        }
        if (shotgun != null && shotgun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = shotgun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = shotgun.bulletCount;
            return;
        }
        if (sniper != null && sniper.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = sniper.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = sniper.bulletCount;
            return;
        }
        if (grenadeLauncher != null && grenadeLauncher.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = grenadeLauncher.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = grenadeLauncher.bulletCount;
            return;
        }
        if (machineGun != null && machineGun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = machineGun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = machineGun.bulletCount;
            return;
        }
        if (fireGun != null && fireGun.gameObject.activeSelf)
        {
            PlayerManager.Instance.TotalBullet = fireGun.maxBulletCount;
            PlayerManager.Instance.CurrentBullet = fireGun.bulletCount;
            return;
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
