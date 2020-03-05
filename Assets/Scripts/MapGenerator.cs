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
            Stages.MakeNewVertex();
        }
    }
    public abstract class MapGenerator
    {
        public abstract MapFeatData GenerateMap(int seed, int scale = 1);


    }
    public class CubeGenerator : MapGenerator
    {
        public override MapFeatData GenerateMap(int seed, int scale = 1) {
            var tpData = new MapFeatData();
            var room = new SquareRoom(scale * 5);
            
            tpData.Features.Add(room);

            var deadend1 = new DeadendFeature();
            deadend1.Index = 0;
            deadend1.Position = new Vector3Int(-1, 0, 0);
            deadend1.Rotation = Quaternion.Euler(0, 0, 180);
            tpData.Features.Add(deadend1);
            var deadend2 = new DeadendFeature();
            deadend2.Index = 1;
            deadend2.Position = new Vector3Int(scale*5, 0, 0);
            tpData.Features.Add(deadend2);
            return tpData;
        }
    }
    public abstract class MapRenderer
    {
        public abstract void RenderMap(MapFeatData data);


    }
    public class CubeRenderer : MapRenderer
    {
        public override void RenderMap(MapFeatData data) {
            
        }
    }
    public struct MapMetaData
    {
        int seed;
        MapGenerator generator;
        MapRenderer renderer;
    }
    public class MapFeatData {
        public List<MapFeature> Features = new List<MapFeature>();
        public List<List<Vector3Int>> Paths = new List<List<Vector3Int>>();
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
        public Quaternion Rotation;
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
        }
    }
}