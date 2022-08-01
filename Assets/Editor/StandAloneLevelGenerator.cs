using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StandAloneLevelGenerator : EditorWindow
{
    [MenuItem("Window/Super Level Generator/Level Generator GUI")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(StandAloneLevelGenerator));
    }

    private void OnGUI()
    {
        
    }
}
