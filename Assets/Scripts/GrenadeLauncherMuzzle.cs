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
        // 珥앹븣 ?앹꽦
        // 珥앹븣 ?앹꽦
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);

        // 珥앹븣???띾룄 ?곸슜 (AddForce濡?蹂寃? y異?媛믪? 0?쇰줈 ?ㅼ젙)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
