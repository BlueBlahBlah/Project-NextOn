using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageFormula
{
    // Damage 적용 규칙
    // 1. 일반 공격의 Damage는 플레이어의 Damage 스탯에 기반하여 연산
    // 2. 스킬의 Damage는 플레이어의 SkillDamage 스탯에 기반하여 연산
    // 3. 치명타의 경우, 위 연산이 끝난 뒤 최종적으로 Crit Damage 스탯에 기반하여 연산
    // 4. 연산이 끝난 Damage 들의 정보는 PlayerStatManager 에 저장되고, Damage를 입히는 개체들이
    //  이 정보를 가져가서 각각의 계수를 적용하여 사용될 예정

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
