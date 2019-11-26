using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(SceneObjectManager))]
public class SceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneObjectManager scene = target as SceneObjectManager;
    }
}
