using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Space_PlayerManager : MonoBehaviour
{

    public static Space_PlayerManager instance = null;
    /*[SerializeField] private Rifle rifle = GameObject.FindObjectOfType<Rifle>();
    [SerializeField] private Shotgun shotgun = GameObject.FindObjectOfType<Shotgun>();
    [SerializeField] private Sniper sniper = GameObject.FindObjectOfType<Sniper>();
    [SerializeField] private GrenadeLauncher grenadeLauncher = GameObject.FindObjectOfType<GrenadeLauncher>();
    [SerializeField] private MachineGun machineGun = GameObject.FindObjectOfType<MachineGun>();
    [SerializeField] private FireGun fireGun = GameObject.FindObjectOfType<FireGun>();*/


    public float TotalHealth;                          //�ִ�ü��
    public float Health;                          //����ü��
    public float HealthGen;                     //ü��
    public int DefensivePower;                  //����
    public int MovingSpeed;                     //�̵��ӵ�
    public int PlayerPlainHitDamage;            //��Ÿ ���ݷ�
    public int PlayerSkillDamage;               //��ų ���ݷ�
    public int CurrentBullet;                   //���� ��ź ��
    public int TotalBullet;                     //���� źâ ��

    public float SkillCoolTimeRate;                     //�������� ��Ÿ�Ӱ�����

    public bool Death;

    public enum WeaponType
    {
        nonType,
        closeType,
        longType
    }
    [SerializeField] private GameObject player_LongWeapon;
    [SerializeField] private GameObject player_NonWeapon;
    [SerializeField] private GameObject player_CloseWeapon;
    [SerializeField] private List<GameObject> player_WeaponList;
    [SerializeField] private Button attackBtn;

    public DropItemPosition _dropItemPosition;


    public int revive;      //��Ȱ Ƚ��
    private bool revive_decrease_once;      //��Ȱ Ƚ���� 1�� ���̱� ���� ����

    private void Awake()
    {

        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            //Destroy(this.gameObject);
        }
    }
    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
    public static Space_PlayerManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        player_NonWeapon = GameObject.Find("Check_Sprite");
        player_CloseWeapon = GameObject.Find("Check_Sprite_Short");

        // player_NonWeapon�� ���� ������(���� �θ� ����) ������Ʈ�� ã��
        Transform parentTransform = player_NonWeapon.transform.parent;
        foreach (Transform sibling in parentTransform)
        {
            DropItemPosition dropItemPosition = sibling.GetComponent<DropItemPosition>();

            if (dropItemPosition != null)
            {
                // DropItemPosition�� ���� ������Ʈ�� ã���� �� _dropItemPosition�� ����
                _dropItemPosition = dropItemPosition;
                break;
            }
        }

        player_LongWeapon.SetActive(false);
        player_CloseWeapon.SetActive(false);

        TotalHealth = 100;  //���� �� ü�� 100
        Health = TotalHealth;
        SkillCoolTimeRate = 0;     //���� �� ��ų ��Ÿ��
        Death = false;
    }

    /*void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        init();
    }

    private void init()
    {
        TotalHealth = 100;  //���� �� ü�� 100
        Health = TotalHealth;
        SkillCoolTimeRate = 0;     //���� �� ��ų ��Ÿ��
        Death = false;
        /*player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        player_NonWeapon = GameObject.Find("Check_Sprite");
        player_CloseWeapon = GameObject.Find("Check_Sprite_Short");*/
        Debug.Log("********************tlqkf*******************");

        // �� ���� ����Ʈ�� �߰��� ������Ʈ �̸�
        string[] longWeaponNames = {
            "FlameGun",
            "SM_Wep_MachineGun_01",
            "SM_Wep_Grenade_Launcher_01",
            "SM_Wep_Sniper_Mil_01",
            "SM_Wep_Rifle_Assault_01",
            "SM_Wep_Shotgun_01"
        };

        string[] closeWeaponNames = {
            "SwordStatic",
            "SwordStreamOfEgde",
            "SwordSilver",
            "SwordDemacia",
            "FantasyAxe_Unity"
        };

        // parent ������Ʈ ã��
        if (GameObject.Find("Check_Sprite_Long") is not null)
        {
            player_LongWeapon = GameObject.Find("Check_Sprite_Long");
        }
        if (GameObject.Find("Check_Sprite") is not null)
        {
            player_NonWeapon = GameObject.Find("Check_Sprite");
        }
        if (GameObject.Find("Check_Sprite_Short") is not null)
        {
            player_CloseWeapon = GameObject.Find("Check_Sprite_Short");
        }




        // ��� �ڽ� ������Ʈ�� ��������
        Transform[] longWeaponChildren = player_LongWeapon.GetComponentsInChildren<Transform>(true);
        Transform[] closeWeaponChildren = player_CloseWeapon.GetComponentsInChildren<Transform>(true);

        // longWeapon �ڽ� �� ���� �̸��� ��ġ�ϴ� ������Ʈ �߰�
        foreach (string weaponName in longWeaponNames)
        {
            foreach (Transform child in longWeaponChildren)
            {
                if (child.name == weaponName)
                {
                    player_WeaponList.Add(child.gameObject);
                    break;
                }
            }
        }

        // closeWeapon �ڽ� �� ���� �̸��� ��ġ�ϴ� ������Ʈ �߰�
        foreach (string weaponName in closeWeaponNames)
        {
            foreach (Transform child in closeWeaponChildren)
            {
                if (child.name == weaponName)
                {
                    player_WeaponList.Add(child.gameObject);
                    break;
                }
            }
        }


        // player_NonWeapon�� ���� ������(���� �θ� ����) ������Ʈ�� ã��
        Transform parentTransform = player_NonWeapon.transform.parent;

        // �θ��� �ڽ� ������Ʈ�� �� DropItemPosition�� ������ �ִ� ������Ʈ ã��
        foreach (Transform sibling in parentTransform)
        {
            DropItemPosition dropItemPosition = sibling.GetComponent<DropItemPosition>();

            if (dropItemPosition != null)
            {
                // DropItemPosition�� ���� ������Ʈ�� ã���� �� _dropItemPosition�� ����
                _dropItemPosition = dropItemPosition;
                break;
            }
        }

        int i;
        for (i = 0; i < player_WeaponList.Count; i++)
        {
            player_WeaponList[i].SetActive(false);
        }

        player_LongWeapon.SetActive(false);
        player_CloseWeapon.SetActive(false);

        revive = 3;
        revive_decrease_once = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (player_NonWeapon is null)
        {
            init();
        }
        if (Health <= 0)        //ü���� �� ���� ���
        {
            Health = 0;         //ü�¹ٰ� ������� ���� ����
            if (revive_decrease_once == false)
            {
                revive_decrease_once = true;
                revive -= 1;
                if (revive >= 1)
                    revive_Health_Invoke();
                else if (revive == 0)
                {
                    //��¥ ����
                    EventManager.Instance.fadeout();
                    //�� �̵��ϴ� �ڵ�
                }
            }


        }
        //���� �ѱ���� �������
        if (player_LongWeapon.activeSelf)
        {
            player_LongWeapon.GetComponent<Space_PlayerScriptRifle>().BulletInfo();
            //Debug.LogError("���� ��ź "  + CurrentBullet);
            //Debug.LogError("�� ��ź "  + TotalBullet);
        }
        else
        {
            //��ź�� ���� ���Ѵ�� �ϴ� �ڵ�
        }
    }


    public void find_attackBtn_Invoke()
    {
        Invoke("find_attackBtn", 3f);
    }
    private void find_attackBtn()
    {
        attackBtn = GameObject.Find("FireBtn").GetComponent<Button>();
    }

    private void revive_Health_Invoke()
    {
        Invoke("revive_Health", 5.5f);
    }

    public void revive_Health()
    {
        Health = 100;
        revive_decrease_once = false;
        Death = false;
    }
    public void ChangeWeapon(WeaponType Wt, GameObject Weapon)
    {
        //�𵨸� Ȱ��ȭ
        if (Wt == WeaponType.closeType)
        {
            player_LongWeapon.SetActive(false);
            player_NonWeapon.SetActive(false);
            player_CloseWeapon.SetActive(true);
            //���������� ��� ���⿡�� ��ư �̺�Ʈ�� ����ϴ� ���� �ƴϱ⿡ �������� ����� ���⼭ ���
            attackBtn.onClick.AddListener(player_CloseWeapon.GetComponent<Space_PlayerScriptOneHand>().OnAttackButtonClick);
        }
        else if (Wt == WeaponType.longType)
        {
            player_LongWeapon.SetActive(true);
            player_NonWeapon.SetActive(false);
            player_CloseWeapon.SetActive(false);
        }
        else if (Wt == WeaponType.nonType)
        {
            player_LongWeapon.SetActive(false);
            player_NonWeapon.SetActive(true);
            player_CloseWeapon.SetActive(false);
        }
        //���� Ȱ��ȭ
        foreach (GameObject g in player_WeaponList)
        {
            if (Weapon == g)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
        }

        if (Wt == WeaponType.closeType)
        {
            try
            {
                player_CloseWeapon.GetComponent<Space_PlayerScriptOneHand>().WeaponSynchronization();  //���� ���� ���� �ٽ� Ž��
            }
            catch (NullReferenceException e)
            {

            }

        }
        else if (Wt == WeaponType.longType)
        {
            try
            {
                player_LongWeapon.GetComponent<Space_PlayerScriptRifle>().WeaponSynchronization();    //���� ���� ���� �ٽ� Ž��
            }
            catch (NullReferenceException e)
            {

            }
        }
    }


    //���Ÿ� ���Ⱑ �ٲ� ���� ����ִ� ������ ź�� ��������
    //���� ������ ��� ��ź�� ���� ���Ѵ�� �ϸ�
    //-> ���� ����ִ� ������� �Ǵ�
}

