using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "TiData", menuName = "ScriptableObjects/TileData", order = 4)]
    public class TileDataset : ScriptableObject {

        public List<TileData> Dataset;

    }
    [System.Serializable]
    public class TileData : Data {
        public Tile Type;
        public TileObject Prefab;

    }
}