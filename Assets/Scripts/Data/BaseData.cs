using UnityEngine;

namespace ProjectNextOn.Data
{
    /// <summary>
    /// 모든 아이템, 무기, 스킬 데이터의 최상위 베이스 클래스입니다.
    /// </summary>
    public abstract class BaseData : ScriptableObject
    {
        [Header("기본 정보")]
        public string itemName;            // 아이템 이름 (표시용)
        public GameObject itemPrefab;      // 소환할 프리팹 (3D 모델링 및 스크립트 포함)
        
        [TextArea(3, 10)]
        public string description;         // 아이템 설명
    }
}
