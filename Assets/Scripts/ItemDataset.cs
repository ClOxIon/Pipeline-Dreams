using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItData", menuName = "ScriptableObjects/ItemData", order = 3)]
public class ItemDataset : ScriptableObject {

    public List<ItemData> Dataset;

}
[System.Serializable]
public class Data {
    public string Name;
    [TextArea(5, 10)]
    public string Description;
    public Sprite Icon;
    [SerializeField] List<Parameter> Parameters;
    public bool HasParameter(string Name) {
        foreach (var x in Parameters)
            if (x.Name == Name)
                return true;
        return false;
    }
    public float FindParameterFloat(string Name) {
        foreach (var x in Parameters)
            if (x.Name == Name)
                return float.Parse(x.Value);
        Debug.LogWarning("No Float Parameter Found: " + GetType());
        return 0;
    }
    public int FindParameterInt(string Name) {
        foreach (var x in Parameters)
            if (x.Name == Name)
                return int.Parse(x.Value);
        Debug.LogWarning("No Integer Parameter Found: " + GetType());
        return 0;
    }
    public string FindParameterString(string Name) {
        foreach (var x in Parameters)
            if (x.Name == Name)
                return x.Value;
        Debug.LogWarning("No String Parameter Found: " + GetType());
        return null;
    }
}
[System.Serializable]
public class ItemData : Data {
    [NonSerialized]public string[] ItemActions;
    [NonSerialized] public string DefaultAction;

}
[System.Serializable]
public struct Parameter {
    public string Name;
    public string Value;
}