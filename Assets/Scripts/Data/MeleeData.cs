using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 근접 무기(Sword, Axe 등)에 사용하는 데이터 클래스입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMeleeData", menuName = "Data/Weapon/MeleeData")]
    public class MeleeData : WeaponData
    {
        [Header("근접 공격 설정")]
        public GameObject skillPrefab;      // 무기 전용 스킬 프리팹
        public float skillCoolTime;         // 스킬 재사용 대기시간
        
        [Header("특수 효과 (선택사항)")]
        public int attackNumThreshold;      // 특정 타수마다 효과가 발생하는 경우 (예: SwordStatic)
    }
}
