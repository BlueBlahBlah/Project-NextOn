using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMuzzle : MonoBehaviour
{
    
    public void shoot(GameObject bulletPrefab, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // ì´ì•Œ???ë„ ?ìš© (AddForceë¡?ë³€ê²? yì¶?ê°’ì? 0?¼ë¡œ ?¤ì •)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
