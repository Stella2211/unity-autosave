using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;

[InitializeOnLoad]
public static class AutoSaveExtension
{
    private const string PREF_KEY_ENABLED = "AutoSave_Enabled";
    private const string PREF_KEY_INTERVAL = "AutoSave_Interval";
    private const string PREF_KEY_SAVE_COPY = "AutoSave_SaveCopy";
    private const string PREF_KEY_SHOW_NOTIFICATIONS = "AutoSave_ShowNotifications";

    private const float DEFAULT_SAVE_INTERVAL = 600f;
    private const bool DEFAULT_ENABLED = true;
    private const bool DEFAULT_SAVE_COPY = false;
    private const bool DEFAULT_SHOW_NOTIFICATIONS = true;

    private static double lastSaveTime;
    private static bool isInitialized = false;

    public static bool Enabled
    {
        get { return EditorPrefs.GetBool(PREF_KEY_ENABLED, DEFAULT_ENABLED); }
        set { EditorPrefs.SetBool(PREF_KEY_ENABLED, value); }
    }

    public static float SaveInterval
    {
        get { return EditorPrefs.GetFloat(PREF_KEY_INTERVAL, DEFAULT_SAVE_INTERVAL); }
        set { EditorPrefs.SetFloat(PREF_KEY_INTERVAL, Mathf.Max(60f, value)); }
    }

    public static bool SaveCopy
    {
        get { return EditorPrefs.GetBool(PREF_KEY_SAVE_COPY, DEFAULT_SAVE_COPY); }
        set { EditorPrefs.SetBool(PREF_KEY_SAVE_COPY, value); }
    }

    public static bool ShowNotifications
    {
        get { return EditorPrefs.GetBool(PREF_KEY_SHOW_NOTIFICATIONS, DEFAULT_SHOW_NOTIFICATIONS); }
        set { EditorPrefs.SetBool(PREF_KEY_SHOW_NOTIFICATIONS, value); }
    }

    static AutoSaveExtension()
    {
        Initialize();
    }

    private static void Initialize()
    {
        if (isInitialized)
            return;

        isInitialized = true;
        lastSaveTime = EditorApplication.timeSinceStartup;

        EditorApplication.update += OnEditorUpdate;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        if (ShowNotifications)
        {
            Debug.Log($"[AutoSave] Initialized - Saving every {SaveInterval / 60f:F1} minutes");
        }
    }

    private static void OnEditorUpdate()
    {
        if (!Enabled)
            return;

        if (EditorApplication.timeSinceStartup - lastSaveTime >= SaveInterval)
        {
            PerformAutoSave();
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (Enabled && state == PlayModeStateChange.ExitingEditMode)
        {
            PerformAutoSave("Auto-saving before entering play mode...");
        }
    }

    private static void PerformAutoSave(string customMessage = null)
    {
        if (EditorApplication.isPlaying || EditorApplication.isCompiling)
            return;

        var activeScene = EditorSceneManager.GetActiveScene();

        if (string.IsNullOrEmpty(activeScene.path))
        {
            if (ShowNotifications)
            {
                Debug.LogWarning("[AutoSave] Scene has not been saved yet. Please save it manually first.");
            }
            lastSaveTime = EditorApplication.timeSinceStartup;
            return;
        }

        try
        {
            string savePath = activeScene.path;

            if (SaveCopy)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(activeScene.path);
                string directory = System.IO.Path.GetDirectoryName(activeScene.path);
                savePath = System.IO.Path.Combine(directory, $"{sceneName}_AutoSave_{timestamp}.unity");
            }

            bool success = EditorSceneManager.SaveScene(activeScene, savePath, SaveCopy);

            if (success)
            {
                lastSaveTime = EditorApplication.timeSinceStartup;

                if (ShowNotifications)
                {
                    string message = customMessage ?? $"[AutoSave] Scene saved: {System.IO.Path.GetFileName(savePath)}";
                    Debug.Log(message);
                }
            }
            else
            {
                if (ShowNotifications)
                {
                    Debug.LogError("[AutoSave] Failed to save scene");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AutoSave] Exception during save: {e.Message}");
        }
    }

    [MenuItem("Tools/AutoSave/Save Now")]
    public static void SaveNow()
    {
        PerformAutoSave("[AutoSave] Manual save triggered");
    }

    public static float GetTimeUntilNextSave()
    {
        if (!Enabled)
            return -1f;

        float timeSinceLastSave = (float)(EditorApplication.timeSinceStartup - lastSaveTime);
        return Mathf.Max(0, SaveInterval - timeSinceLastSave);
    }

    public static void ResetTimer()
    {
        lastSaveTime = EditorApplication.timeSinceStartup;
    }
}