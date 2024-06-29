using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RemAction
{
    public static void Heal()
    {
        // ġ�� �ڵ� �ۼ�
        RemTestManager.instance.hp = Mathf.Min(RemTestManager.instance.hp + 20, 100); // 20��ŭ ü�� ȸ��, �ִ� ü�� 100 ����
        Debug.Log("Healed to: " + RemTestManager.instance.hp);
    }

    public static void DeployShield()
    {
        // �� ���� �ڵ� �ۼ�
        Debug.Log("Shield deployed!");
    }

    public static void Attack()
    {
        // ���� �ڵ� �ۼ�
        Debug.Log("Attacking the enemy!");
    }
}
