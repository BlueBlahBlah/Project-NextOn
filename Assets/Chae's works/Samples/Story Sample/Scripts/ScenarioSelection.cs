using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialPanel;
    [SerializeField]
    private GameObject EndObject;

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("PlayScenario", 4f);
    }

    private void Update()
    {
        if (UIManager.instance.DialogueNumber >= 56 || !TutorialPanel.activeInHierarchy)
        {
            TutorialPanel.SetActive(true);
        }
        if (UIManager.instance.DialogueNumber == 418)
        {
            EndObject.SetActive(true);
        }
    }

    public void PlayScenario()
    {
        int count = 0;

        for (int i = 0; i < StageClearManager.instance.stageClearStatus.Length; i++)
        {
            if (StageClearManager.instance.stageClearStatus[i] == true) count++;
        }

        Debug.Log(count);

        if (count == 0 && StageClearManager.instance.isSuccess == true)
        {
            PlayScenarioExplain();
        }
        else if (count == 4)
        {
            PlayScenarioEnding();
        }
        else if (count > 0 && count < 4 && StageClearManager.instance.isSuccess == true)
        {
            PlayScenarioSuccess();
        }
        else if (count >= 0 && count < 4 && StageClearManager.instance.isSuccess == false)
        {
            PlayScenarioFailure();
        }
    }

    public void PlayScenarioExplain()
    {
        UIManager.instance.DialogueNumber = 20;
        PlayDialogue();
    }

    public void PlayScenarioSuccess()
    {
        // 340, 350, 360 Áß ·£´ý Ãâ·Â
        UIManager.instance.DialogueNumber = Random.Range(34, 37) * 10;
        PlayDialogue();
    }

    public void PlayScenarioFailure()
    {
        // 370, 380, 390, 400 Áß ·£´ý Ãâ·Â
        UIManager.instance.DialogueNumber = Random.Range(37, 41) * 10;
        PlayDialogue();
    }

    public void PlayScenarioEnding()
    {
        UIManager.instance.DialogueNumber = 410;
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }
}
