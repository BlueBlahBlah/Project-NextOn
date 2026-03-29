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

    [MenuItem("Tools/ë¬´ê¸° ?°ì´??ê´€ë¦¬ì (Weapon Data Manager)")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDataManagerWindow>("ë¬´ê¸° ?°ì´??ê´€ë¦¬ì").Show();
    }

    private void OnEnable()
    {
        LoadAllWeapons();
    }

    private void LoadAllWeapons()
    {
        weaponDatabase.Clear();
        // AssetDatabase.FindAssetsë¡?WeaponData ?€?…ì„ ?ì†ë°›ëŠ” ëª¨ë“  ?ì…‹ ê²€??        string[] guids = AssetDatabase.FindAssets("t:WeaponData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponData weapon = AssetDatabase.LoadAssetAtPath<WeaponData>(path);
            if (weapon != null)
            {
                weaponDatabase.Add(weapon);
            }
        }
        // ?´ë¦„???•ë ¬
        weaponDatabase = weaponDatabase.OrderBy(w => w.name).ToList();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        // 1. ?¼ìª½ ?¨ë„: ë¬´ê¸° ë¦¬ìŠ¤??        DrawWeaponList();

        // 2. ?¤ë¥¸ìª??¨ë„: ? íƒ??ë¬´ê¸°???”í…Œ???¸ìŠ¤?™í„°
        DrawWeaponDetails();

        GUILayout.EndHorizontal();
    }

    private void DrawWeaponList()
    {
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(300), GUILayout.ExpandHeight(true));
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("ë¬´ê¸° ëª©ë¡", EditorStyles.boldLabel);
        if (GUILayout.Button("?ˆë¡œê³ ì¹¨", GUILayout.Width(60)))
        {
            LoadAllWeapons();
        }
        GUILayout.EndHorizontal();
        
        // ??ë¬´ê¸° ?ì„± ë²„íŠ¼??        GUILayout.BeginHorizontal();
        if (GUILayout.Button("??Gun ?ì„±")) CreateNewWeapon<GunData>("NewGunData");
        if (GUILayout.Button("??Melee ?ì„±")) CreateNewWeapon<WeaponData>("NewMeleeData");
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var weapon in weaponDatabase)
        {
            if (weapon == null) continue;

            // ? íƒ????ª© ?˜ì´?¼ì´???œì‹œ
            GUI.backgroundColor = (selectedWeapon == weapon) ? Color.cyan : Color.white;
            
            // ?Œì¼ ?´ë¦„(name)ë§??œì‹œ?˜ë„ë¡??ìƒë³µêµ¬
            string displayName = weapon.name;
            string typePrefix = (weapon is GunData) ? "[Gun] " : "[Melee] ";

            if (GUILayout.Button(typePrefix + displayName, EditorStyles.toolbarButton))
            {
                selectedWeapon = weapon;
                // ? íƒ??ë°”ë€Œë©´ ?ë””???ˆë¡œ ?ì„±
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
            GUILayout.Label("?ì…‹ ?Œì¼ ?´ë¦„ (?”í„°ë¡?ë³€ê²?:", GUILayout.Width(170));
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
            GUILayout.Label($"{selectedWeapon.name} ?ì„¸ ?•ë³´", EditorStyles.boldLabel);
            if (GUILayout.Button("?„ì¹˜ì°¾ê¸°", GUILayout.Width(60)))
            {
                EditorGUIUtility.PingObject(selectedWeapon);
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("?? œ", GUILayout.Width(60)))
            {
                if (EditorUtility.DisplayDialog("ê²½ê³ ", $"'{selectedWeapon.name}' ?ì…‹???„ì „???? œ?˜ì‹œê² ìŠµ?ˆê¹Œ?\n???‘ì—…?€ ?˜ëŒë¦????†ìŠµ?ˆë‹¤.", "Delete", "Cancel"))
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
            
            // ? ë‹ˆ??ê¸°ë³¸ ?¸ìŠ¤?™í„°ë¥??ˆë„???ˆì— ê·¸ë ¤ì¤?            if (weaponEditor == null || weaponEditor.target != selectedWeapon)
            {
                if (weaponEditor != null) DestroyImmediate(weaponEditor);
                weaponEditor = Editor.CreateEditor(selectedWeapon);
            }

            Vector2 rightScroll = Vector2.zero;
            rightScroll = GUILayout.BeginScrollView(rightScroll);
            
            // ë³€ê²½ì‚¬??ì¶”ì  ?œì‘
            EditorGUI.BeginChangeCheck();
            
            weaponEditor.OnInspectorGUI();
            
            if (EditorGUI.EndChangeCheck())
            {
                // ë³€ê²½ì‚¬??´ ?ˆìœ¼ë©??ì…‹???€??                EditorUtility.SetDirty(selectedWeapon);
                AssetDatabase.SaveAssets(); // ë³€ê²??´ìš© ì¦‰ì‹œ ?”ìŠ¤?¬ì— ?€??            }
            
            GUILayout.EndScrollView();
        }
        else
        {
            GUILayout.Label("?¼ìª½ ëª©ë¡?ì„œ ë¬´ê¸°ë¥?? íƒ?´ì£¼?¸ìš”.", EditorStyles.centeredGreyMiniLabel);
        }
        
        GUILayout.EndVertical();
    }

    private void CreateNewWeapon<T>(string defaultName) where T : WeaponData
    {
        T newWeapon = ScriptableObject.CreateInstance<T>();
        
        // ?°ì´???´ë”ê°€ ?†ìœ¼ë©??ì„±
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
