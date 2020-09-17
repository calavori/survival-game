using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            // Set auto update if on change
            if (mapGen.autoUpdate)
            {
                mapGen.DrawMapInEditor();
            }
        }
        // Set generate map button
        if (GUILayout.Button("Generate"))
        {
            mapGen.DrawMapInEditor();
        }
    }
}
