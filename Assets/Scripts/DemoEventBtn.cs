using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoEventBtn : MonoBehaviour
{
    [SerializeField] private Button eventBtn;
    [SerializeField] private GameObject peiz3Gauge;
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
        peiz3Gauge.SetActive(true);
        peiz3Gauge.GetComponent<Peiz3Gauge>().StartPeiz3Gauge();
        GameObject.Find("StageManager").GetComponent<StageManager>().Peiz3MonsterSpawn();
        GameObject.Find("StageManager").GetComponent<StageManager>().StartPeiz3Pannel();
        Debug.LogError("컴파일러 해결");
        
    }
    
}
