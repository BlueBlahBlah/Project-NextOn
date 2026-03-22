using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 모든 총기류(Rifle, Shotgun, Sniper 등)에 공동으로 사용할 수 있는 데이터 클래스입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGunData", menuName = "Data/Weapon/GunData")]
    public class GunData : WeaponData
    {
        [Header("총기 성능")]
        public float fireRate;          // 연사 속도 (초 단위)
        public float bulletSpeed;       // 탄속
        public float reloadDuration;    // 재장전 시간 (초 단위)
        public AudioClip shootSound;    // 발사 사운드 에셋
        public bool isLoopingSound;     // 루핑 사운드 여부 (머신건, 화염방사기 등)

        [Header("탄약 설정")]
        public int maxMagazineSize;     // 한 탄창에 들어가는 최대 탄수
        public int totalMaxAmmo;        // 소지 가능한 최대 탄수 (예비 탄창 포함)
        public GameObject bulletPrefab; // 발사할 탄환 프리팹
    }
}
