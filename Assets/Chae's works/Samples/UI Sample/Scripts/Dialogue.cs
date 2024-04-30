using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    private string dialogueType; // 다이얼로그 타입 (길이)
    [SerializeField]
    private GameObject dialogue; // 대화창 오브젝트
    [SerializeField]
    private Image dialogueImage; // 대화 캐릭터 이미지
    [SerializeField]
    private TextMeshProUGUI dialogueName; // 대화 캐릭터 이름
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // 대화 내용
    [SerializeField]
    private float typingSpeed = 0.05f; // 대화 출력 속도

    private List<Dictionary<string, object>> data_Dialogue;


    // Start is called before the first frame update
    void Start()
    {
        // 다이얼로그 타입 (길이) 에 맞춰 적합한 다이얼로그 csv 호출
        if (dialogueType == "Short") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue");
        else if (dialogueType == "Long") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
