using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // ���� �������̽��� �������� ��ġ�� �����ϴ� �������� �����ϰ�, �̸� �����ϴ� �Լ�����
    // �ۼ��� ��ũ��Ʈ��, 'InGameUI' �����տ� ���ԵǴ� ��ũ��Ʈ�Դϴ�.
    // ĵ������ �� ��Ҹ� �����ϰ�, ���� �Ŵ������� �����͸� ������ �˸��� �κп� �����ŵ�ϴ�.
    // ��, �����δ� <UIManager> ��ũ��Ʈ���� InGameUI ��ũ��Ʈ�� ������ ������Ʈ �Լ����� �����մϴ�.

    // ������ �̸� ���� �ʿ��� ���� [SerializeField]
    #region
    [Header("Player Information")]
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private Image playerIcon; // �÷��̾��� ������. ���¿� ���� ���� ����
    [SerializeField]
    private TextMeshProUGUI playerHp; // �÷��̾��� ü�� �ؽ�Ʈ
    [SerializeField]
    private Image playerHpBar; // �÷��̾��� ü�� ��

    [Header("Weapon Information")]
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private TextMeshProUGUI currentBullet;
    [SerializeField]
    private TextMeshProUGUI maxBullet;

    [Header("Boss Information")]
    [SerializeField]
    private GameObject bossInfo;
    [SerializeField]
    private Image bossIcon; // ���� ������
    [SerializeField]
    private TextMeshProUGUI bossName; // ���� �̸�
    [SerializeField]
    private TextMeshProUGUI bossHp; // ���� ü�� �ؽ�Ʈ (�ۼ�Ʈ)
    [SerializeField]
    private Image bossHpBar; // ���� ü�� ��

    [Header("Gimmick Information")]
    [SerializeField]
    private GameObject gimmickInfo;
    [SerializeField]
    private Image gimmickIcon; // ��� ������
    [SerializeField]
    private TextMeshProUGUI gimmickName; // ��� �̸�
    [SerializeField]
    private TextMeshProUGUI gimmickPercent; // ��� ���൵ (�ۼ�Ʈ)
    [SerializeField]
    private Image gimmickProgressBar; // ��� ���� ��

    [Header("Minimap")]
    [SerializeField]
    private TextMeshProUGUI numOfEnemy; // ���� ��

    [Header("Dialogue")]
    [SerializeField]
    private GameObject dialogue; // ��ȭâ ������Ʈ
    [SerializeField]
    private Image dialogueImage; // ��ȭ ĳ���� �̹���
    [SerializeField]
    private TextMeshProUGUI dialogueName; // ��ȭ ĳ���� �̸�
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // ��ȭ ����
    [SerializeField]
    private float typingSpeed = 0.05f; // ��ȭ ��� �ӵ�
    #endregion

    // ���� ������ ���� Ȥ�� ��ũ��Ʈ ������ InGameUI Ŭ���� ������ ��������
    #region
    public float PlayerHp; // �÷��̾� ���� ü�� ����
    public float PlayerMaxHp; // �÷��̾� �ִ� ü�� ����

    public string WeaponName; // ���� �̸� ���� << �̹��� ȣ���
    public int CurrentBullet; // ���� ���� �Ѿ� �� ����
    public int MaxBullet; // ���� �ִ� �Ѿ� �� ����

    public bool isBoss; // ���� ���� ���� ����
    public float BossHp; // ���� ü�� ����
    public float BossMaxHp; // ���� �ִ� ü�� ����
    public string BossName; // ���� �̸� ����

    public bool isGimmick; // ��� ���� ���� ����
    public float GimmickPercent; // ��� ���൵ ����
    public int GimmickCount;
    public string GimmickName; // ��� �̸� ����

    public int NumOfEnemy; // �ʵ� �� ���� �� ����

    [SerializeField]
    private StageManager stageManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // GetManager => �̱������� ������� ���� �Ŵ������� �����ɴϴ�.
    // �Ŵ������� �̱������� �����ϵ��� ������ �����Ѵٸ� ȣ�� ����� �ٲߴϴ�.
    public void GetManager()
    {
        GetStageManager();
    }

    public void GetStageManager()
    {
        if (GameObject.Find("StageManager") != null) 
            stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Initailize
    #region
    public void InitWeaponInfo() // ���� ���� �ʱ�ȭ << ���� 1�� ��� �� ���� ����
    {
        // �̹��� �ʱ�ȭ
        weaponImage.sprite = Resources.Load($"UI/Image/Icons/Weapons/{WeaponName}", typeof(Sprite)) as Sprite;

        // �Ѿ� �� �ʱ�ȭ
        UpdateBullet();
    }
    #endregion

    // Update **�������� �������� �ٸ� �Ŵ������� ȣ�� �� �� �ֵ��� ���� �ʿ�
    #region
    public void UpdatePlayerInfo() // �÷��̾� ���� ����
    {
        // ü�� ����
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // ü�� ����
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // ü�¹� �̹��� ����
    }

    public void UpdateBullet() // �Ѿ� ���� 
    {
        currentBullet.text = CurrentBullet.ToString();
        maxBullet.text = MaxBullet.ToString();
    }

    public void UpdateBossInfo() // ���� ���� ����
    {
        if (isBoss)
        {
            // ���� ���� ��
            if (!bossInfo.activeInHierarchy)
            {
                // ������ ���� ������, UI�� ������ ���� ���¿��� ȣ��
                // >> ���� Info �ʱ�ȭ
                // ������ �̸�, �̹��� �� ������Ʈ
                bossName.text = BossName;
                bossIcon.sprite = Resources.Load($"UI/Image/Icons/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
                bossInfo.SetActive(true);// UI Ȱ��ȭ
            }
            // ���� ���� ����
            bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // ü�� ����
            bossHpBar.fillAmount = BossHp / BossMaxHp; // ü�¹� �̹��� ����
        }
        else
        {
            // ���� óġ ��, �ʵ� ���� ������ �������� ���� �� 
            // >> ���� Info ��Ȱ��ȭ
            if (bossInfo.activeInHierarchy) bossInfo.SetActive(false);
        }
    }

    public void UpdateGimmickInfo() // ��� ���� ����
    {
        if (isGimmick)
        {
            if (!gimmickInfo.activeInHierarchy)
            {
                // ����� ���� ������, UI�� ������ ���� ���¿��� ȣ��
                // >> ��� Info �ʱ�ȭ
                // ����� �̸�, �̹��� �� ������Ʈ
                gimmickName.text = GimmickName;
                gimmickIcon.sprite = Resources.Load($"UI/Image/Icons/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

                // ��� ȣ�� �� 0%�� �����Ѵٰ� ����
                gimmickPercent.text = "0%";
                gimmickProgressBar.fillAmount = 0f;

                gimmickInfo.SetActive(true);
            }

            // ����� ���, ����� Ÿ�� (�ð� ��� / Ƚ�� ���)�� ���� �ٸ��� �ڵ� �ۼ�

        }
        else
        {
            // ��� ���� ����
            if (gimmickInfo.activeInHierarchy) gimmickInfo.SetActive(false);
        }

    }

    public void UpdateNumOfEnemy()
    {
        // ���Ͱ� ����, Ȥ�� ��� �� �̴ϸ� �ϴ��� ���� �� ����
        if (stageManager != null)
        {
            NumOfEnemy = stageManager.enemies.Length;
        }
        else
        {
            Debug.Log("There's no stageManager!");
        }
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion

 

    // Button


    
}
