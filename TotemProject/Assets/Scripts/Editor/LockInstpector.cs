using UnityEditor;
using UnityEngine;

static class LockInspector
{
    [MenuItem("Tools/Toggle Inspector Lock %q")] // Ctrl + Q
    static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }

    [MenuItem("Tools/Remove Player Prefs %t")]
    static void RemovePlayerPrefs()
    {
        Debug.Log(PlayerPrefs.HasKey("Level"));
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.HasKey("Level"));
    }
}