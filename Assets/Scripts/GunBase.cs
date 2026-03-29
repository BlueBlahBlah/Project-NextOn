using UnityEngine;
using UnityEngine.UI;
using ProjectNextOn.Data;

public abstract class GunBase : WeaponBase
{
    // 부모 클래스의 weaponData를 GunData로 편하게 쓰기 위한 헬퍼 프로퍼티
    public GunData gunData => weaponData as GunData;

    [Header("실시간 상태 (데이터에서 초기화됨)")]
    public int bulletCount;
    public int maxBulletCount;
    public bool nowReloading;
    public bool IsFiring; // 외부(PlayerManager, AI 등)에서 사발 트리거

    [Header("머즐 위치 설정")]
    public Transform[] muzzles;

    protected virtual void Awake()
    {
        // 머즐이 비어있을 경우에만 자동 탐색
        if (muzzles == null || muzzles.Length == 0)
        {
            Transform muzzleParent = FindMuzzleRecursive(transform, "muzzle") ?? FindMuzzleRecursive(transform, "Muzzle");

            if (muzzleParent != null)
            {
                // 부모 밑에 실제 자식(각 구멍)들이 있다면 배열에 담기
                if (muzzleParent.childCount > 0)
                {
                    muzzles = new Transform[muzzleParent.childCount];
                    for (int i = 0; i < muzzleParent.childCount; i++)
                    {
                        muzzles[i] = muzzleParent.GetChild(i);
                    }
                }
                else
                {
                    // 자식이 없고 Muzzle 통짜 하나라면 그거 하나를 발사구로 지정
                    muzzles = new Transform[] { muzzleParent };
                }
            }
        }
    }

    // 이름으로 자식을 무기 모델 내부 끝까지 파고들며 찾는 재귀 함수
    private Transform FindMuzzleRecursive(Transform parent, string name)
    {
        if (parent.name.Contains(name)) return parent; // 이름에 Muzzle이 포함되어 있으면 반환
        
        foreach (Transform child in parent)
        {
            Transform result = FindMuzzleRecursive(child, name);
            if (result != null) return result;
        }
        return null;
    }

    protected virtual void Start()
    {
        nowReloading = false;
        timer = 0;
        InitializeFromData();
    }

    protected void InitializeFromData()
    {
        if (gunData != null)
        {
            bulletCount = gunData.maxMagazineSize;
            maxBulletCount = gunData.totalMaxAmmo;
            fireRate = gunData.fireRate;
        }
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        bool canFire = IsFiring && bulletCount > 0 && !nowReloading;

        // 자동 사격 처리
        if (canFire && timer >= fireRate)
        {
            OnFireButtonClick();
            timer = 0;
        }
        else if (IsFiring && bulletCount <= 0 && !nowReloading)
        {
            // 총알이 없는데 사격 시도 중이면 자동 재장전 요청
            RequestReload();
        }

        // 루핑 사운드 처리 (머신건, 화염방사기 등)
        if (gunData != null && gunData.isLoopingSound && gunData.shootSound != null)
        {
            if (canFire)
            {
                // 소리가 안 나고 있으면 재생 시작
                var source = PlayerSoundManager.Instance.GetAudioSource(gunData.shootSound);
                if (source != null && !source.isPlaying)
                {
                    source.loop = true;
                    source.Play();
                }
            }
            else
            {
                // 조건이 안 맞으면 중지
                PlayerSoundManager.Instance.StopWeaponSound(gunData.shootSound);
            }
        }
    }

    protected virtual void OnEnable()
    {
        // 이제 외부에서 IsFiring을 제어하므로 리스너가 필요 없음
    }

    protected virtual void OnDisable()
    {
        IsFiring = false; // 비활성화 시 사격 중지
        
        // 비활성화될 때 루핑 사운드 중지
        if (gunData != null && gunData.isLoopingSound)
        {
            PlayerSoundManager.Instance.StopWeaponSound(gunData.shootSound);
        }
    }

    public virtual void OnFireButtonClick()
    {
        if (bulletCount > 0)
        {
            Shoot();
            // 루핑 사운드가 아닐 때만 단발음 재생
            if (gunData != null && !gunData.isLoopingSound)
            {
                PlayShootSound();
            }
        }
        else
        {
            RequestReload();
        }
    }

    protected virtual void Shoot()
    {
        if (gunData == null || gunData.bulletPrefab == null) return;

        if (muzzles != null && muzzles.Length > 0)
        {
            foreach (Transform muzzle in muzzles)
            {
                if (muzzle == null) continue;

                // Pooling 방식으로 총알 가져오기 (Instantiate 대신 활용)
                GameObject bullet = BulletPoolManager.Instance.GetBullet(gunData.bulletPrefab, muzzle.position, muzzle.rotation);
                
                // 총알 초기화 (데미지 전달)
                BulletBase bulletComponent = bullet.GetComponentInChildren<BulletBase>();
                if (bulletComponent != null)
                {
                    bulletComponent.Initialize(weaponData.baseDamage);
                }

                Vector3 force = muzzle.forward * gunData.bulletSpeed;
                force.y = 0f;
                
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(force, ForceMode.VelocityChange);
                }
            }
        }
        bulletCount--;
    }

    protected virtual void RequestReload()
    {
        if (nowReloading == false)
        {
            // 이제 PlayerRifle을 직접 호출하지 않고 상태만 변경함.
            // PlayerManager가 이 상태를 감지하여 애니메이션을 실행함.
            nowReloading = true;

            // 재장전 시작 시 루핑 사운드 즉시 중지
            if (gunData != null && gunData.isLoopingSound)
            {
                PlayerSoundManager.Instance.StopWeaponSound(gunData.shootSound);
            }
        }
    }

    protected virtual void PlayShootSound()
    {
        if (gunData != null && gunData.shootSound != null)
        {
            PlayerSoundManager.Instance.PlayWeaponSound(gunData.shootSound);
    }
}
