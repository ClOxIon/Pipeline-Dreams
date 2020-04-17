using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Map
{
    public class MapGizmoDraw : MonoBehaviour
    {
        [SerializeField] Generator mg;
        readonly int scale = 10;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnDrawGizmos()
        {
            var mapFeatCode = mg.LastGenData;
            if (mapFeatCode != null)
            {
                foreach (var room in mapFeatCode.Features)
                {
                    Gizmos.color = Color.blue;
                    foreach (var cell in room.OccupiedCells)
                        Gizmos.DrawSphere(scale*(room.Rotation * cell + room.Position), 0.5f);
                    ;
                    foreach (var cell in room.UsedEntrances)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(scale * (room.Rotation * cell.Position + room.Position), 0.5f);
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(scale * (room.Rotation * cell.Position + room.Position), scale * (room.Rotation * (cell.Position+Util.LHQToLHUnitVector(cell.Rotation)) + room.Position));
                    }
                    
                }
                foreach (var path in mapFeatCode.Paths)
                {
                   

                    Gizmos.color = Color.magenta;
                    for (int i = 0; i < path.Cells.Count - 1; i++)

                        Gizmos.DrawLine(scale * path.Cells[i], scale * path.Cells[i + 1]);
                }
            }
        }
    }
}