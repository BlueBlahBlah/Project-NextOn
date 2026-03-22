using UnityEditor;
using UnityEngine;
using ProjectNextOn.Data;
using System.Collections.Generic;
using System.Linq;

public class WeaponDataManagerWindow : EditorWindow
{
    private Vector2 scrollPosition;
    private WeaponData selectedWeapon;
    private Editor weaponEditor;
    private List<WeaponData> weaponDatabase = new List<WeaponData>();

    [MenuItem("Tools/무기 데이터 관리자 (Weapon Data Manager)")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDataManagerWindow>("무기 데이터 관리자").Show();
    }

    private void OnEnable()
    {
        LoadAllWeapons();
    }

    private void LoadAllWeapons()
    {
        weaponDatabase.Clear();
        // AssetDatabase.FindAssets로 WeaponData 타입을 상속받는 모든 에셋 검색
        string[] guids = AssetDatabase.FindAssets("t:WeaponData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponData weapon = AssetDatabase.LoadAssetAtPath<WeaponData>(path);
            if (weapon != null)
            {
                weaponDatabase.Add(weapon);
            }
        }
        // 이름순 정렬
        weaponDatabase = weaponDatabase.OrderBy(w => w.name).ToList();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // 1. 왼쪽 패널: 무기 리스트
        DrawWeaponList();

        // 2. 오른쪽 패널: 선택된 무기의 디테일 인스펙터
        DrawWeaponDetails();

        GUILayout.EndHorizontal();
    }

    private void DrawWeaponList()
    {
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300), GUILayout.ExpandHeight(true));
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("무기 목록", EditorStyles.boldLabel);
        if (GUILayout.Button("새로고침", GUILayout.Width(60)))
        {
            LoadAllWeapons();
        }
        GUILayout.EndHorizontal();
        
        // 새 무기 생성 버튼들
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("새 Gun 생성")) CreateNewWeapon<GunData>("NewGunData");
        if (GUILayout.Button("새 Melee 생성")) CreateNewWeapon<WeaponData>("NewMeleeData");
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var weapon in weaponDatabase)
        {
            if (weapon == null) continue;

            // 선택된 항목 하이라이트 표시
            GUI.backgroundColor = (selectedWeapon == weapon) ? Color.cyan : Color.white;
            
            string displayName = string.IsNullOrEmpty(weapon.itemName) ? weapon.name : weapon.itemName;
            string typePrefix = (weapon is GunData) ? "[Gun] " : "[Melee] ";

            if (GUILayout.Button(typePrefix + displayName, EditorStyles.toolbarButton))
            {
                selectedWeapon = weapon;
                // 선택이 바뀌면 에디터 새로 생성
                if (weaponEditor != null) DestroyImmediate(weaponEditor);
                weaponEditor = Editor.CreateEditor(selectedWeapon);
            }
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndScrollView();
        
        GUILayout.EndVertical();
    }

    private void DrawWeaponDetails()
    {
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        
        if (selectedWeapon != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{selectedWeapon.name} 상세 정보", EditorStyles.boldLabel);
            if (GUILayout.Button("Ping", GUILayout.Width(60)))
            {
                EditorGUIUtility.PingObject(selectedWeapon);
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("삭제", GUILayout.Width(60)))
            {
                if (EditorUtility.DisplayDialog("경고", $"'{selectedWeapon.name}' 에셋을 완전히 삭제하시겠습니까?\n이 작업은 되돌릴 수 없습니다.", "Delete", "Cancel"))
                {
                    string path = AssetDatabase.GetAssetPath(selectedWeapon);
                    AssetDatabase.DeleteAsset(path);
                    selectedWeapon = null;
                    LoadAllWeapons();
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    return;
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            // 유니티 기본 인스펙터를 윈도우 안에 그려줌
            if (weaponEditor == null || weaponEditor.target != selectedWeapon)
            {
                if (weaponEditor != null) DestroyImmediate(weaponEditor);
                weaponEditor = Editor.CreateEditor(selectedWeapon);
            }

            Vector2 rightScroll = Vector2.zero;
            rightScroll = GUILayout.BeginScrollView(rightScroll);
            
            // 변경사항 추적 시작
            EditorGUI.BeginChangeCheck();
            
            weaponEditor.OnInspectorGUI();
            
            if (EditorGUI.EndChangeCheck())
            {
                // 변경사항이 있으면 에셋에 저장
                EditorUtility.SetDirty(selectedWeapon);
                AssetDatabase.SaveAssets(); // 변경 내용 즉시 디스크에 저장
            }
            
            GUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("왼쪽 목록에서 무기를 선택해주세요.", EditorStyles.centeredGreyMiniLabel);
        }
        
        GUILayout.EndVertical();
    }

    private void CreateNewWeapon<T>(string defaultName) where T : WeaponData
    {
        T newWeapon = ScriptableObject.CreateInstance<T>();
        
        // 데이터 폴더가 없으면 생성
        string folderPath = "Assets/WeaponData";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "WeaponData");
        }
        
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{defaultName}.asset");
        AssetDatabase.CreateAsset(newWeapon, path);
        AssetDatabase.SaveAssets();
        
        LoadAllWeapons();
        selectedWeapon = newWeapon;
        if (weaponEditor != null) DestroyImmediate(weaponEditor);
        weaponEditor = Editor.CreateEditor(selectedWeapon);
    }
}
