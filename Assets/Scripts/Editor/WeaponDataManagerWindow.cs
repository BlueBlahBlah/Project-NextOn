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

    [MenuItem("Tools/Weapon Data Manager")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDataManagerWindow>("Weapon Data Manager").Show();
    }

    private void OnEnable()
    {
        LoadAllWeapons();
    }

    private void LoadAllWeapons()
    {
        weaponDatabase.Clear();
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
        weaponDatabase = weaponDatabase.OrderBy(w => w.name).ToList();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        DrawWeaponList();
        DrawWeaponDetails();
        GUILayout.EndHorizontal();
    }

    private void DrawWeaponList()
    {
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300), GUILayout.ExpandHeight(true));
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapon List", EditorStyles.boldLabel);
        if (GUILayout.Button("Refresh", GUILayout.Width(60)))
        {
            LoadAllWeapons();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Gun")) CreateNewWeapon<GunData>("NewGunData");
        if (GUILayout.Button("New Melee")) CreateNewWeapon<WeaponData>("NewMeleeData");
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var weapon in weaponDatabase)
        {
            if (weapon == null) continue;

            GUI.backgroundColor = (selectedWeapon == weapon) ? Color.cyan : Color.white;
            
            string displayName = weapon.name;
            string typePrefix = (weapon is GunData) ? "[Gun] " : "[Melee] ";

            if (GUILayout.Button(typePrefix + displayName, EditorStyles.toolbarButton))
            {
                selectedWeapon = weapon;
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
            GUILayout.Label("Asset file name:", GUILayout.Width(170));
            string newFileName = EditorGUILayout.DelayedTextField(selectedWeapon.name);
            if (newFileName != selectedWeapon.name && !string.IsNullOrEmpty(newFileName))
            {
                string path = AssetDatabase.GetAssetPath(selectedWeapon);
                AssetDatabase.RenameAsset(path, newFileName);
                AssetDatabase.SaveAssets();
                
                LoadAllWeapons();
                selectedWeapon = weaponDatabase.FirstOrDefault(w => w.name == newFileName) ?? weaponDatabase[0];
                if (weaponEditor != null) DestroyImmediate(weaponEditor);
                weaponEditor = Editor.CreateEditor(selectedWeapon);
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"{selectedWeapon.name} Details", EditorStyles.boldLabel);
            if (GUILayout.Button("Ping", GUILayout.Width(60)))
            {
                EditorGUIUtility.PingObject(selectedWeapon);
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                if (EditorUtility.DisplayDialog("Warning", $"Are you sure you want to delete '{selectedWeapon.name}'?\nThis action cannot be undone.", "Delete", "Cancel"))
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
            
            if (weaponEditor == null || weaponEditor.target != selectedWeapon)
            {
                if (weaponEditor != null) DestroyImmediate(weaponEditor);
                weaponEditor = Editor.CreateEditor(selectedWeapon);
            }

            Vector2 rightScroll = Vector2.zero;
            rightScroll = GUILayout.BeginScrollView(rightScroll);
            
            EditorGUI.BeginChangeCheck();
            
            weaponEditor.OnInspectorGUI();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(selectedWeapon);
                AssetDatabase.SaveAssets();
            }
            
            GUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("Please select a weapon from the list.", EditorStyles.centeredGreyMiniLabel);
        }
        
        GUILayout.EndVertical();
    }

    private void CreateNewWeapon<T>(string defaultName) where T : WeaponData
    {
        T newWeapon = ScriptableObject.CreateInstance<T>();
        
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
