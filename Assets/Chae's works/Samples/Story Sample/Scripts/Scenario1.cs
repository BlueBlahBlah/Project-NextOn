using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    [SerializeField]
    private Scenario1UI scenario1UI;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ScenarioNumber = 0; // 시나리오 넘버 저장
        UIManager.instance.DialogueNumber = 10; // 다이얼로그 넘버 저장 (대사 시작지점)

        if (scenario1UI == null) scenario1UI = GameObject.Find("Scenario1UI").GetComponent<Scenario1UI>();

        StartCoroutine("StartScenario1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }

    IEnumerator StartScenario1()
    {
        // 키보드 사운드 출력
        yield return new WaitForSeconds(5f);

        PrintLongDialogue();
        yield return new WaitForSeconds(7f); // 대사 출력 방법을 수동으로 변경할 시, 코루틴 동작 변경 필요
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(0.2f);
        scenario1UI.SetLittleDark();
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        yield return new WaitForSeconds(3f);
        PrintLongDialogue();
        yield return null;
    }
}
