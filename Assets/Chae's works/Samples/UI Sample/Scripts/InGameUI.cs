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
    public int DialogueNumber;

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
        UpdateBossInfo(); // ���� ���� �� ���� info ������Ʈ
        UpdateGimmickInfo(); // ��� ���� �� ��� info ������Ʈ
        UpdateNumOfEnemy(); // ���� �� ������Ʈ
        UpdateDialogue(); // ���̾�α� ������Ʈ


        FunctionTestUpdate(); // �׽�Ʈ�� �ڵ�
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
        if (isBoss)
        {
            // ���� ���� ��
            if (!bossInfo.activeInHierarchy)
            {
                // ������ ���� ������, UI�� ������ ���� ���¿��� ȣ��
                // >> ���� Info �ʱ�ȭ
                // ������ �̸�, �̹��� �� ������Ʈ
                bossName.text = BossName;
                bossIcon.sprite = Resources.Load($"UI/Image/BossIcons/{BossName}", typeof(Sprite)) as Sprite;
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
                //gimmickName.text = GimmickName;
                //gimmickIcon.sprite = Resources.Load($"UI/Image/GimmickIcons/{GimmickName}", typeof(Sprite)) as Sprite;

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
        numOfEnemy.text = NumOfEnemy.ToString();
    }

    public void UpdateDialogue() // ��ȭ ��ȣ �Է°����� ����
    {
        if (isDialogue)
        {
            if (!dialogue.activeInHierarchy)
            {
                dialogue.SetActive(true);
            }
            string Name = data_Dialogue[DialogueNumber]["Character Name"].ToString();
            
            // �̹��� ���� << **csv�� ����� ĳ���� �̸��� �ѱ��ε�, ���ҽ����� Load�� �̸��� ����� ��.
            // �ذ� ���
            // ��ȭ �̹����� ����� ĳ���Ͱ� ���� ������ Switch������ �޾ƿ� Name ��.
            // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/{Name}", typeof(Sprite)) as Sprite;
            
            // �̸� ����
            dialogueName.text = Name;
            
            // ���� ����
            dialogueContent.text = data_Dialogue[DialogueNumber]["Contents"].ToString();
        }
        else
        {
            if (dialogue.activeInHierarchy) dialogue.SetActive(false);
        }
    }
    #endregion



    // Button
    #region
    public void ButtonPause()
    {
        // ���� �÷��� Pause ���

        // �ٽ��ϱ�, Setting, ���� �� ���� UI ȣ��
    }

    public void ButtonPlayerInfo()
    {
        // ���� �÷��� Pause ���

        // �÷��̾��� ���� ���� (����, ���ݷ�, ü�� �� ���� Ȯ��?) ���� ������ UI ȣ��
    }
    #endregion


    // Test
    #region
    private void FunctionTestStart()
    {
        // �׽�Ʈ �ʱ�ȭ
        PlayerHp = 100f;
        BossHp = 100f;

        isBoss = true;
        isGimmick = true;
        isDialogue = true;

        PlayerMaxHp = PlayerHp;
        BossMaxHp = BossHp;
        GimmickPercent = 0f;
        NumOfEnemy = 0;
        DialogueNumber = 0;

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
        DialogueNumber++;
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
        isBoss = false;
        isGimmick = false;
        NumOfEnemy++;
        yield return new WaitForSeconds(1f);

        yield return null;
    }
    #endregion
}
