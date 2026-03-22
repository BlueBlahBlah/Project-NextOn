using UnityEngine;

public class MachineGun : GunBase
{
    [Header("머신건 이펙트")]
    public GameObject Effect;

    protected override void Start()
    {
        base.Start();
        if (Effect != null) Effect.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        // 사격 중이 아닐 때는 이펙트 비활성화
        if (Effect != null && !fireBtnDown)
        {
            Effect.SetActive(false);
        }
    }

    public override void OnFireButtonClick()
    {
        if (bulletCount > 0)
        {
            if (Effect != null) Effect.SetActive(true);
            Shoot();
            PlayShootSound();
        }
        else
        {
            if (Effect != null) Effect.SetActive(false);
            RequestReload();
        }
    }

}
