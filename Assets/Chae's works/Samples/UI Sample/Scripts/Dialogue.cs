using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // ���̾�α׿� �ʿ��� �������� �����ϰ�, ������ ����� csv ������ ȣ���ϴ� ��ũ��Ʈ�Դϴ�.
    // 'LongDialogue', 'ShortDialogue' UI ������Ʈ�� ���ԵǴ� ��ũ��Ʈ�Դϴ�. 

    [Header("Dialogue")]
    [Header("Connect")]
    [SerializeField]
    private GameObject dialogue; // ��ȭâ ������Ʈ
    [SerializeField]
    private Image dialogueImage; // ��ȭ ĳ���� �̹���
    [SerializeField]
    private TextMeshProUGUI dialogueName; // ��ȭ ĳ���� �̸�
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // ��ȭ ����
    [SerializeField]
    private Button printSpeed;
    [SerializeField]
    private Button auto;

    [Header("Option")]
    [SerializeField]
    private string dialogueType; // ���̾�α� Ÿ�� (����)
    [SerializeField]
    private float typingSpeed = 0.03f; // ��ȭ ��� �ӵ�

    [Header("Data")]
    public bool isDialogue; // ��ȭâ ǥ�� ����
    private List<Dictionary<string, object>> data_Dialogue; // csv ���� ���� ����
    public int DialogueNumber; // ����� ��ȭ�� ��ȣ
    private float dialogueTime; // ��ȭ�� ���� (WaitforSeconds �Է� ����)
    private int dialogueIsContinuous; // �̾����� ��ȭ�� �ִ��� Ȯ���� ����


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �ʿ��� �Լ�
    #region
    public void PrintDialogueByNumber(int _DialogueNumber) 
    {
        // ����� ��ȭ ��ȣ(csv ���� ����)�� �Է°����� �޴� ��ȭ ��� �Լ�
        DialogueNumber = _DialogueNumber;

        if (isDialogue)
        {
            UIManager.instance.isDone = false;

            if (!dialogue.activeInHierarchy)
            {
                dialogue.SetActive(true);
            }
            string Name = data_Dialogue[DialogueNumber]["Character Name"].ToString();

            // �̸� ����
            dialogueName.text = Name;

            dialogueTime = float.Parse(data_Dialogue[DialogueNumber]["Time"].ToString());
            dialogueIsContinuous = int.Parse(data_Dialogue[DialogueNumber]["Continuous"].ToString());

            // �̹��� ����
            if (dialogueType == "Long") ChangeImage(Name);

            Debug.Log($"Time : {dialogueTime}, Continuous : {dialogueIsContinuous}");
            // ���� ����
            StartCoroutine("TypeText");
        }
        else
        {
            UIManager.instance.isDone = true;
            if (dialogue.activeInHierarchy) dialogue.SetActive(false);
        }
    }

    public void PrintDialogueByKeyword(string _keyword)
    {
        // Ű���带 �Է°����� �޴� ��ȭ ��� �Լ�

    }

    private void Init()
    {
        if (dialogueType == "Short") data_Dialogue = CSVReader.Read("Data (.csv)/Announce"); 
        if (dialogueType == "Long") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); 

        dialogue.SetActive(false);
    }
    
    private void ChangeImage(string Name)
    {
        switch (Name)
        {
            case "����":
                dialogueImage.sprite = Resources.Load($"UI/Image/Characters/Devin/Devin", typeof(Sprite)) as Sprite;
                dialogueImage.color = new Color(255, 255, 255, 255);
                break;
            case "�۾�������":
                dialogueImage.sprite = null;
                dialogueImage.color = new Color(0, 0, 0, 0);
                break;
            default:
                dialogueImage.sprite = null;
                dialogueImage.color = new Color(0, 0, 0, 0);
                break;
        }
        
    }
    #endregion

    // ��ư �۵� �Լ�
    #region
    // ���� ����
    public void Auto() { UIManager.instance.isAuto = !UIManager.instance.isAuto; }
    // 2�� ����
    public void PrintSpeed() { UIManager.instance.printSpeed = UIManager.instance.printSpeed == 1 ? 2 : 1; }

    public void SkipAndNext() // ��ȭ ��ŵ, �ѱ��
    {
        if (!UIManager.instance.isCompletelyPrinted)
        {
            // ������ ��µ��� ���� ����. (Skip)
        }
        else
        {
            // ��� ������ ��µ� ���� (Next)
            UIManager.instance.doNext = true;
        }
    }
    #endregion

    // �ʿ��� �ڷ�ƾ
    #region
    IEnumerator TypeText()
    {
        UIManager.instance.isCompletelyPrinted = false;
        // ��� ����
        // ���ڿ��� ���ʴ�� �Է��ϴ� �ڷ�ƾ
        for (int i = 0; i <= data_Dialogue[DialogueNumber]["Contents"].ToString().Length; i++)
        {
            ;
            dialogueContent.text = data_Dialogue[DialogueNumber]["Contents"].ToString().Substring(0, i);
            yield return new WaitForSeconds(typingSpeed / UIManager.instance.printSpeed);
        }

        UIManager.instance.isCompletelyPrinted = true;
        // ����� �� �� ��

        if (!UIManager.instance.isAuto)
        {
            // ���� �ѱ��
            // RunLoop ��� ���ѷ��� �ڷ�ƾ ����
            // Dialogue UI���� Next�� �����ϸ� ������ ����Ǹ� ���� �Ѿ
            yield return StartCoroutine("RunLoop");
        }
        else 
        {
            yield return new WaitForSeconds(dialogueTime / UIManager.instance.printSpeed);
        }


        if (dialogueIsContinuous == 1)
        {
            DialogueNumber++;
            PrintDialogueByNumber(DialogueNumber);
        }
        else if (dialogueIsContinuous == 0)
        {
            isDialogue = false;
            PrintDialogueByNumber(DialogueNumber);
        }
        UIManager.instance.DialogueNumber++;

        yield return null;
    }

    IEnumerator RunLoop()
    {
        while (true)
        {
            if (!UIManager.instance.doNext)
            {
                if (UIManager.instance.isAuto)
                {
                    yield return new WaitForSeconds(dialogueTime / UIManager.instance.printSpeed);
                }
                yield return null;
            }
            else
            {
                UIManager.instance.doNext = false;
                yield break;
            }
        }
    }
    #endregion
}
