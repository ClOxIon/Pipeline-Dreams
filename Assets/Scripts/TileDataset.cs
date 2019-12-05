using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TiData", menuName = "ScriptableObjects/TileData", order = 4)]
public class TileDataset : ScriptableObject {

    public List<TileData> Dataset;

}
[System.Serializable]
public class TileData : Data {
    public Tile Type;
    public SceneObject Prefab;

}