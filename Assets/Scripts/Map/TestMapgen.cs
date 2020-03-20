using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Map
{
    public class TestMapgen : MonoBehaviour
    {
        MapFeatData mapFeatCode;
        [SerializeField] Generator mapGenerator;
        // Start is called before the first frame update
        void Start() {
            mapFeatCode = mapGenerator.GenerateMap(0, 1);
            Debug.Log("MapFeatCode Generated: "+mapFeatCode);
            Debug.Log("Paths Generated: " + mapFeatCode.Paths.Count);
            foreach (var x in mapFeatCode.Paths)
                Debug.Log("Length: " + x.Cells.Count);
        }

        // Update is called once per frame
        void Update() {

        }
        public void OnDrawGizmos() {
            if (mapFeatCode != null) {
                foreach (var room in mapFeatCode.Features) {
                    Gizmos.color = Color.blue;
                    foreach (var cell in room.OccupiedCells)
                        Gizmos.DrawSphere(room.Rotation* cell + room.Position, 0.5f);
                    Gizmos.color = Color.red;
                    foreach (var cell in room.UsedEntrances)
                        Gizmos.DrawSphere(room.Rotation * cell.Position + room.Position, 0.5f);
                }
                foreach (var path in mapFeatCode.Paths) {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(path.Head, path.Cells[0]);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(path.Tail, path.Cells[path.Cells.Count - 1]);

                    Gizmos.color = Color.magenta;
                    for (int i = 0; i < path.Cells.Count-1; i++)

                        Gizmos.DrawLine(path.Cells[i], path.Cells[i + 1]);
                }
            }
        }
    }
}