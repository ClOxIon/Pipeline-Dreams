using UnityEngine;

namespace PipelineDreams {
    public class StationMapGenerator : MapGenerator {
        public override MapBundle CreateMap() {
            var m = new MapBundle {
                v = new MapBoxel[6, 6, 6]

            };
            for (int i = 0; i < m.v.GetLength(0); i++)
                for (int j = 0; j < m.v.GetLength(1); j++)
                    for (int k = 0; k < m.v.GetLength(2); k++) {
                        m.v[i, j, k].b = (Block)UnityEngine.Random.Range(0, 2);
                        m.v[i, j, k].t = new TileAttribute[6];
                    }

            GenerateTrivialTopology(m);
            return m;
        }
        protected override void GenerateTrivialTopology(MapBundle m) {
            for (int i = 0; i < m.v.GetLength(0); i++)
                for (int j = 0; j < m.v.GetLength(1); j++)
                    for (int k = 0; k < m.v.GetLength(2); k++)
                        if (m.v[i, j, k].b == 0)
                            for (int f = 0; f < 6; f++)
                                m.v[i, j, k].t[f] = 0;
                        else {
                            for (int f = 0; f < 6; f++) {
                                var v = new Vector3Int(i, j, k) + Util.FaceToLHVector(f);
                                m.v[i, j, k].t[f] = (IsOutofRange(m, v) || m.v[v.x, v.y, v.z].b == Block.nothing) ? TileAttribute.wall : TileAttribute.hole;
                            }
                        }
        }
    }
}