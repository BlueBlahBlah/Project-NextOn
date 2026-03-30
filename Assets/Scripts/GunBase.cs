using UnityEngine;
using UnityEngine.UI;
using ProjectNextOn.Data;
using System;

public abstract class GunBase : WeaponBase
{
    public GunData gunData => weaponData as GunData;

    [Header("Runtime State")]
    public int bulletCount;
    public int maxBulletCount;
    public bool nowReloading;
    public bool IsFiring; 
    
    public event Action<int, int> OnAmmoChanged; // [Broadcasting] 탄약 변경 이벤트 (현재탄환, 최대탄환)

    [Header("Muzzle Setup")]
    public Transform[] muzzles;

    protected virtual void Awake()
    {
        if (muzzles == null || muzzles.Length == 0)
        {
            Transform muzzleParent = FindMuzzleRecursive(transform, "muzzle") ?? FindMuzzleRecursive(transform, "Muzzle");

            if (muzzleParent != null)
            {
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
                    muzzles = new Transform[] { muzzleParent };
                }
            }
        }
    }

    private Transform FindMuzzleRecursive(Transform parent, string name)
    {
        if (parent.name.Contains(name)) return parent; 
        
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
            
            Debug.Log($"<color=cyan>[Model]</color> Ammo Initialized: {bulletCount} / {maxBulletCount}");
            OnAmmoChanged?.Invoke(bulletCount, maxBulletCount);
        }
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        bool canFire = IsFiring && bulletCount > 0 && !nowReloading;

        if (canFire && timer >= fireRate)
        {
            OnFireButtonClick();
            timer = 0;
        }
        else if (IsFiring && bulletCount <= 0 && !nowReloading)
        {
            RequestReload();
        }

        if (gunData != null && gunData.isLoopingSound && gunData.shootSound != null)
        {
            if (canFire)
            {
                var source = PlayerSoundManager.Instance.GetAudioSource(gunData.shootSound);
                if (source != null && !source.isPlaying)
                {
                    source.loop = true;
                    source.Play();
                }
            }
            else
            {
                PlayerSoundManager.Instance.StopWeaponSound(gunData.shootSound);
            }
        }
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
        IsFiring = false; 
        
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

    // 외부(보급 상자 등)에서 탄약을 추가할 때 사용하는 메서드 (UI 동기화 포함)
    public void AddAmmo(int amount)
    {
        maxBulletCount += amount;
        Debug.Log($"<color=cyan>[Model]</color> Ammo Added: +{amount} (Total: {maxBulletCount})");
        
        // [Broadcast] 탄약 보급 상태를 컨트롤러와 UI에 즉시 알림
        OnAmmoChanged?.Invoke(bulletCount, maxBulletCount);
    }

    protected virtual void Shoot()
    {
        if (gunData == null || gunData.bulletPrefab == null) return;

        if (muzzles != null && muzzles.Length > 0)
        {
            foreach (Transform muzzle in muzzles)
            {
                if (muzzle == null) continue;

                GameObject bullet = BulletPoolManager.Instance.GetBullet(gunData.bulletPrefab, muzzle.position, muzzle.rotation);
                
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
        Debug.Log($"<color=cyan>[Model]</color> Ammo Changed (Shoot): {bulletCount} / {maxBulletCount}");
        OnAmmoChanged?.Invoke(bulletCount, maxBulletCount);
    }

    protected virtual void RequestReload()
    {
        if (nowReloading == false)
        {
            nowReloading = true;
            Debug.Log("<color=cyan>[Model]</color> Reload Requested.");
        }
    }

    public virtual void CompleteReload()
    {
        if (gunData == null) return;

        int reloadAmount = gunData.maxMagazineSize;
        
        if (maxBulletCount >= reloadAmount)
        {
            maxBulletCount -= reloadAmount;
            bulletCount += reloadAmount;
        }
        else
        {
            bulletCount += maxBulletCount;
            maxBulletCount = 0;
        }
        nowReloading = false;
        
        Debug.Log($"<color=cyan>[Model]</color> Reload Completed: {bulletCount} / {maxBulletCount}");
        // [Broadcast] 재장전 완료 후 UI 갱신 방송
        OnAmmoChanged?.Invoke(bulletCount, maxBulletCount);
    }

    public virtual void PlayShootSound()
    {
        if (gunData != null && gunData.shootSound != null)
        {
            PlayerSoundManager.Instance.PlayWeaponSound(gunData.shootSound);
        }
    }
}
