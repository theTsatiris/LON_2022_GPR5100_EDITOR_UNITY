using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SALGHelp : EditorWindow
{
    [MenuItem("Window/Super Level Generator/Level Generator Help")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SALGHelp));
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("YADA YADA DOCUMENTATION ETC.", MessageType.Info);
    }
}
