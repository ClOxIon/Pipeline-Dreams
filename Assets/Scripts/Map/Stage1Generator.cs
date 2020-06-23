using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

namespace PipelineDreams.Map
{
    using Feature;
    /// <summary>
    /// A simple implementation of mapgenerator.
    /// </summary>

    [CreateAssetMenu(fileName = "Stage1Generator", menuName = "ScriptableObjects/Generator/Stage1Generator")]

    public class Stage1Generator : Generator
    {
        [SerializeField] int pathSimplicity;
        [SerializeField] int pathLinearity;
        [SerializeField] Feature.Feature DeadendFeature;
        [SerializeField] Feature.Feature GeneratorRoom;
        [SerializeField] Feature.Feature SquareRoom;
        public override MapFeatData GenerateMap(int seed, float scale = 1) {

            var tpData = new MapFeatData();
            //The generator room works as a base. Players spend most time around it.
            var generatorRoom = Instantiate(GeneratorRoom);
            for(int f = 0; f<6;f++)
                generatorRoom.Entrances.Add(new Z3Q() { Position = Util.FaceToUVector(f)*5+4*Vector3Int.one, Rotation = Util.FaceToLHQ(Util.FaceFlip(f)) });//Entrance to the generator room.
            generatorRoom.Position = new Vector3Int(-4, -4, -4);
            tpData.Features.Add(generatorRoom);

            var room2 = Instantiate(SquareRoom);
            room2.Position = new Vector3Int((int)(scale * 10), (int)(scale * 10), (int)(scale * 10));
            var room3 = Instantiate(SquareRoom);
            room3.Position = new Vector3Int(-(int)(scale * 10), -(int)(scale * 10), -(int)(scale * 10));
            var room4 = Instantiate(SquareRoom);
            room4.Position = new Vector3Int((int)(scale * 10), -(int)(scale * 10), -(int)(scale * 10));
            var room5 = Instantiate(SquareRoom);
            room5.Position = new Vector3Int((int)(scale * 10), (int)(scale * 10), -(int)(scale * 10));

            tpData.Features.Add(room2);

            tpData.Features.Add(room3);

            tpData.Features.Add(room4);

            tpData.Features.Add(room5);
            var deadend1 = Instantiate(DeadendFeature);
            deadend1.Index = 0;
            deadend1.Position = new Vector3Int(0, 0, -(int)(scale * 15));
            deadend1.Rotation = Util.FaceToLHQ(5);
            tpData.Features.Add(deadend1);
            var deadend2 = Instantiate(DeadendFeature);
            deadend2.Index = 1;
            deadend2.Position = new Vector3Int(0, 0, (int)(scale * 15));
            deadend2.Rotation = Util.FaceToLHQ(4);
            tpData.Features.Add(deadend2);
            var deadend3 = Instantiate(DeadendFeature);
            deadend3.Index = 2;
            deadend3.Position = new Vector3Int(0, -(int)(scale * 15), 0);
            deadend3.Rotation = Util.FaceToLHQ(3);
            tpData.Features.Add(deadend3);
            var deadend4 = Instantiate(DeadendFeature);
            deadend4.Index = 3;
            deadend4.Position = new Vector3Int(0, (int)(scale * 15), 0);
            deadend4.Rotation = Util.FaceToLHQ(2);
            tpData.Features.Add(deadend4);
            //TODO: Replace with seeded PRNG
            tpData.Paths = GeneratePaths(new List<Feature.Feature>(), new List<Feature.Feature> { generatorRoom, room2, room3,room4, room5, deadend1, deadend2, deadend3, deadend4 }, pathSimplicity, pathLinearity, 1, 3, () => UnityEngine.Random.value);
            LastGenData = tpData;

            return tpData;
        }
    }
}