using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormula
{
    // Damage ���� ��Ģ
    // 1. �Ϲ� ������ Damage�� �÷��̾��� Damage ���ȿ� ����Ͽ� ����
    // 2. ��ų�� Damage�� �÷��̾��� SkillDamage ���ȿ� ����Ͽ� ����
    // 3. ġ��Ÿ�� ���, �� ������ ���� �� ���������� Crit Damage ���ȿ� ����Ͽ� ����
    // 4. ������ ���� Damage ���� ������ PlayerStatManager �� ����ǰ�, Damage�� ������ ��ü����
    //  �� ������ �������� ������ ����� �����Ͽ� ���� ����

    public static float UpdateDamage()
    {
        float _damage = PlayerStatManager.instance.damage;

        return _damage;
    }

    public static float UpdateCritDamage()
    {
        float _damage = UpdateDamage();
        _damage += (_damage * PlayerStatManager.instance.critDamage / 100);

        return _damage;
    }

    public static float UpdateSkillDamage()
    {
        float _damage = PlayerStatManager.instance.skillDamage;

        return _damage;
    }

    public static float UpdateSkillCritDamage()
    {
        float _damage = UpdateSkillDamage();
        _damage += (_damage * PlayerStatManager.instance.critDamage / 100);

        return _damage;
    }
}
