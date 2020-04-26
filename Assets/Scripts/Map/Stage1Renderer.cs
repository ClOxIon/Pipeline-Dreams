using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace PipelineDreams.Map
{
    /// <summary>
    /// A simple implementation of maprenderer.
    /// </summary>

    [CreateAssetMenu(fileName = "Stage1Renderer", menuName = "ScriptableObjects/Renderer/Stage1Renderer")]
    public class Stage1Renderer : Renderer
    {
        [SerializeField] List<string> NPCs;
        [SerializeField] int StationCount;
        public override void RenderMap(MapFeatData data)
        {

            List<Vector3Int> entrancePoints = new List<Vector3Int>();
            foreach (var room in data.Features)
            {
                if (room.Name == "Room")
                    foreach (var p in room.OccupiedCells)
                        for (int f = 0; f < 6; f++)
                        {
                            var vf = Vector3Int.RoundToInt(room.Rotation * p + room.Position + Util.FaceToLHVector(f));
                            if (room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation * x.Position + room.Position) == vf))
                            {

                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomEntrance", TM,0);

                            }
                            else if (!room.OccupiedCells.Any((x) => Vector3Int.RoundToInt(room.Rotation * x + room.Position) == vf))
                            {

                                enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomWall", TM,0);

                            }
                        }
                if (room.Name == "Deadend")
                {
                    foreach (var p in room.OccupiedCells)
                        for (int f = 0; f < 6; f++)
                        {
                            var vf = Vector3Int.RoundToInt(room.Rotation * p + room.Position + Util.FaceToLHVector(f));
                            if (room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation * x.Position + room.Position) == vf))
                            {
                                if (IsNPCDeadend(room))
                                    enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomEntrance", TM,0);
                                else if (IsSpawnerDeadend(room))
                                    enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomEntrance", TM,0);
                            }
                            else if (!room.OccupiedCells.Any((x) => Vector3Int.RoundToInt(room.Rotation * x + room.Position) == vf))
                            {
                                if (IsNPCDeadend(room))
                                    enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomWall", TM,0);//We will spawn an NPC in this dead end feature.
                                else if (IsSpawnerDeadend(room))
                                    enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Rotation * p + room.Position), Util.FaceToLHQ(f), "RoomWall", TM,0);
                            }
                        }
                    if (IsNPCDeadend(room))
                        enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Position), room.Rotation, NPCs[room.Index], TM,0);//Dead End Rooms index = 0 to NPCs.length are filled with corresponding NPCs.
                    else if (IsStationDeadend(room))
                        enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Position), room.Rotation, "Station", TM,0);//Dead End Rooms index = 0 to NPCs.length are filled with corresponding NPCs.
                    else if (IsSpawnerDeadend(room))

                        enDataContainer.AddEntityInScene(Vector3Int.RoundToInt(room.Position), room.Rotation, "SpiderSpawner", TM,0);
                }

                foreach (var ent in room.UsedEntrances)
                    if (!entrancePoints.Contains(Vector3Int.RoundToInt(room.Rotation * ent.Position + room.Position)))
                        entrancePoints.Add(Vector3Int.RoundToInt(room.Rotation * ent.Position + room.Position));


            }
            List<Vector3Int> PathEnds = new List<Vector3Int>();
            List<DirectionalFeature> PathJoints = new List<DirectionalFeature>();
            foreach (var path in data.Paths)
            {
                PathEnds.Add(path.Head);
                PathEnds.Add(path.Tail);
                PathJoints.AddRange(path.Joints);
            }
            //Check room entrance points.
            foreach (var p in entrancePoints)
                if(!PathJoints.Any((j)=>j.Position == p))//In this case, the entrance is a part of a path joint. This feature is rendered below, during path render.
                for (int f = 0; f < 6; f++)
                {
                    if (data.Features.Any((room) => room.UsedEntrances.Any((x) => Vector3Int.RoundToInt(room.Rotation * x.Position) + room.Position == p && Util.LHQToFace(x.Rotation * room.Rotation) == f)))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM,0);
                    }
                    else if (data.Paths.Any((path) => (path.Head == p && path.Cells[1] == p + Util.FaceToLHVector(f)) || (path.Tail == p && path.Cells[path.Cells.Count - 2] == p + Util.FaceToLHVector(f))))
                    {
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipePath", TM,0);
                    }
                    else
                        enDataContainer.AddEntityInScene(p, Util.FaceToLHQ(f), "PipeWall", TM,0);
                }

            
            foreach (var path in data.Paths)
            {
                for (int i = 1; i < path.Cells.Count - 1; i++)
                    for (int f = 0; f < 6; f++)
                    {
                        if (path.Cells[i + 1] == path.Cells[i] + Util.FaceToLHVector(f) || (path.Cells[i - 1] == path.Cells[i] + Util.FaceToLHVector(f)))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM,0);
                        else if (PathJoints.Any((joint) => joint.Position == path.Cells[i] && Util.LHQToFace(joint.Rotation) == f))
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipePath", TM,0);
                        else
                            enDataContainer.AddEntityInScene(path.Cells[i], Util.FaceToLHQ(f), "PipeWall", TM,0);
                    }

            }


        }
        bool IsNPCDeadend(MapFeature deadend) {
            return deadend.Index < NPCs.Count;
        }
        bool IsStationDeadend(MapFeature deadend)
        {
            return deadend.Index >= NPCs.Count && deadend.Index < NPCs.Count + StationCount;
        }
        bool IsSpawnerDeadend(MapFeature deadend)
        {
            return deadend.Index >= NPCs.Count + StationCount;
        }

    }
}