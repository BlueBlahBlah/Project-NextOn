using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoEventBtn : MonoBehaviour
{
    [SerializeField] private Button eventBtn; 
    void Start()
    {
        eventBtn.onClick.AddListener(CompilerErrorClear);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CompilerErrorClear()
    {
        GameObject.Find("StageManager").GetComponent<StageManager>().Area3 = true;
        GameObject.Find("StageManager").GetComponent<StageManager>().OnWave3Direction();
        Debug.LogError("컴파일러 해결");
        Debug.LogError("Area3 true");
    }
    
}
