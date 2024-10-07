using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
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

    public Button fireBtn;
    public bool nowReloading;

    // Start is called before the first frame update
    void Start()
    {
        nowReloading = false;
    }
    
    private void OnEnable()
    {
        // 버튼 클릭 이벤트 등록
        fireBtn.onClick.AddListener(OnFireButtonClick);
    }
    

    void OnFireButtonClick()
    {
        if (nowReloading == false)
        {
            Shoot();
            PlayerSoundManager.Instance.Sniper_Shoot_Sound();
            GameObject.Find("Check_Sprite_Long").GetComponent<PlayerScriptRifle>().reloaing = true;     //재장전중으로 수정
            nowReloading = true;
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
