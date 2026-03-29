using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// ë¬´ê¸°ê°€ ?„ë‹Œ ?¹ìˆ˜???¤í‚¬ ?„ì´??Turret, Helicopter, Bomb ?????¬ìš©?˜ëŠ” ?°ì´???´ë˜?¤ì…?ˆë‹¤.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSkillData", menuName = "Data/SkillData")]
    public class SkillData : BaseData
    {
        [Header("?¤í‚¬ ?ì„¸ ?¤ì •")]
        public float duration;      // ?¤í‚¬ ì§€???œê°„
        public float cooldown;      // ?¤í‚¬ ì¿¨í???        
        [Header("?¨ê³¼ ê´€??)]
        public float effectRadius;  // ?í–¥ ë²”ìœ„
        public int effectValue;     // ?°ë?ì§€???Œë³µ?????˜ì¹˜
    }
}
