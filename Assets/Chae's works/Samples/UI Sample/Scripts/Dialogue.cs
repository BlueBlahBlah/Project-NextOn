using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    private string dialogueType; // ���̾�α� Ÿ�� (����)
    [SerializeField]
    private GameObject dialogue; // ��ȭâ ������Ʈ
    [SerializeField]
    private Image dialogueImage; // ��ȭ ĳ���� �̹���
    [SerializeField]
    private TextMeshProUGUI dialogueName; // ��ȭ ĳ���� �̸�
    [SerializeField]
    private TextMeshProUGUI dialogueContent; // ��ȭ ����
    [SerializeField]
    private float typingSpeed = 0.05f; // ��ȭ ��� �ӵ�

    private List<Dictionary<string, object>> data_Dialogue;


    // Start is called before the first frame update
    void Start()
    {
        // ���̾�α� Ÿ�� (����) �� ���� ������ ���̾�α� csv ȣ��
        if (dialogueType == "Short") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue");
        else if (dialogueType == "Long") data_Dialogue = CSVReader.Read("Data (.csv)/Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
