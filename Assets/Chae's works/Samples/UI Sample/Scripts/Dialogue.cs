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
    private GameObject dialogue; // 다이얼로그 오브젝트
    [SerializeField]
    private GameObject imageDevin; // 데빈의 이미지 오브젝트
    [SerializeField]
    private GameObject imageREM; // REM의 이미지 오브젝트
    [SerializeField]
    private Image dialogueImage; // 
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 대사 주체
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 대사 내용
    [SerializeField]
    private Button printSpeed;
    [SerializeField]
    private Button auto;

    [Header("Option")]
    [SerializeField]
    private string dialogueType; // 다이얼로그 타입 (현재 사용 x)
    [SerializeField]
    private float typingSpeed = 0.03f; // 타이핑 속도

    [Header("Data")]
    public bool isDialogue; // 현재 다이얼로그가 출력 가능한지 판단
    private List<Dictionary<string, object>> data_Dialogue; // csv 파일로부터 데이터를 불러와 저장하는 자료구조
    public int DialogueNumber; // 현재 다이얼로그 넘버
    private float dialogueTime; // Auto 모드에서 다이얼로그 출력 후 대기 시간
    private int dialogueIsContinuous; // 이어지는 대사가 있는지 판단하는 변수 (1 : 있음, 0 : 없음)

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

        ColorBlock colorsAuto = auto.colors;
        colorsAuto.normalColor = UIManager.instance.isAuto ? Color.gray : Color.white;
        auto.colors = colorsAuto;

        ColorBlock colorsPrintSpeed = printSpeed.colors;
        colorsPrintSpeed.normalColor = UIManager.instance.printSpeed == 2 ? Color.gray : Color.white;
        printSpeed.colors = colorsPrintSpeed;

        dialogue.SetActive(false);
    }

    private void ChangeImage(string Name)
    {
        switch (Name)
        {
            case "데빈":
                if (!imageDevin.activeInHierarchy)
                {
                    imageDevin.SetActive(true);  
                }
                imageDevin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                imageREM.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);

                // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/Devin/Devin", typeof(Sprite)) as Sprite;
                // dialogueImage.color = new Color(255, 255, 255, 255);
                break;
            case "R.E.M":
                if (!imageREM.activeInHierarchy)
                {
                    imageREM.SetActive(true);
                }
                imageDevin.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                imageREM.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                // dialogueImage.sprite = Resources.Load($"UI/Image/Characters/REM/REM", typeof(Sprite)) as Sprite;
                // dialogueImage.color = new Color(255, 255, 255, 255);
                break;

            case "작업 관리자":
                imageDevin.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                imageREM.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                // dialogueImage.sprite = null;
                // dialogueImage.color = new Color(0, 0, 0, 0);
                break;
            default:
                imageDevin.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                imageREM.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                // dialogueImage.sprite = null;
                // dialogueImage.color = new Color(0, 0, 0, 0);
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