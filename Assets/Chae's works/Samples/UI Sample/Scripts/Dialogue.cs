using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [Header("Connect")]
    [SerializeField]
    private GameObject dialogue; // 대화창 오브젝트
    [SerializeField]
    private Image dialogueImage; // 대화 캐릭터 이미지
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 대화 캐릭터 이름
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 대화 내용
    [SerializeField]
    private Button printSpeed;
    [SerializeField]
    private Button auto;

    [Header("Option")]
    [SerializeField]
    private string dialogueType; // 다이얼로그 타입 (길이)
    [SerializeField]
    private float typingSpeed = 0.03f; // 대화 출력 속도

    [Header("Data")]
    public bool isDialogue; // 대화창 표시 여부
    private List<Dictionary<string, object>> data_Dialogue; // csv 파일 담을 변수
    public int DialogueNumber; // 출력할 대화의 번호
    private float dialogueTime; // 대화의 길이 (WaitforSeconds 입력 변수)
    private int dialogueIsContinuous; // 이어지는 대화가 있는지 확인할 변수

    private Coroutine typingCoroutine; // 현재 실행 중인 코루틴을 제어할 변수

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    public void PrintDialogueByNumber(int _DialogueNumber)
    {
        DialogueNumber = _DialogueNumber;

        if (isDialogue)
        {
            UIManager.instance.isDone = false;

            if (!dialogue.activeInHierarchy)
            {
                dialogue.SetActive(true);
            }
            string Name = data_Dialogue[DialogueNumber]["Character Name"].ToString();

            // 이름 변경
            dialogueName.text = Name;

            dialogueTime = float.Parse(data_Dialogue[DialogueNumber]["Time"].ToString());
            dialogueIsContinuous = int.Parse(data_Dialogue[DialogueNumber]["Continuous"].ToString());

            // 이미지 변경
            if (dialogueType == "Long") ChangeImage(Name);

            Debug.Log($"Time : {dialogueTime}, Continuous : {dialogueIsContinuous}");
            // 내용 변경
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeText());
        }
        else
        {
            UIManager.instance.isDone = true;
            if (dialogue.activeInHierarchy) dialogue.SetActive(false);
        }
    }

    public void PrintDialogueByKeyword(string _keyword)
    {
        // 키워드를 입력값으로 받는 대화 출력 함수
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
            case "데빈":
                dialogueImage.sprite = Resources.Load($"UI/Image/Characters/Devin/Devin", typeof(Sprite)) as Sprite;
                dialogueImage.color = new Color(255, 255, 255, 255);
                break;
            case "작업관리자":
                dialogueImage.sprite = null;
                dialogueImage.color = new Color(0, 0, 0, 0);
                break;
            default:
                dialogueImage.sprite = null;
                dialogueImage.color = new Color(0, 0, 0, 0);
                break;
        }
    }

    public void Auto()
    {
        UIManager.instance.isAuto = !UIManager.instance.isAuto;

        // 버튼 색상 변경
        ColorBlock colors = auto.colors;
        colors.normalColor = UIManager.instance.isAuto ? Color.green : Color.white;
        auto.colors = colors;
    }

    public void PrintSpeed()
    {
        UIManager.instance.printSpeed = UIManager.instance.printSpeed == 1 ? 2 : 1;

        // 버튼 색상 변경
        ColorBlock colors = printSpeed.colors;
        colors.normalColor = UIManager.instance.printSpeed == 2 ? Color.green : Color.white;
        printSpeed.colors = colors;
    }

    public void SkipAndNext()
    {
        if (!UIManager.instance.isCompletelyPrinted)
        {
            // 온전히 출력되지 않은 상태. (Skip)
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 현재 실행 중인 타이핑 코루틴 중지
                StartCoroutine(FinishTyping()); // 남은 텍스트를 모두 출력하는 코루틴 시작
            }
        }
        else
        {
            // 모든 내용을 자동으로 출력된 상태 (Next)
            UIManager.instance.doNext = true;
        }
    }

    IEnumerator TypeText()
    {
        UIManager.instance.isCompletelyPrinted = false;

        string content = data_Dialogue[DialogueNumber]["Contents"].ToString();
        for (int i = 0; i <= content.Length; i++)
        {
            dialogueContent.text = content.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed / UIManager.instance.printSpeed);
        }

        UIManager.instance.isCompletelyPrinted = true;

        if (!UIManager.instance.isAuto)
        {
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
    }

    IEnumerator FinishTyping()
    {
        string content = data_Dialogue[DialogueNumber]["Contents"].ToString();
        dialogueContent.text = content; // 모든 내용을 한 번에 출력
        UIManager.instance.isCompletelyPrinted = true;

        // 타이핑이 완료된 후에는 다음 동작으로 바로 넘어가도록 설정
        if (UIManager.instance.isAuto)
        {
            yield return new WaitForSeconds(dialogueTime / UIManager.instance.printSpeed);
        }
        else
        {
            yield return StartCoroutine("RunLoop");
        }

        // 대화가 이어지면 다음 대화로 진행
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

        yield break;
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
}