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
        Debug.LogError("컴파일러 해결");
        Debug.LogError("Area3 true");
    }

    void StartBigMonster()      //3페이즈 대형 몬스터 등장
    {
        GameObject.Find("StageManager").GetComponent<StageManager>().Area3 = true;
        GameObject.Find("StageManager").GetComponent<StageManager>().OnWave3Direction();  //3페이즈 화살표 활성화 + 탈출 벽 비활성화
    }
    
}
