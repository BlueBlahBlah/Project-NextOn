using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    // ������ �̸� ���� �ʿ��� ����
    [Header("Player Information")]
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private Image playerIcon; // �÷��̾��� ������. ���¿� ���� ���� ����
    [SerializeField]
    private TextMeshProUGUI playerHp; // �÷��̾��� ü�� �ؽ�Ʈ
    [SerializeField]
    private Image playerHpBar; // �÷��̾��� ü�� ��
    //[SerializeField]
    //private Image playerWeapon1; // �÷��̾��� ���� �̹���1
    //[SerializeField]
    //private Image playerWeapon2; // �÷��̾��� ���� �̹���2

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


    // ���� ������ ����
    private float PlayerHp; // �÷��̾� ���� ü�� ����
    private float PlayerMaxHp; // �÷��̾� �ִ� ü�� ����

    private bool isBoss; // ���� ���� ���� ����
    private float BossHp; // ���� ü�� ����
    private float BossMaxHp; // ���� �ִ� ü�� ����
    private string BossName; // ���� �̸� ����

    private bool isGimmick; // ��� ���� ���� ����
    private float GimmickPercent; // ��� ���൵ ����
    private string GimmickName; // ��� �̸� ����

    private int NumOfEnemy; // �ʵ� �� ���� �� ����

    private bool isDialogue; // ��ȭâ ǥ�� ����
    private List<Dictionary<string, object>> data_Dialogue; // csv ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        FunctionTestStart(); // �׽�Ʈ�� �ڵ�

        data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); // csv ���� ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo(); // �÷��̾� info �׻� ������Ʈ
        if (isBoss) UpdateBossInfo(); // ���� ���� �� ���� info ������Ʈ
        if (isGimmick) UpdateGimmickInfo(); // ��� ���� �� ��� info ������Ʈ
        UpdateNumOfEnemy(); // ���� �� ������Ʈ

        FunctionTestUpdate(); // �׽�Ʈ�� �ڵ�
    }

    private void FunctionTestStart() 
    {
        // �׽�Ʈ �ʱ�ȭ
        PlayerHp = 100f;
        BossHp = 100f;

        isBoss = true;
        isGimmick = true;

        PlayerMaxHp = PlayerHp;
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;

        BossName = "Overflow"; // ���� ������ ���� �̸� ����
        GimmickName = "Gimmick"; // ��� �̸� ����
    }
    
    private void FunctionTestUpdate()
    {
        // �׽�Ʈ ������Ʈ
        if (PlayerHp == PlayerMaxHp) StartCoroutine("TestCoroutine");
    }

    IEnumerator TestCoroutine()
    {
        // �׽�Ʈ �ڷ�ƾ
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);
        UpdateDialogue(0);
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f); 
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f); 
        PlayerHp--;
        BossHp -= 7;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);

        yield return null;
    }

    // Update
    #region
    public void UpdatePlayerInfo() // �÷��̾� ���� ����
    {
        // ü�� ����
        playerHp.text = PlayerHp.ToString() + " / " + PlayerMaxHp.ToString(); // ü�� ����
        playerHpBar.fillAmount = PlayerHp / PlayerMaxHp; // ü�¹� �̹��� ����
    }

    public void UpdateBossInfo() // ���� ���� ����
    {

        if (!bossInfo.activeInHierarchy)
        {
            // ������ ���� ������, UI�� ������ ���� ���¿��� ȣ��
            // >> ���� Info �ʱ�ȭ
            // ������ �̸�, �̹��� �� ������Ʈ
            bossName.text = BossName;
            bossIcon.sprite = Resources.Load($"UI/Image/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
            bossInfo.SetActive(true);// UI Ȱ��ȭ
        }
        bossHp.text = (Mathf.Round(BossHp / BossMaxHp * 100)).ToString() + "%"; // ü�� ����
        bossHpBar.fillAmount = BossHp / BossMaxHp; // ü�¹� �̹��� ����

        if (!isBoss)
        {
            // ���� óġ ��, �ʵ� ���� ������ �������� ���� �� 
            // >> ���� Info ��Ȱ��ȭ
            // ***** Update ���� !isBoss �� �� �Լ��� ���� �Ұ����ϹǷ� ���� �ʿ�
            bossInfo.SetActive(false);
        }
    }

    public void UpdateGimmickInfo() // ��� ���� ����
    {
        // UI Ȱ��ȭ
        if (!gimmickInfo.activeInHierarchy)
        {
            // ����� ���� ������, UI�� ������ ���� ���¿��� ȣ��
            // >> ��� Info �ʱ�ȭ
            // ����� �̸�, �̹��� �� ������Ʈ
            //gimmickName.text = GimmickName;
            //gimmickIcon.sprite = Resources.Load($"UI/Image/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

            // ��� ȣ�� �� 0%�� �����Ѵٰ� ����
            gimmickPercent.text = "0%";
            gimmickProgressBar.fillAmount = 0f;

            gimmickInfo.SetActive(true);
        }

        // ����� ���, ����� Ÿ�� (�ð� ��� / Ƚ�� ���)�� ���� �ٸ��� �ڵ� �ۼ�
    }

    public void UpdateNumOfEnemy()
    {
        // ���Ͱ� ����, Ȥ�� ��� �� �̴ϸ� �ϴ��� ���� �� ����
        numOfEnemy.text = NumOfEnemy.ToString();
    }
    #endregion
    
    public void UpdateDialogue(int number) // ��ȭ ��ȣ �Է°����� ����
    {
        if (!dialogue.activeInHierarchy)
        {
            dialogue.SetActive(true);
        }
        string Name = data_Dialogue[number]["Character Name"].ToString();
        Debug.Log(Name);
        // �̹��� ����
        // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/{Name}", typeof(Sprite)) as Sprite;
        // �̸� ����
        dialogueName.text = Name;
        // ���� ����
        dialogueContent.text = data_Dialogue[number]["Contents"].ToString();
    }
    // Button
    #region
    public void ButtonPause()
    {

    }

    public void ButtonPlayerInfo()
    {

    }
    #endregion

   
}
