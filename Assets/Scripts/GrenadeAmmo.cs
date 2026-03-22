using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Effect;
    [SerializeField] private Rigidbody rigidbody;
    //[SerializeField] private DamageManager DamageManager;
    public int Damage;
    public float boomTimer = 1;

    private bool dead;          //유탄이 터지면 데미지가 여러번 들어가는 것 방지
    // Start is called before the first frame update
    void Start()
    {
        Bullet.SetActive(true);
        Effect.SetActive(false);
        dead = false;
        rigidbody = GetComponent<Rigidbody>();
        Damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rigidbody.velocity;
        if (dead == false)
        {
            if (transform.position.y < 0.2F)
            {
                dead = true;
                explosion();
            }
            boomTimer -= Time.deltaTime;
            if (boomTimer < 0)
            {
                dead = true;
                explosion();
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && dead == true)        //적 오브젝트에 부딪히면
        {
            explosion();
        }
    }

    void explosion()
    {
        PlayerSoundManager.Instance.Granade_Explosion_Sound();
        rigidbody.velocity = Vector3.zero;                                  //움직임 그만
        rigidbody.angularVelocity = new Vector3(0, 0, 0);             //회전 그만
        Effect.SetActive(true);
        Bullet.SetActive(false);
        
        int TempDamage =  DamageManager.Instance.GrenadeLauncher_DamageCounting * Damage;

        Collider[] colls;
        colls = Physics.OverlapSphere(transform.position, 3f);
        if (colls.Length == 0)      //반경에 아무것도 없는 경우
        {
            Destroy(gameObject,1.5f);
        }

        foreach (Collider collider in colls)
        {
            if (collider.CompareTag("Enemy"))       //Enemy tag를 가진경우
            {
                //공격하는 매커니즘
                collider.GetComponent<Enemy>().curHealth -= TempDamage ;
            }
        }
        Destroy(gameObject,1.5f);
    }
}
