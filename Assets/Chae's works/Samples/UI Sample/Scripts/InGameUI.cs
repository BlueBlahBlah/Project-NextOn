using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InGameUI : MonoBehaviour
{
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
    private float PlayerHp; // �÷��̾� ���� ü�� ����
    private float PlayerMaxHp; // �÷��̾� �ִ� ü�� ����

    private string WeaponName; // ���� �̸� ���� << �̹��� ȣ���
    private int CurrentBullet; // ���� ���� �Ѿ� �� ����
    private int MaxBullet; // ���� �ִ� �Ѿ� �� ����

    private bool isBoss; // ���� ���� ���� ����
    private float BossHp; // ���� ü�� ����
    private float BossMaxHp; // ���� �ִ� ü�� ����
    private string BossName; // ���� �̸� ����

    private bool isGimmick; // ��� ���� ���� ����
    private float GimmickPercent; // ��� ���൵ ����
    private int GimmickCount;
    private string GimmickName; // ��� �̸� ����

    private int NumOfEnemy; // �ʵ� �� ���� �� ����

    private bool isDialogue; // ��ȭâ ǥ�� ����
    private List<Dictionary<string, object>> data_Dialogue; // csv ���� ���� ����
    public int DialogueNumber; // ����� ��ȭ�� ��ȣ
    private float dialogueTime; // ��ȭ�� ���� (WaitforSeconds �Է� ����)
    private int dialogueIsContinuous; // �̾����� ��ȭ�� �ִ��� Ȯ���� ����

    [SerializeField]
    private StageManager stageManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); // ���̾�α� csv ���� ȣ��
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        FunctionTestStart(); // �׽�Ʈ�� �ڵ�
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(); // �÷��̾� info �׻� ������Ʈ
        UpdateBossInfo(); // ���� ���� �� ���� info ������Ʈ
        UpdateGimmickInfo(); // ��� ���� �� ��� info ������Ʈ
        UpdateNumOfEnemy(); // ���� �� ������Ʈ
        UpdateBullet();


        FunctionTestUpdate(); // �׽�Ʈ�� �ڵ�
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

    // Update
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
        NumOfEnemy = stageManager.enemies.Length;
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion

    // Event
    public void DialogueEvent(int _DialogueNumber) // ����� ��ȭ ��ȣ(csv ���� ����)�� �Է°����� ����
    {
        DialogueNumber = _DialogueNumber;

        if (isDialogue)
        {
            if (!dialogue.activeInHierarchy)
            {
                dialogue.SetActive(true);
            }
            string Name = data_Dialogue[DialogueNumber]["Character Name"].ToString();

            // �̹��� ����
            // ** ���� �ʿ��� ��1) csv�� ����� ĳ���� �̸��� �ѱ��ε�, ���ҽ����� Load�� �̸��� ����� ��.
            // - ���̵��
            // ��ȭ �̹����� ����� ĳ���Ͱ� ���� ������ Switch������ �޾ƿ� Name ��.
            // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/{Name}", typeof(Sprite)) as Sprite;

            // �̸� ����
            dialogueName.text = Name;

            

            dialogueTime = float.Parse(data_Dialogue[DialogueNumber]["Time"].ToString());
            dialogueIsContinuous = int.Parse(data_Dialogue[DialogueNumber]["Continuous"].ToString());

            Debug.Log($"Time : {dialogueTime}, Continuous : {dialogueIsContinuous}");
            // ���� ����
            StartCoroutine("TypeText");
        }
        else
        {
            if (dialogue.activeInHierarchy) dialogue.SetActive(false);
        }
    }

    // Button
    #region
    public void ButtonPause()
    {
        // ���� �÷��� Pause ���

        // �ٽ��ϱ�, Setting, ���� �� ���� UI ȣ��
    }
    #endregion

    // Coroutine
    #region
    IEnumerator TypeText()
    {
        // ���ڿ��� ���ʴ�� �Է��ϴ� �ڷ�ƾ
        for (int i = 0; i <= data_Dialogue[DialogueNumber]["Contents"].ToString().Length; i++)
        {
            ;
            dialogueContent.text = data_Dialogue[DialogueNumber]["Contents"].ToString().Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(dialogueTime);
        if (dialogueIsContinuous == 1)
        {
            DialogueNumber++;
            DialogueEvent(DialogueNumber);
        }
        else if (dialogueIsContinuous == 0)
        {
            isDialogue = false;
            DialogueEvent(DialogueNumber);
        }
        yield return null;
    }
    #endregion

    // Test
    #region
    private void FunctionTestStart()
    {
        // �� �Լ��� �����Ͽ�, �Ʒ� �����鿡 ���� ������ ���� �����մϴ�.

        // �׽�Ʈ �ʱ�ȭ
        PlayerHp = 100f;
        BossHp = 100f;

        // isDialogue = true; // ��簡 ��µ��� �����մϴ�.

        PlayerMaxHp = PlayerHp;
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;

        CurrentBullet = 60;
        MaxBullet = 60;

        BossName = "Overflow"; // ���� ������ ���� �̸� ����
        GimmickName = "Error404"; // ��� �̸� ����
        WeaponName = "Weapon1"; // ���� ���� ���� ���� �̸� ����
        
        
        InitWeaponInfo();
        // DialogueEvent(0);
    }

    

    private void FunctionTestUpdate()
    {
        // �׽�Ʈ ������Ʈ
        if (PlayerHp == PlayerMaxHp)
        {

        }
    }

    IEnumerator TestCoroutine()
    {
        // �׽�Ʈ �ڷ�ƾ
        // �÷��̾��� ü��, ������ ü��, ���̾�α��� ���� �� ����, �̴ϸ� �ϴ� ���� �� ����� �׽�Ʈ�մϴ�.

        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 10;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        NumOfEnemy++;
        yield return new WaitForSeconds(5f);
        isBoss = false;
        isGimmick = false;

        yield return null;
    }

    IEnumerator TestCoroutineBullet()
    {
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        CurrentBullet--;
        yield return new WaitForSeconds(0.2f);
        yield return null;
    }
    #endregion
}
