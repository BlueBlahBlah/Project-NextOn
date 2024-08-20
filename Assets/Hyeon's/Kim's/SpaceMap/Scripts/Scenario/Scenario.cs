using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public GameObject Scenario_1;
    public GameObject Scenario_2;
    public GameObject Scenario_3;
    public GameObject Scenario_4;

    public GameObject Dialogue;
    // 싱글톤 선언
    #region
    public static Scenario instance = null;

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
    void Start()
    {
        Scenario_1.SetActive(false);
        Scenario_2.SetActive(false);
        Scenario_3.SetActive(false);
        Scenario_4.SetActive(false);

        StartCoroutine("Scenario1Start");
    }

    void Update()
    {

    }
    IEnumerator Scenario1Start()
    {
        Scenario_1.SetActive(true);
        yield return null;
    }

    IEnumerator Scenario2Start()
    {
        StopCoroutine("Scenario1Start");
        Scenario_1.SetActive(false);
        Scenario_2.SetActive(true);
        yield return null;
    }

    IEnumerator Scenario3Start()
    {
        StopCoroutine("Scenario2Start");
        Scenario_2.SetActive(false);
        Scenario_3.SetActive(true);
        yield return null;
    }
}
