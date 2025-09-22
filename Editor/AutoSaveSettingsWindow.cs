using UnityEditor;
using UnityEngine;

public class AutoSaveSettingsWindow : EditorWindow
{
    private bool enabled;
    private float saveInterval;
    private bool saveCopy;
    private bool showNotifications;
    private float timeUntilNextSave;
    
    private GUIStyle headerStyle;
    private GUIStyle statusStyle;
    private bool stylesInitialized = false;

    [MenuItem("Tools/AutoSave/Settings")]
    public static void ShowWindow()
    {
        var window = GetWindow<AutoSaveSettingsWindow>("AutoSave Settings");
        window.minSize = new Vector2(350, 250);
        window.Show();
    }

    private void OnEnable()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        enabled = AutoSaveExtension.Enabled;
        saveInterval = AutoSaveExtension.SaveInterval;
        saveCopy = AutoSaveExtension.SaveCopy;
        showNotifications = AutoSaveExtension.ShowNotifications;
    }

    private void InitStyles()
    {
        if (stylesInitialized)
            return;

        headerStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft
        };

        statusStyle = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Italic,
            wordWrap = true
        };

        stylesInitialized = true;
    }

    private void OnGUI()
    {
        InitStyles();
        
        EditorGUILayout.Space(10);
        
        EditorGUILayout.LabelField("AutoSave Configuration", headerStyle);
        EditorGUILayout.Space(10);

        EditorGUI.BeginChangeCheck();

        enabled = EditorGUILayout.Toggle("Enable AutoSave", enabled);
        
        EditorGUILayout.Space(5);

        GUI.enabled = enabled;
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Save Interval");
        saveInterval = EditorGUILayout.Slider(saveInterval / 60f, 1f, 60f) * 60f;
        EditorGUILayout.LabelField($"{saveInterval / 60f:F1} min", GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(5);

        saveCopy = EditorGUILayout.Toggle(new GUIContent("Save as Copy", 
            "If enabled, creates timestamped backup files instead of overwriting the original scene"), 
            saveCopy);
        
        showNotifications = EditorGUILayout.Toggle(new GUIContent("Show Notifications", 
            "Display console messages when auto-save occurs"), 
            showNotifications);

        GUI.enabled = true;

        if (EditorGUI.EndChangeCheck())
        {
            SaveSettings();
        }

        EditorGUILayout.Space(20);
        DrawSeparator();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Status", headerStyle);
        EditorGUILayout.Space(5);

        if (enabled)
        {
            timeUntilNextSave = AutoSaveExtension.GetTimeUntilNextSave();
            
            if (timeUntilNextSave > 0)
            {
                int minutes = (int)(timeUntilNextSave / 60);
                int seconds = (int)(timeUntilNextSave % 60);
                EditorGUILayout.LabelField($"Next auto-save in: {minutes:00}:{seconds:00}", statusStyle);
                
                Rect progressRect = GUILayoutUtility.GetRect(18, 18, "TextField");
                float progress = 1f - (timeUntilNextSave / saveInterval);
                EditorGUI.ProgressBar(progressRect, progress, "");
            }
            else
            {
                EditorGUILayout.LabelField("Auto-save will trigger soon...", statusStyle);
            }
        }
        else
        {
            EditorGUILayout.LabelField("AutoSave is disabled", statusStyle);
        }

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Save Now", GUILayout.Height(30)))
        {
            AutoSaveExtension.SaveNow();
            ShowNotification(new GUIContent("Scene Saved!"));
        }
        
        if (GUILayout.Button("Reset Timer", GUILayout.Height(30)))
        {
            AutoSaveExtension.ResetTimer();
            ShowNotification(new GUIContent("Timer Reset"));
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Reset to Defaults", GUILayout.Height(25)))
        {
            ResetToDefaults();
        }

        Repaint();
    }

    private void SaveSettings()
    {
        AutoSaveExtension.Enabled = enabled;
        AutoSaveExtension.SaveInterval = saveInterval;
        AutoSaveExtension.SaveCopy = saveCopy;
        AutoSaveExtension.ShowNotifications = showNotifications;
        
        if (enabled)
        {
            AutoSaveExtension.ResetTimer();
        }
    }

    private void ResetToDefaults()
    {
        enabled = true;
        saveInterval = 600f;
        saveCopy = false;
        showNotifications = true;
        SaveSettings();
        ShowNotification(new GUIContent("Reset to defaults"));
    }

    private void DrawSeparator()
    {
        var rect = GUILayoutUtility.GetRect(1f, 1f);
        rect.xMin = 0f;
        rect.width += 4f;
        
        if (Event.current.type == EventType.Repaint)
        {
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
    }
}