using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams
{
    public enum VertexAnnotation
    {
        MultiEntranceRoom, Room, Bridge, Door, Deadend

    }
    public struct VertexData
    {
        Vector3Int position;
        int index;

    }
    public abstract class GameStageSetup {
        //Stage generation parameters
        int NumStage;
        int StageGraphMaxDepth;
        /// <summary>
        /// Note that generators can restrict the number of connections.
        /// </summary>
        int NumAverageConnection;
        /// <summary>
        /// Determines the average size of stages. The number of features increases; the size stays the same.
        /// </summary>
        int StageSizeMultiplier;
        int gameSize;

        Graph<MapMetaData> Stages = new Graph<MapMetaData>();
        public void GenerateStages(int seed) {
           // Stages.MakeNewVertex();
        }
    }
    
    public abstract class MapGenerator : ScriptableObject
    {
        public abstract MapFeatData GenerateMap(int seed, float scale = 1);


    }
    
    public abstract class MapRenderer : ScriptableObject
    {
        [SerializeField] protected Entity Station;

        [SerializeField] protected Entity PipePath;

        [SerializeField] protected Entity PipeWall;

        [SerializeField] protected Entity RoomWall;

        [SerializeField] protected Entity RoomEntrance;

        [SerializeField] protected TaskManager TM;

        [SerializeField] protected EntityDataContainer enDataContainer;
        public abstract void RenderMap(MapFeatData data);
        public void Initialize(TaskManager tm) => TM = tm;

    }
    public struct MapMetaData
    {
        int seed;
        MapGenerator generator;
        MapRenderer renderer;
    }
    public class MapFeatData {
        public List<MapFeature> Features = new List<MapFeature>();
        public List<PDMapPath> Paths = new List<PDMapPath>();
    }
    public class MapFeature {
       
        public string Name;

        /// <summary>
        /// When multiple instances of a same feature exists, a unique index is given to each of them. 
        /// </summary>
        public int Index;
        /// <summary>
        /// The position of the feature origin.
        /// </summary>
        public Vector3Int Position;
        /// <summary>
        /// The position of the cells that this feature occupies; relative to the feature origin.
        /// </summary>
        public List<Vector3Int> OccupiedCells = new List<Vector3Int>();
        /// <summary>
        /// The position of entrances to the feature; if not specified, then every occupied point could be an entrance. All specified entrances should NOT be in OccupiedCells, and point toward an OccupiedCell.
        /// Multiple entrances could exist in a cell if they all point to different points.
        /// </summary>
        public List<DirectionalFeature> Entrances = new List<DirectionalFeature>();

        public List<DirectionalFeature> UsedEntrances = new List<DirectionalFeature>();
        public Quaternion Rotation = Quaternion.identity;
    }
    public class DirectionalFeature {
        public Vector3Int Position;
        public Quaternion Rotation;
    }
    public class PDMapPath {
        public Vector3Int Head;
        public Vector3Int Tail;
        /// <summary>
        /// The points where the path branches out
        /// </summary>
        public List<DirectionalFeature> Joints = new List<DirectionalFeature>();
        public List<Vector3Int> Cells = new List<Vector3Int>();
    }
    public class SquareRoom : MapFeature {
        public SquareRoom(int size) : base() {
            Name = "Room";
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++) {
                        OccupiedCells.Add(new Vector3Int(i, j, k));
                    }
        }
    }

    public class DeadendFeature : MapFeature
    {
        public DeadendFeature() : base() {
            Name = "Deadend";
            OccupiedCells.Add(Vector3Int.zero);
            //The entrance to this feature is at -z direction, heading +z direction.
            Entrances.Add(new DirectionalFeature() { Position = new Vector3Int(0,0,-1), Rotation = Util.FaceToLHQ(4)});
        }
    }
}