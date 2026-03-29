using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// ê·¼ì ‘ ë¬´ê¸°(Sword, Axe ?????¬ìš©?˜ëŠ” ?°ì´???´ë˜?¤ì…?ˆë‹¤.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMeleeData", menuName = "Data/Weapon/MeleeData")]
    public class MeleeData : WeaponData
    {
        [Header("ê·¼ì ‘ ê³µê²© ?¤ì •")]
        public GameObject skillPrefab;      // ë¬´ê¸° ?„ìš© ?¤í‚¬ ?„ë¦¬??        public float skillCoolTime;         // ?¤í‚¬ ?¬ì‚¬???€ê¸°ì‹œê°?        
        [Header("?¹ìˆ˜ ?¨ê³¼ (? íƒ?¬í•­)")]
        public int attackNumThreshold;      // ?¹ì • ?€?˜ë§ˆ???¨ê³¼ê°€ ë°œìƒ?˜ëŠ” ê²½ìš° (?? SwordStatic)
    }
}
