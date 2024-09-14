using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineGun : MonoBehaviour
{
    [Header("총 연사속도 조정")] 
    public float fireRate;
    [Header("탄알 개수")]
    public int bulletCount;
    public int maxBulletCount;
    [Header("탄알 프리팹")]
    public GameObject bulletPrefab;
    [Header("탄속")]
    public float speed;

    //public Button fireBtn;
    public bool nowReloading;
    public bool fireBtnDowing;      //격발 버튼이 눌리고 있는지
    private float timer;
    public GameObject Effect;

    // Start is called before the first frame update
    void Start()
    {
        // 버튼 클릭 이벤트 등록
        //fireBtn.onClick.AddListener(OnFireButtonClick);
        nowReloading = false;
        timer = 0;
        Effect.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (fireBtnDowing && timer > fireRate)
        {
            OnFireButtonClick();
            timer = 0;
        }
        else if (fireBtnDowing == false)
        {
            Effect.SetActive(false);
        }
            
    }


    public void OnFireButtonClick()
    {
        if (bulletCount > 0)
        {
            Effect.SetActive(true);
            // 버튼이 눌렸을 때 발사
            Shoot();
            
        }
        else
        {
            if (nowReloading == false)
            {
                Effect.SetActive(false);        //재장전일때도 탄피가 튀지않게
                // 탄알이 없을 때의 처리 (e.g., 재장전 등)
                Debug.Log("탄알이 없습니다!");
                GameObject.Find("Check_Sprite_Long").GetComponent<PlayerScriptRifle>().reloaing = true;     //재장전중으로 수정
                nowReloading = true;
            }
            //재장전 중인데 버튼 연타시 많이 재장전 됨을 방지
            
        }
       
    }

    void Shoot()
    {
        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // 총알에 속도 적용 (AddForce로 변경, y축 값은 0으로 설정)
        Vector3 force = transform.forward * speed;
        force.y = 0f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

        // 탄알 개수 감소
        bulletCount--;
    }
}
