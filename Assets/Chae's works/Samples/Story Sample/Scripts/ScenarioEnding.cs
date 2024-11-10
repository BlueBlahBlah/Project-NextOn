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
        Invoke("DoScenarioEnding", 2f);
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
        yield return null;
    }
}
