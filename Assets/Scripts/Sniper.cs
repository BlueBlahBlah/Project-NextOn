using UnityEngine;

public class Sniper : GunBase
{
    public override void OnFireButtonClick()
    {
        // 스나이퍼는 한 발 쏘고 무조건 재장전 시퀀스에 들어갑니다.
        if (nowReloading == false && bulletCount > 0)
        {
            Shoot();
            PlayShootSound();
            RequestReload(); // 한 발 쏘고 바로 재장전 요청
        }
    }

}
