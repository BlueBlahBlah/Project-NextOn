using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    // �� �������� Ŭ���� ����
    [SerializeField] private bool stage1Clear = false;
    [SerializeField] private bool stage2Clear = false;
    [SerializeField] private bool stage3Clear = false;
    [SerializeField] private bool stage4Clear = false;

    // �� �������� ���� ������Ʈ
    private GameManager stage1Object;
    private GameManager stage2Object;
    private GameManager stage3Object;
    private GameManager stage4Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Scene�� ���� �Ǿ��� ��, ������Ʈ���� Ŭ���� ���θ� üũ. ������Ʈ���� �����Ѵٸ� �ı� ����
    }
}
