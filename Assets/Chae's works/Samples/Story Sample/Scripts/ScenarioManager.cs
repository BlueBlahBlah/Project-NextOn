using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    // ** �̱������� ����� �ó����� �Ŵ������� �ڷ�ƾ ��ü�� �����ؼ�
    // �� �ó������� ������ ��, �ڷ�ƾ ��ü�� �ó������� �´� �ڷ�ƾ�� �����ϰ�
    // ���̷��α�, UI ��� Start, Stop �ڷ�ƾ�� ����ϴ� ���?

    [Header("Scenario")]
    [SerializeField]
    private Scenario1 scenario1;

    public Coroutine scenario_Coroutine = null;

    // �̱��� ����
    #region
    public static ScenarioManager instance = null;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
