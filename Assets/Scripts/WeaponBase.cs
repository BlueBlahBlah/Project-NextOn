using UnityEngine;
using ProjectNextOn.Data;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("장착된 무기 데이터 (ScriptableObject)")]
    public WeaponData weaponData;
    public float fireRate;
    public float timer;
}
