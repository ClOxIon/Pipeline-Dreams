using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "TiData", menuName = "ScriptableObjects/TileData", order = 4)]
    public class TileDataset : IPDDataSet {
        public List<PDData> DataSet
        {
            get
            {
                var d = new List<PDData>();
                foreach (var x in dataSet)
                    d.Add(x);
                return d;
            }
        }

        public List<TileData> dataSet;

    }
    [System.Serializable]
    public class TileData : PDData {
        private TileAttribute attribute;
        private Tile prefab;

        public TileAttribute Attribute => attribute;
        public Tile Prefab => prefab; 
    }
}