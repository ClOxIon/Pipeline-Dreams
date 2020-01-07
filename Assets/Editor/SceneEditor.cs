using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PipelineDreams;
[CustomEditor(typeof(TileRenderer))]
public class SceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileRenderer scene = target as TileRenderer;
    }
}
