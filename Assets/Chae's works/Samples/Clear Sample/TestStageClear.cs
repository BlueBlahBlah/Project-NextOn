using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStageClear : MonoBehaviour
{
    [SerializeField]
    public StageClearPanel stageClearPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StageClear", 2.0f);
    }

    public void StageClear()
    {
        stageClearPanel.OpenPanel();
    }
}
