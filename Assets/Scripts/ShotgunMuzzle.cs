using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMuzzle : MonoBehaviour
{
    
    public void shoot(GameObject bulletPrefab, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // 珥앹븣???띾룄 ?곸슜 (AddForce濡?蹂寃? y異?媛믪? 0?쇰줈 ?ㅼ젙)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
