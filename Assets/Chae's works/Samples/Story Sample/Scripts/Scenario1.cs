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
        // 싱글톤 클래스들 내의 정보 갱신
        if (SceneContainer.instance != null)
        {
            SceneContainer.instance.currentScene = "Scenario1 Scene";
            SceneContainer.instance.nextScene = "Selection Scene";
        }
        
        if (UIManager.instance != null)
        {
            UIManager.instance.ScenarioNumber = 0; // 시나리오 넘버 저장
            UIManager.instance.DialogueNumber = 0; // 다이얼로그 넘버 저장 (대사 시작지점)
        }
        
        // Scenario1UI 가져오기
        if (scenario1UI == null) scenario1UI = GameObject.Find("Scenario1UI").GetComponent<Scenario1UI>();

        StartCoroutine("StartScenario1");
    }

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue , UIManager.instance.DialogueNumber);
    }

    public void ChangeScene() { LoadingManager.ToLoadScene(); }

    // test
    public void TestCoroutine() { StartCoroutine("StartScenario1"); }


    // Coroutine
    #region
    IEnumerator StartScenario1()
    {
        // Scenario1의 전체적인 연출을 나타낼 코루틴입니다.
        // 긴 내용을 포함하고 있기 때문에 특징적인 연출마다 주석을 작성합니다.

        // 키보드 사운드 출력
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlayEffectSound("Keyboard", 1f);
        yield return new WaitForSeconds(4f);

        // 대사 출력1
        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        SoundManager.instance.PlayEffectSound("Keyboard", 1f);
        yield return new WaitForSeconds(2f);

        // 깜빡임 연출
        scenario1UI.SetLittleDark();
        SoundManager.instance.PlayEffectSound("Blink1", 0.5f);
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        SoundManager.instance.PlayEffectSound("Blink2", 0.5f);
        yield return new WaitForSeconds(0.2f);
        scenario1UI.SetLittleDark();
        SoundManager.instance.PlayEffectSound("Blink1", 0.5f);
        yield return new WaitForSeconds(0.1f);
        scenario1UI.SetLight();
        SoundManager.instance.PlayEffectSound("Blink2", 0.5f);
        yield return new WaitForSeconds(2f);

        // 대사 출력2 및 캐릭터 도트 정지
        PrintLongDialogue();
        scenario1UI.StopCharacter();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        yield return new WaitForSeconds(1f);
        scenario1UI.SetLittleDark();
        SoundManager.instance.PlayEffectSound("Blink1", 0.5f);
        yield return new WaitForSeconds(0.5f);
        scenario1UI.SetLight();
        SoundManager.instance.PlayEffectSound("Blink2", 0.5f);
        yield return new WaitForSeconds(1f);
        scenario1UI.SetAlmostDark();
        SoundManager.instance.PlayEffectSound("Blink1", 0.5f);
        SoundManager.instance.PlayEffectSound("Turn Off", 0.5f);
        yield return new WaitForSeconds(2.5f);

        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");

        SoundManager.instance.PlayEffectSound("Click", 0.5f);
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlayEffectSound("Click Double", 0.5f);

        yield return new WaitForSeconds(2f);
        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");

        yield return new WaitForSeconds(1f);
        scenario1UI.ElectricShock();
    }

    IEnumerator Scenario1Sound()
    {
        yield break;
    }

    IEnumerator RunLoopUntilDone()
    {
        while (true)
        {
            if (!UIManager.instance.isDone) 
            {
                yield return null;
            }
                
            else
            {
                yield break;
            }
        }
    }
    #endregion
}
