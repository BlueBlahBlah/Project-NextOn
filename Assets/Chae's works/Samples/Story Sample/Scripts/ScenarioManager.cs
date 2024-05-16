using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    // ** 싱글톤으로 선언된 시나리오 매니저에서 코루틴 객체를 생성해서
    // 각 시나리오에 입장할 때, 코루틴 객체에 시나리오에 맞는 코루틴을 대입하고
    // 다이럴로그, UI 등에서 Start, Stop 코루틴을 사용하는 방법?

    [Header("Scenario")]
    [SerializeField]
    private Scenario1 scenario1;

    public Coroutine scenario_Coroutine = null;

    // 싱글톤 선언
    #region
    public static ScenarioManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
