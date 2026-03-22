using UnityEngine;
using UnityEngine.UI;
using ProjectNextOn.Data;

public abstract class GunBase : MonoBehaviour
{
    [Header("연동할 데이터 에셋")]
    public GunData gunData;

    [Header("실시간 상태 (데이터에서 초기화됨)")]
    public int bulletCount;
    public int maxBulletCount;
    public bool nowReloading;
    public Button fireBtn;
    public PlayerScriptRifle playerRifle; 
    
    [Header("연사 설정")]
    public float fireRate;
    protected float timer;
    public bool fireBtnDown; // 외부(UI 등)에서 설정해줌

    [Header("머즐 위치 설정")]
    public Transform[] muzzles;

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

        if (fireBtn != null)
        {
            var btn = fireBtn.GetComponent<FireBtn>();
            if (btn != null) fireBtnDown = btn.BtnDown;
        }
        
        bool canFire = fireBtnDown && bulletCount > 0 && !nowReloading;

        // 자동 사격 처리
        if (canFire && timer >= fireRate)
        {
            OnFireButtonClick();
            timer = 0;
        }
        else if (fireBtnDown && bulletCount <= 0 && !nowReloading)
        {
            // 총알이 없는데 버튼을 누르고 있으면 자동 재장전 요청
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
        if (fireBtn != null)
        {
            fireBtn.onClick.RemoveListener(OnFireButtonClick);
            fireBtn.onClick.AddListener(OnFireButtonClick);
        }
    }

    protected virtual void OnDisable()
    {
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

                GameObject bullet = Instantiate(gunData.bulletPrefab, muzzle.position, muzzle.rotation);
                
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
            if (playerRifle != null) 
            {
                playerRifle.reloaing = true;
                nowReloading = true;
            }

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
}
