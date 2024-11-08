using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStageClear : MonoBehaviour
{
    [SerializeField]
    public StageClearPanel stageClearPanel;
    [SerializeField]
    public StageFailPanel failPanel;

    [SerializeField]
    private bool isClear;
    [SerializeField]
    private bool isFail;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isClear) Invoke("StageClear", 2.0f);
        if (isFail) Invoke("StageFail", 2.0f);
    }

    public void StageClear()
    {
        stageClearPanel.OpenPanel();
    }

    public void StageFail()
    {
        failPanel.OpenPanel();
    }
}
