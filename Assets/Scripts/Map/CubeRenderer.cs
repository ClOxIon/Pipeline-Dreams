using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace PipelineDreams.Map
{
    /// <summary>
    /// A simple implementation of maprenderer.
    /// </summary>

    [CreateAssetMenu(fileName = "CubeRenderer", menuName = "ScriptableObjects/Renderer/CubeRenderer")]
    public class CubeRenderer : Renderer
    {

        public override void RenderMap(MapFeatData data)
        {

            List<Vector3Int> entrancePoints = new List<Vector3Int>();
            foreach (var room in data.Features)
            {
                foreach (var p in room.OccupiedCells)
                    for (int f = 0; f < 6; f++)
                    {
                        var vf = Vector3Int.RoundToInt(room.Rotation * p + room.Position + Util.FaceToUVector(f));
                        if (room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation* x.Position+ room.Position) == vf))
                        {
                            if (room.name != "Deadend")
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomEntrance", TM,0);
                            else
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "PipePath", TM,0);
                        }
                        else if (!room.OccupiedCells.Any((x)=> Vector3Int.RoundToInt(room.Rotation * x + room.Position) == vf))
                        {
                            if(room.name!="Deadend")
                            enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomWall", TM,0);
                            else
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "PipeVendingMachine", TM,0);
                        }
                    }

                foreach (var ent in room.UsedEntrances)
                    if (!entrancePoints.Contains(Vector3Int.RoundToInt(room.Rotation*ent.Position + room.Position)))
                        entrancePoints.Add(Vector3Int.RoundToInt(room.Rotation * ent.Position + room.Position));
                  
                
            }
            //Check room entrance points.
            foreach (var p in entrancePoints)
                for (int f = 0; f < 6; f++)
                {
                    if (data.Features.Any((room)=>room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation * x.Position) + room.Position == p && Util.QToFace(x.Rotation * room.Rotation) == f)))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM,0);
                    }
                    else if (data.Paths.Any((path) => (path.Head == p && path.Cells[1] == p + Util.FaceToUVector(f)) || (path.Tail == p && path.Cells[path.Cells.Count - 2] == p + Util.FaceToUVector(f))))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM,0);
                    }
                    else
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipeWall", TM,0);
                }

            List<Vector3Int> PathEnds = new List<Vector3Int>();
            List<Z3Q> PathJoints = new List<Z3Q>();
            foreach (var path in data.Paths)
            {
                PathEnds.Add(path.Head);
                PathEnds.Add(path.Tail);
                PathJoints.AddRange(path.Joints);
            }
            foreach (var path in data.Paths)
            {
                for (int i = 1; i < path.Cells.Count - 1; i++)
                    for (int f = 0; f < 6; f++)
                    {
                        if (path.Cells[i + 1] == path.Cells[i] + Util.FaceToUVector(f) || (path.Cells[i - 1] == path.Cells[i] + Util.FaceToUVector(f)))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM,0);
                        else if (PathJoints.Any((joint)=>joint.Position==path.Cells[i]&&Util.QToFace(joint.Rotation)==f))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM,0);
                        else
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipeWall", TM,0);
                    }
               
            }
            
            
        }
    }
}