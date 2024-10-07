using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [Header("총 연사속도 조정")] 
    public float fireRate;
    [Header("탄알 개수")]
    public int bulletCount;
    public int maxBulletCount;
    
    
    //public Button fireBtn;
    public bool nowReloading;
    public bool fireBtnDowing;      //격발 버튼이 눌리고 있는지
    private float timer;
    [SerializeField] private GameObject Effect;
   
    // Start is called before the first frame update
    void Start()
    {
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
            Effect.GetComponent<FireGunFlame>().active = false;
            Effect.SetActive(false);
        }
    }


    public void OnFireButtonClick()
    {
        if (bulletCount > 0)
        {
            Effect.SetActive(true);
            Effect.GetComponent<FireGunFlame>().active = true;
            // 버튼이 눌렸을 때 발사
            bulletCount--;
            
        }
        else
        {
            if (nowReloading == false)
            {
                Effect.GetComponent<FireGunFlame>().active = false;
                Effect.SetActive(false);
                // 탄알이 없을 때의 처리 (e.g., 재장전 등)
                Debug.Log("탄알이 없습니다!");
                GameObject.Find("Check_Sprite_Long").GetComponent<PlayerScriptRifle>().reloaing = true;     //재장전중으로 수정
                nowReloading = true;
            }
            //재장전 중인데 버튼 연타시 많이 재장전 됨을 방지
            
        }
       
    }
    
}
