using UnityEngine;
using ProjectNextOn.Data;

public class WeaponChange_Trigger : MonoBehaviour
{
    [Header("획득할 무기 데이터")]
    public WeaponData weaponData;
    
    [Header("프리뷰가 생성될 전시기둥(Pivot)")]
    public Transform pivot;

    public void UpdatePreview()
    {
        // 1. 피벗이 없으면 자식 중에 찾기
        if (pivot == null)
        {
            pivot = transform.Find("Pivot");
        }
        if (pivot == null) return;

        // 2. 피벗 하위에 이미 무기가 있다면 삭제
        // 자식 오브젝트들을 모두 없앰 (에디터 상이므로 DestroyImmediate 사용)
        for (int i = pivot.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(pivot.GetChild(i).gameObject);
        }

        // 3. 새 무기 생성
        if (weaponData != null && weaponData.itemPrefab != null)
        {
            GameObject preview = Instantiate(weaponData.itemPrefab, pivot);
            preview.transform.localPosition = Vector3.zero;
            preview.transform.localRotation = Quaternion.identity;
            
            // 프리뷰용이므로 잡다한 스크립트를 꺼줄 수도 있음 (삭제하면 인스펙터 에러 발생 가능)
            MonoBehaviour[] scripts = preview.GetComponents<MonoBehaviour>();
            foreach(var s in scripts) 
            {
                s.enabled = false;
            }
        }
    }

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
