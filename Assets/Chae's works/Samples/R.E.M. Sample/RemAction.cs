using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RemAction
{
    public static void Heal()
    {
        // 치유 코드 작성
        RemTestManager.instance.hp = Mathf.Min(RemTestManager.instance.hp + 20, 100); // 20만큼 체력 회복, 최대 체력 100 제한
        Debug.Log("Healed to: " + RemTestManager.instance.hp);
    }

    public static void DeployShield()
    {
        // 방어막 전개 코드 작성
        Debug.Log("Shield deployed!");
    }

    public static void Attack()
    {
        // 공격 코드 작성
        Debug.Log("Attacking the enemy!");
    }
}
