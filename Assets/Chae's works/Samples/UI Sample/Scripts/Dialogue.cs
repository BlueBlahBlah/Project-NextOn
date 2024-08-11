using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // 다이얼로그에 필요한 정보들을 저장하고, 사전에 저장된 csv 파일을 호출하는 스크립트입니다.
    // 'LongDialogue', 'ShortDialogue' UI 오브젝트에 포함되는 스크립트입니다. 

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


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 필요한 함수
    #region
    public void PrintDialogueByNumber(int _DialogueNumber) 
    {
        // 출력할 대화 번호(csv 파일 기준)를 입력값으로 받는 대화 출력 함수
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
        // 키워드를 입력값으로 받는 대화 출력 함수

    }

    private void Init()
    {
        // 다이얼로그 타입 (길이) 에 맞춰 적합한 csv 호출
        if (dialogueType == "Short") data_Dialogue = CSVReader.Read("Data (.csv)/Announce"); // 인게임 내의 안내 메세지
        if (dialogueType == "Long") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue"); // 시나리오용 대사

        dialogue.SetActive(false);
    }
    
    private void ChangeImage(string Name)
    {
        // 전달받은 이름에 따라 사용 이미지 초기화
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
    #endregion

    // 버튼 작동 함수
    #region
    // 오토 설정
    public void Auto() { UIManager.instance.isAuto = !UIManager.instance.isAuto; }
    // 2배 설정
    public void PrintSpeed() { UIManager.instance.printSpeed = UIManager.instance.printSpeed == 1 ? 2 : 1; }

    public void SkipAndNext() // 대화 스킵, 넘기기
    {
        if (!UIManager.instance.isCompletelyPrinted)
        {
            // 온전히 출력되지 않은 상태. (Skip)
        }
        else
        {
            // 모든 내용이 출력된 상태 (Next)
            UIManager.instance.doNext = true;
        }
    }
    #endregion

    // 필요한 코루틴
    #region
    IEnumerator TypeText()
    {
        UIManager.instance.isCompletelyPrinted = false;
        // 출력 과정
        // 문자열을 차례대로 입력하는 코루틴
        for (int i = 0; i <= data_Dialogue[DialogueNumber]["Contents"].ToString().Length; i++)
        {
            ;
            dialogueContent.text = data_Dialogue[DialogueNumber]["Contents"].ToString().Substring(0, i);
            yield return new WaitForSeconds(typingSpeed / UIManager.instance.printSpeed);
        }

        UIManager.instance.isCompletelyPrinted = true;
        // 출력이 다 된 후

        if (!UIManager.instance.isAuto)
        {
            // 수동 넘기기
            // RunLoop 라는 무한루프 코루틴 시작
            // Dialogue UI에서 Next를 실행하면 루프가 종료되며 다음 넘어감
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
