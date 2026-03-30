using UnityEngine;
using ProjectNextOn.Data;

public class BulletSupply : MonoBehaviour
{
    [Header("보급할 탄약 비율")]
    [Tooltip("기본값 1 = 무기의 최대 소지 탄약(TotalMaxAmmo)만큼 회복")]
    [Range(0.1f, 10f)]
    public float supplyRatio = 1f;

    [Header("일회성")]
    public bool destroyOnPickup = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 하위에 있는 원거리 무기 스크립트를 찾음
            PlayerScriptRifle rifleScript = other.GetComponentInChildren<PlayerScriptRifle>();
            
            // 현재 활성화된 원거리 무기 상태이고 들고 있는 총이 존재한다면
            if (rifleScript != null && rifleScript.gameObject.activeSelf && rifleScript.currentGun != null)
            {
                GunBase gun = rifleScript.currentGun;
                GunData gd = gun.gunData;

                if (gd != null)
                {
                    // GunData에 정의된 최대 소지 가능 탄수(totalMaxAmmo) 기준으로 획득량 계산
                    // 예: supplyRatio가 0.5이고 최대 탄수가 100이면 50발 보충
                    int supplyAmount = Mathf.Max(1, Mathf.FloorToInt(gd.totalMaxAmmo * supplyRatio));
                    
                    // 현재 총기 예비 탄약에 보충 (메서드 호출을 통해 UI 동기화 유도)
                    gun.AddAmmo(supplyAmount); 

                    // 아이템 획득 후 상자 파괴 (비활성화)
                    if (destroyOnPickup)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
