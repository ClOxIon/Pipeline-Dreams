using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace PipelineDreams
{
    /// <summary>
    /// A simple implementation of maprenderer.
    /// </summary>
    [CreateAssetMenu]
    public class CubeRenderer : MapRenderer
    {

        public override void RenderMap(MapFeatData data)
        {

            List<Vector3Int> entrancePoints = new List<Vector3Int>();
            foreach (var room in data.Features)
            {
                foreach (var p in room.OccupiedCells)
                    for (int f = 0; f < 6; f++)
                    {
                        var vf = Vector3Int.RoundToInt(room.Rotation * p + room.Position + Util.FaceToLHVector(f));
                        if (room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation* x.Position+ room.Position) == vf))
                        {
                            if (room.Name != "Deadend")
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomEntrance", TM);
                            else
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "PipePath", TM);
                        }
                        else if (!room.OccupiedCells.Any((x)=> Vector3Int.RoundToInt(room.Rotation * x + room.Position) == vf))
                        {
                            if(room.Name!="Deadend")
                            enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomWall", TM);
                            else
                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "PipeVendingMachine", TM);
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
                    if (data.Features.Any((room)=>room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation * x.Position) + room.Position == p && Util.LHQToFace(x.Rotation * room.Rotation) == f)))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM);
                    }
                    else if (data.Paths.Any((path) => (path.Head == p && path.Cells[1] == p + Util.FaceToLHVector(f)) || (path.Tail == p && path.Cells[path.Cells.Count - 2] == p + Util.FaceToLHVector(f))))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM);
                    }
                    else
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipeWall", TM);
                }

            List<Vector3Int> PathEnds = new List<Vector3Int>();
            List<DirectionalFeature> PathJoints = new List<DirectionalFeature>();
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
                        if (path.Cells[i + 1] == path.Cells[i] + Util.FaceToLHVector(f) || (path.Cells[i - 1] == path.Cells[i] + Util.FaceToLHVector(f)))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM);
                        else if (PathJoints.Any((joint)=>joint.Position==path.Cells[i]&&Util.LHQToFace(joint.Rotation)==f))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM);
                        else
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipeWall", TM);
                    }
               
            }
            
            
        }
    }
}