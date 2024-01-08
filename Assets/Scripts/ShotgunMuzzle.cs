using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMuzzle : MonoBehaviour
{
    
    public void shoot(GameObject bulletPrefab, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // 총알에 속도 적용 (AddForce로 변경, y축 값은 0으로 설정)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
