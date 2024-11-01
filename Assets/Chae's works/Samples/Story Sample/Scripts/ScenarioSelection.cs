using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSelection : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("PlayScenario", 5f);
    }

    public void PlayScenario()
    {
        int count = 0;

        for (int i = 0; i < StageClearManager.instance.stageClearStatus.Length; i++)
        {
            if (StageClearManager.instance.stageClearStatus[i] == true) count++;
        }

        Debug.Log(count);

        if (count == 0)
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
        else if (count > 0 && count < 4 && StageClearManager.instance.isSuccess == false)
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
        // UIManager.instance.DialogueNumber = ;
        PlayDialogue();
    }

    public void PlayScenarioFailure()
    {
        // UIManager.instance.DialogueNumber = ;
        PlayDialogue();
    }

    public void PlayScenarioEnding()
    {
        // UIManager.instance.DialogueNumber = ;
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        UIManager.instance.DialogueEventByNumber(UIManager.instance.longDialogue, UIManager.instance.DialogueNumber);
    }
}
