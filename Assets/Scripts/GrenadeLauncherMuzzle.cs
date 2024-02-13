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
        // 총알 생성
        // 총알 생성
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);

        // 총알에 속도 적용 (AddForce로 변경, y축 값은 0으로 설정)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
