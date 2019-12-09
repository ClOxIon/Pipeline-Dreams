using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PipelineDreams;
[CustomEditor(typeof(TileContainer))]
public class SceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileContainer scene = target as TileContainer;
    }
}
