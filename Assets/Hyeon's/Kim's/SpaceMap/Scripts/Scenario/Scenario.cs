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
    // �̱��� ����
    #region
    public static Scenario instance = null;

    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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
