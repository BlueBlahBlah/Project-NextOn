using UnityEngine;
using UnityEditor;
using ProjectNextOn.Data;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(WeaponChange_Trigger))]
public class WeaponChange_TriggerEditor : Editor
{
    private SerializedProperty weaponDataProp;
    private List<WeaponData> weaponList = new List<WeaponData>();
    private string[] weaponNames;

    private void OnEnable()
    {
        weaponDataProp = serializedObject.FindProperty("weaponData");
        LoadWeaponData();
    }

    private void LoadWeaponData()
    {
        weaponList.Clear();
        string[] guids = AssetDatabase.FindAssets("t:WeaponData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponData data = AssetDatabase.LoadAssetAtPath<WeaponData>(path);
            if (data != null) weaponList.Add(data);
        }
        
        // 보기 편하게 알파벳 순 정렬
        weaponList = weaponList.OrderBy(w => w.name).ToList();
        
        // 첫 번째 슬롯은 비우기 용도
        weaponNames = new string[weaponList.Count + 1];
        weaponNames[0] = "None (비어있음)";
        
        for (int i = 0; i < weaponList.Count; i++)
        {
            string category = weaponList[i] is GunData ? "Gun" : "Melee";
            weaponNames[i + 1] = $"{category}/{weaponList[i].name}";
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Space(10);
        EditorGUILayout.LabelField("무기 획득 트리거 설정", EditorStyles.boldLabel);

        if (weaponNames == null || weaponNames.Length == 0) LoadWeaponData();

        // 1. 현재 인스펙터에 등록된 데이터의 인덱스 찾기
        int selectedIndex = 0;
        WeaponData currentData = weaponDataProp.objectReferenceValue as WeaponData;
        if (currentData != null)
        {
            int foundIndex = weaponList.IndexOf(currentData);
            if (foundIndex >= 0) selectedIndex = foundIndex + 1; // None이 0번이므로 +1
        }

        // 2. 드롭다운(Popup) UI 표시
        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUILayout.Popup("획득할 무기 (Data)", selectedIndex, weaponNames);
        
        // 3. 변경되었다면 프로퍼티 값 갱신
        if (EditorGUI.EndChangeCheck())
        {
            if (selectedIndex == 0)
            {
                weaponDataProp.objectReferenceValue = null;
            }
            else
            {
                weaponDataProp.objectReferenceValue = weaponList[selectedIndex - 1]; // 다시 -1 해서 원본 리스트 인덱스 맞춤
            }
        }

        GUILayout.Space(10);
        
        // 4. 새로운 무기를 만들었을 땐 목록 새로고침 버튼
        if (GUILayout.Button("방금 만든 새 무기 목록 새로고침"))
        {
            LoadWeaponData();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
