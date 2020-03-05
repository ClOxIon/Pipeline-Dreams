using UnityEngine;

namespace PipelineDreams {
    public class Tile : PDObject {
        public Vector3Int Position;
        public TileAttribute Attribute { get; private set; }
        public int Face { get; private set; }
    }
}