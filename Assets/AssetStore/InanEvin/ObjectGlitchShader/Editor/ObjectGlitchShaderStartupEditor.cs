/*
 * Object Glitch Shader
 * Copyright (c) 2019-Present, Inan Evin, Inc. All rights reserved.
 * Author: Inan Evin
 * contact: inanevin@gmail.com
 *
 * Feel free to ask about the package and talk about recommendations!
 *
 *
 *
 */

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace IE
{
    public class ObjectGlitchShaderAP : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            string[] entries = Array.FindAll(importedAssets, name => name.Contains("ObjectGlitchShaderStartupEditor") && !name.EndsWith(".meta"));

            for (int i = 0; i < entries.Length; i++)
                if (ObjectGlitchShaderStartupEditor.Init(false))
                    break;
        }
    }
    public sealed class ObjectGlitchShaderStartupEditor : EditorWindow
    {
        public static string versionID = "ogs_v";
        static string imagePath = "Assets/InanEvin/ObjectGlitchShader/Editor/Images/ObjectGlitchShader_cover_img.png";

        Texture2D coverImage;
        Vector2 changelogScroll = Vector2.zero;
        GUIStyle labelStyle;
        GUIStyle buttonStyle;
        GUIStyle iconButtonStyle;

        [MenuItem("Help/Object Glitch Shader/About", false, 0)]
        public static void MenuInit()
        {
            Init(true);
        }

        [MenuItem("Help/Object Glitch Shader/Online Docs", false, 0)]
        public static void MenuManual()
        {
            Application.OpenURL("https://docs.google.com/document/d/17iGutMKX7oC0VUPtpjJ8GUA54NzRvxQ3t7jhpVSS4nc/edit?usp=sharing");
        }

        public static bool Init(bool force)
        {
            imagePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("ObjectGlitchShader_cover_img", null)[0]);
            if (force || EditorPrefs.GetString(versionID) != changelogText.Split('\n')[0])
            {
                ObjectGlitchShaderStartupEditor window;
                window = GetWindow<ObjectGlitchShaderStartupEditor>(true, "About Object Glitch Shader", true);
                Vector2 size = new Vector2(620, 800);
                window.minSize = size;
                window.maxSize = size;
                window.ShowUtility();
                return true;
            }

            return false;
        }

        void OnEnable()
        {
            EditorPrefs.SetString(versionID, changelogText.Split('\n')[0]);

            imagePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("ObjectGlitchShader_cover_img", null)[0]);

            string versionColor = EditorGUIUtility.isProSkin ? "#ffffffee" : "#000000ee";
            int maxLength = 10000;
            bool tooLong = changelogText.Length > maxLength;
            if (tooLong)
            {
                changelogText = changelogText.Substring(0, maxLength);
                changelogText += "...\n\n<color=" + versionColor + ">[Check online documentation for more.]</color>";
            }
            changelogText = Regex.Replace(changelogText, @"^[0-9].*", "<color=" + versionColor + "><size=13><b>Version $0</b></size></color>", RegexOptions.Multiline);
            changelogText = Regex.Replace(changelogText, @"^- (\w+:)", "  <color=" + versionColor + ">$0</color>", RegexOptions.Multiline);
            coverImage = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
        }

        private void SetupLabelStyles()
        {
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;
            labelStyle.wordWrap = true;
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.richText = true;
        }

        void OnGUI()
        {
            SetupLabelStyles();

            Rect headerRect = new Rect(0, 0, 620, 245);
            GUI.DrawTexture(headerRect, coverImage, ScaleMode.StretchToFill, false);

            GUILayout.Space(250);

            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                // Doc
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("<b><size=13>Documentation</size></b>\n<size=11>Online manual.</size>", buttonStyle, GUILayout.MaxWidth(170), GUILayout.Height(56)))
                        Application.OpenURL("https://docs.google.com/document/d/17iGutMKX7oC0VUPtpjJ8GUA54NzRvxQ3t7jhpVSS4nc/edit?usp=sharing");

                    if (GUILayout.Button("<b><size=13>Rate & Review</size></b>\n<size=11>Rate Object Glitch Shader on Asset Store!</size>", buttonStyle, GUILayout.Height(56)))
                        Application.OpenURL("https://assetstore.unity.com/packages/p/object-glitch-shader-163173");

                    if (GUILayout.Button("<b>Unity Forum Post</b>\n<size=9>Unity Community</size>", buttonStyle, GUILayout.Height(56)))
                        Application.OpenURL("https://forum.unity.com/threads/released-object-glitch-shader-v1-0.841168/");
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("<b>E-mail</b>\n<size=9>inanevin@gmail.com</size>", buttonStyle, GUILayout.MaxWidth(300), GUILayout.Height(36)))
                        Application.OpenURL("mailto:inanevin@gmail.com");

                    if (GUILayout.Button("<b>Twitter</b>\n<size=9>@lineupthesky</size>", buttonStyle, GUILayout.Height(36)))
                        Application.OpenURL("http://twitter.com/lineupthesky");

                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                using (var scope = new GUILayout.ScrollViewScope(changelogScroll))
                {
                    GUILayout.Label(changelogText, labelStyle);
                    changelogScroll = scope.scrollPosition;
                }
            }
        }

        static string changelogText = "1.3 \n" +
            "- Unlit Sprite support for Unity versions above 2019.3 and above! \n\n" +
            "1.2 \n" +
            "- Editor utility changes. \n" +
            "- Band Glitch progress is now controllable outside the shader graph, via exposed value or scripting. \n" +
            "- Added an example script to make the band glitch loop for desired amount.\n\n" +
            "1.1\n" +
            "- Added support for Unity versions above 2019.2.x\n" +
            "- Added support for Lightweight Render Pipeline\n" +
            "- Checked and confirmed mobile support.\n\n" +
            "1.0\n" +
            "- Initial version.\n";
    }
}
