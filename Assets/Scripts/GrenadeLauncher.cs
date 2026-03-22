using UnityEngine;

public class GrenadeLauncher : GunBase
{
    [SerializeField] private GrenadeLauncherMuzzle Muzzle;

    protected override void Shoot()
    {
        if (Muzzle != null && gunData != null)
        {
            Muzzle.shoot(gunData.bulletSpeed);
        }
        bulletCount--;
    }

}
