using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 洹쇱젒 臾닿린(Sword, Axe ?????ъ슜?섎뒗 ?곗씠???대옒?ㅼ엯?덈떎.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMeleeData", menuName = "Data/Weapon/MeleeData")]
    public class MeleeData : WeaponData
    {
        [Header("洹쇱젒 怨듦꺽 ?ㅼ젙")]
        public GameObject skillPrefab;      // 臾닿린 ?꾩슜 ?ㅽ궗 ?꾨━??        public float skillCoolTime;         // ?ㅽ궗 ?ъ궗???湲곗떆媛?        
        [Header("?뱀닔 ?④낵 (?좏깮?ы빆)")]
        public int attackNumThreshold;      // ?뱀젙 ??섎쭏???④낵媛 諛쒖깮?섎뒗 寃쎌슦 (?? SwordStatic)
    }
}
