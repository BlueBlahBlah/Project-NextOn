using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemTestManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static RemTestManager instance;

    // ���� ���� - �÷��̾��� ü��
    public int hp;
    public bool isNear;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ��Ÿ ���� �޼������ �߰��� �� ����
}