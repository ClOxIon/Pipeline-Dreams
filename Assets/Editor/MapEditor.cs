using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MapManager))]
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
        MapManager map = target as MapManager;
        if (GUILayout.Button("Generate Map"))
        {
            map.CreateNewMap();
        }
    }
}
