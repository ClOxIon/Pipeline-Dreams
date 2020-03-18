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

    [CreateAssetMenu(fileName = "Stage1Generator", menuName = "ScriptableObjects/Generator/Stage1Generator")]

    public class Stage1Generator : MapGenerator
    {
        [SerializeField] int pathSimplicity;
        [SerializeField] int pathLinearity;
        public override MapFeatData GenerateMap(int seed, float scale = 1) {

            var tpData = new MapFeatData();
            var spawningroom = new SquareRoom(1);
            spawningroom.Entrances.Add(new DirectionalFeature() { Position = Util.FaceToLHVector(0), Rotation = Util.FaceToLHQ(1) });//Entrance to the spawning room.
            tpData.Features.Add(spawningroom);
            var room2 = new SquareRoom((int)(scale * 5));
            room2.Position = new Vector3Int((int)(scale * 10), (int)(scale * 10), (int)(scale * 10));
            var room3 = new SquareRoom((int)(scale * 5));
            room3.Position = new Vector3Int(-(int)(scale * 10), -(int)(scale * 10), -(int)(scale * 10));
            var room4 = new SquareRoom((int)(scale * 5));
            room4.Position = new Vector3Int((int)(scale * 10), -(int)(scale * 10), -(int)(scale * 10));
            var room5 = new SquareRoom((int)(scale * 5));
            room5.Position = new Vector3Int((int)(scale * 10), (int)(scale * 10), -(int)(scale * 10));

            tpData.Features.Add(room2);

            tpData.Features.Add(room3);

            tpData.Features.Add(room4);

            tpData.Features.Add(room5);
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
            //TODO: Replace with seeded PRNG
            tpData.Paths = GeneratePaths(new List<MapFeature>(), new List<MapFeature> { spawningroom, room2, room3,room4, room5, deadend1, deadend2 }, pathSimplicity, pathLinearity, 1, 3, () => UnityEngine.Random.value);
            LastGenData = tpData;

            return tpData;
        }
    }
}