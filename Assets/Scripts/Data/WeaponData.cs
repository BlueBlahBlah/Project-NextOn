using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 무기(근접, 원거리) 데이터의 기본 클래스입니다.
    /// </summary>
    public abstract class WeaponData : BaseData
    {
        [Header("무기 정보")]
        public PlayerManager.WeaponType weaponType;    // closeType, longType 등
        public int baseDamage;                         // 기본 공격력
    }
}
