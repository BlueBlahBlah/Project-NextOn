using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioEnding : MonoBehaviour
{
    [SerializeField]
    ScenarioEndingUI scenarioEndingUI;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.DialogueNumber = 430;
        Invoke("DoScenarioEnding", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoScenarioEnding()
    {
        StartCoroutine(StartScenarioEnding());
    }

    IEnumerator StartScenarioEnding()
    {
        SoundManager.instance.PlayEffectSound("Wake up", 1f);
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlayEffectSound("Sit down", 1f);
        yield return new WaitForSeconds(3f);
        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlayEffectSound("Click Double", 0.5f);
        yield return new WaitForSeconds(1f);
        PrintLongDialogue();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine("RunLoopUntilDone");
        yield return new WaitForSeconds(1f);
        scenarioEndingUI.StartLerpAlpha();
        yield return new WaitForSeconds(3f);
        SoundManager.instance.PlayMusic("Tuesday");
        scenarioEndingUI.ActivateEndText();
        yield return new WaitForSeconds(1f);
        scenarioEndingUI.ActivateButton();
        yield return null;
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

    public void PrintLongDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }
}
