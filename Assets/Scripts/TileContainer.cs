using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "SceneObjectContainer", menuName = "ScriptableObjects/Manager/SceneObjectContainer")]
    public class TileContainer : ScriptableObject {
        MapDataContainer mManager;
        [SerializeField] TileDataset Dataset;
        public const float worldscale = 10;
        public const int sightscale = 12;
        public const int despawnscale = 9;

        bool[,,] GridVisibility = new bool[2 * sightscale + 1, 2 * sightscale + 1, 2 * sightscale + 1];
        List<TileObject> SceneObjects;

        // Start is called before the first frame update
        private void Awake() {
        }

        void SetGridVisibilityRelative(Vector3Int v, bool b) {

            v += Vector3Int.one * sightscale;
            GridVisibility[v.x, v.y, v.z] = b;
        }
        public bool GetGridVisibilityRelative(Vector3Int v) {
            v += Vector3Int.one * sightscale;
            return GridVisibility[v.x, v.y, v.z];
        }
        /*
        GridVisibility.Initialize();
        GridVisibility[sightscale, sightscale, sightscale] = true;
        for (int f = 0; f < 6; f++) {
            var direction = Util.FaceToLHVector(f);
            for (int d = 0; d < sightscale; d++) {
                if (mManager.GetTileRelative(direction * d, 0,Player) == Tile.wall) break;
                SetGridVisibilityRelative(direction * (d + 1), true);
                for (int fp = 0; fp < 6; fp++) {
                    if (fp >> 1 == f >> 1) continue;
                    var directionp = Util.FaceToLHVector(fp);
                    for (int dp = 0; dp < sightscale; dp++) {
                        if (mManager.GetTileRelative(direction * (d + 1) + directionp * dp, 0,Player) == Tile.wall) break;
                        SetGridVisibilityRelative(direction * (d + 1) + directionp * (dp + 1), true);
                    }
                }
            }
        }
        */

        int Squaremetric(Vector3Int a, Vector3Int b) {
            return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));
        }
        public void Init(MapDataContainer mapManager) {
            mManager = mapManager;
            Debug.Log("Initializing world objects");
            SceneObjects = new List<TileObject>();
            for (int i = 0; i < mManager.GetMapScale(0); i++)
                for (int j = 0; j < mManager.GetMapScale(1); j++)
                    for (int k = 0; k < mManager.GetMapScale(2); k++) {

                        for (int f = 0; f < 6; f++) {
                            if (mManager.GetTile(i, j, k, f) == Tile.nothing)
                                continue;
                            var obj = Dataset.Dataset.Find((x) => x.Type == mManager.GetTile(i, j, k, f));
                            if (obj.Prefab != null) {
                                var o = Instantiate(obj.Prefab, new Vector3(i, j, k) * GraphicalConstants.WORLDSCALE, Util.FaceToLHQ(f));
                                o.Position = new Vector3Int(i, j, k);
                                SceneObjects.Add(o);
                            }
                        }

                    }
        }
        void LoadWorldObjects() {

        }
        bool IsOutofRange(bool[,,] m, int i, int j, int k) {
            try {
                var b = m[i, j, k];
            }
            catch (IndexOutOfRangeException e) {

                return true;
            }

            return false;

        }

        void RecalculateGridVisibility() {

        }

    }
}