using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PipelineDreams;
using System.Collections.Generic;

[CustomEditor(typeof(MapDataContainer))]
public class MapEditor : Editor
{
    //SerializedProperty damageProp;
    void OnEnable()
    {
        // Setup the SerializedProperties.
        //damageProp = serializedObject.FindProperty ("damage");
    }

    public override void OnInspectorGUI()
    {
        MapDataContainer map = target as MapDataContainer;
        if (GUILayout.Button("Generate Map"))
        {
            //map.CreateNewMap();
        }
    }
}
