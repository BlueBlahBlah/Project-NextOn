using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ScenarioNumber = 0; // 시나리오 넘버 저장
        UIManager.instance.DialogueNumber = 10; // 다이얼로그 넘버 저장 (대사 시작지점)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }
}
