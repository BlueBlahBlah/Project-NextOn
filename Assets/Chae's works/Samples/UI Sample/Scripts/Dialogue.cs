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
    private GameObject dialogue; // 占쏙옙화창 占쏙옙占쏙옙占쏙옙트
    [SerializeField]
    private Image dialogueImage; // 占쏙옙화 캐占쏙옙占쏙옙 占싱뱄옙占쏙옙
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 占쏙옙화 캐占쏙옙占쏙옙 占싱몌옙
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 占쏙옙화 占쏙옙占쏙옙
    [SerializeField]
    private Button printSpeed;
    [SerializeField]
    private Button auto;

    [Header("Option")]
    [SerializeField]
    private string dialogueType; // 占쏙옙占싱억옙慣占 타占쏙옙 (占쏙옙占쏙옙)
    [SerializeField]
    private float typingSpeed = 0.03f; // 占쏙옙화 占쏙옙占 占쌈듸옙

    [Header("Data")]
    public bool isDialogue; // 占쏙옙화창 표占쏙옙 占쏙옙占쏙옙
    private List<Dictionary<string, object>> data_Dialogue; // csv 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙
    public int DialogueNumber; // 占쏙옙占쏙옙占 占쏙옙화占쏙옙 占쏙옙호
    private float dialogueTime; // 占쏙옙화占쏙옙 占쏙옙占쏙옙 (WaitforSeconds 占쌉뤄옙 占쏙옙占쏙옙)
    private int dialogueIsContinuous; // 占싱억옙占쏙옙占쏙옙 占쏙옙화占쏙옙 占쌍댐옙占쏙옙 확占쏙옙占쏙옙 占쏙옙占쏙옙

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

            // 다이얼로그 캐릭터 이름 변경
            dialogueName.text = Name;

            dialogueTime = float.Parse(data_Dialogue[DialogueNumber]["Time"].ToString());
            dialogueIsContinuous = int.Parse(data_Dialogue[DialogueNumber]["Continuous"].ToString());

            // 다이얼로그 스프라이트 변경
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
            case "작업 관리자":
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
        colors.normalColor = UIManager.instance.isAuto ? Color.gray : Color.white;
        auto.colors = colors;
    }

    public void PrintSpeed()
    {
        UIManager.instance.printSpeed = UIManager.instance.printSpeed == 1 ? 2 : 1;

        // 버튼 색상 변경
        ColorBlock colors = printSpeed.colors;
        colors.normalColor = UIManager.instance.printSpeed == 2 ? Color.gray : Color.white;
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

        // 텍스트 출력 시작 시 효과음 재생
        SoundManager.instance.PlayEffectSound("UISound/TypeSound", 0.3f);

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

        // 타이핑이 완료되었거나 스킵 시 효과음 중지
        SoundManager.instance.StopEffects();

        if (UIManager.instance.isAuto)
        {
            yield return new WaitForSeconds(dialogueTime / UIManager.instance.printSpeed);
        }
        else
        {
            yield return StartCoroutine("RunLoop");
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