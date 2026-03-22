using UnityEngine;
using ProjectNextOn.Data;

public class WeaponChange_Trigger : MonoBehaviour
{
    [Header("획득할 무기 데이터")]
    [SerializeField] private WeaponData weaponData;
    
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 Player 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            if (weaponData == null) return;

            // PlayerManager에 데이터 전달하여 무기 교체 수행
            PlayerManager.Instance.ChangeWeapon(weaponData);

            // 트리거 오브젝트 비활성화 (아이템 획득 처리)
            gameObject.SetActive(false);
        }
    }
}
