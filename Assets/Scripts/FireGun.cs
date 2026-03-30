using UnityEngine;

public class FireGun : GunBase
{
    [Header("화염 이펙트")]
    [SerializeField] private GameObject Effect;

    protected override void Start()
    {
        base.Start();
        if (Effect != null) Effect.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        // 사격 중이 아닐 때는 화염 비활성화
        if (Effect != null && !IsFiring)
        {
            var flame = Effect.GetComponent<FireGunFlame>();
            if (flame != null) flame.active = false;
            Effect.SetActive(false);
        }
    }

    public override void OnFireButtonClick()
    {
        if (bulletCount > 0)
        {
            if (Effect != null)
            {
                Effect.SetActive(true);
                var flame = Effect.GetComponent<FireGunFlame>();
                if (flame != null) flame.active = true;
            }
            
            bulletCount--; // 화염방사기는 직접 차감 (별도의 투사체 생성이 없음)
            PlayShootSound();
        }
        else
        {
            if (Effect != null)
            {
                var flame = Effect.GetComponent<FireGunFlame>();
                if (flame != null) flame.active = false;
                Effect.SetActive(false);
            }
            RequestReload();
        }
    }

}
