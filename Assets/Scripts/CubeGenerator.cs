using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
namespace PipelineDreams
{
    /// <summary>
    /// A simple implementation of mapgenerator.
    /// </summary>

    [CreateAssetMenu(fileName = "CubeGenerator", menuName = "ScriptableObjects/Generator/CubeGenerator")]

    public class CubeGenerator : MapGenerator
    {
        [SerializeField] int pathSimplicity;
        [SerializeField] int pathLinearity;
        public override MapFeatData GenerateMap(int seed, float scale = 1) {

            var tpData = new MapFeatData();
            var room = new SquareRoom((int)(scale * 20));

            tpData.Features.Add(room);
            var room2 = new SquareRoom((int)(scale * 5));
            room2.Position = new Vector3Int((int)(scale * 20), (int)(scale * 20), (int)(scale * 20));
            var room3 = new SquareRoom((int)(scale * 5));
            room3.Position = new Vector3Int(-(int)(scale * 20), -(int)(scale * 20), -(int)(scale * 20));

            tpData.Features.Add(room2);

            tpData.Features.Add(room3);
            var deadend1 = new DeadendFeature();
            deadend1.Index = 0;
            deadend1.Position = new Vector3Int(0, 0, -(int)(scale * 25));
            deadend1.Rotation = Util.FaceToLHQ(5);
            tpData.Features.Add(deadend1);
            var deadend2 = new DeadendFeature();
            deadend2.Index = 1;
            deadend2.Position = new Vector3Int(0, 0, (int)(scale * 25));
            deadend2.Rotation = Util.FaceToLHQ(4);
            tpData.Features.Add(deadend2);

            tpData.Paths = GeneratePaths(new List<MapFeature>(), new List<MapFeature> { room, room2, room3, deadend1, deadend2 }, pathSimplicity, pathLinearity, 1, 3, () => UnityEngine.Random.value);

            LastGenData = tpData;
            return tpData;
        }
    }
}