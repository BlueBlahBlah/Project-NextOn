using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 무기가 아닌 특수한 스킬 아이템(Turret, Helicopter, Bomb 등)에 사용하는 데이터 클래스입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Data/SkillData")]
    public class SkillData : BaseData
    {
        [Header("스킬 상세 설정")]
        public float duration;      // 스킬 지속 시간
        public float cooldown;      // 스킬 쿨타임
        
        [Header("효과 관련")]
        public float effectRadius;  // 영향 범위
        public int effectValue;     // 데미지나 회복량 등 수치
    }
}
