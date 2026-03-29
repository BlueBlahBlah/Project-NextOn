using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherMuzzle : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(float speed)
    {
        // ì´ì•Œ ?ì„±
        // ì´ì•Œ ?ì„±
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);

        // ì´ì•Œ???ë„ ?ìš© (AddForceë¡?ë³€ê²? yì¶?ê°’ì? 0?¼ë¡œ ?¤ì •)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
