using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MapGenerator
{
    const int margin = 3;
    public virtual MapBundle CreateMap()
    {
        //Initialize the voxel array and find the pipe scale
        var m = new MapBundle
        {
            v = new MapBoxel[UnityEngine.Random.Range(6 + 2 * margin, 10 + 2 * margin), UnityEngine.Random.Range(6 + 2 * margin, 10 + 2 * margin), UnityEngine.Random.Range(6 + 2 * margin, 10 + 2 * margin)]
        }; //Initialize a 3D voxel array of a random size, FIX SEEDING
        //MARGIN IS NOT THE MARGIN, IT'S THE PIPE SPACING
        var scale = m.v.GetLength(0) + m.v.GetLength(1) + m.v.GetLength(2) - 6 * margin;
        //SCALE IS NOT SCALE, IT'S THE MINIMUM LOOP SIZE
        Debug.Log("Mapsize : " + m.v.GetLength(0) + "x" + m.v.GetLength(1) + "x" + m.v.GetLength(2));


        //Using the wilsion alogithm we genorate a series of non-intesection loops, but store only the last one
        Vector3Int cur; //The last connection node for two paths
        List<Vector3Int> Path; //The most recent path output by the wilson alg.
        do
        {
            cur = new Vector3Int(UnityEngine.Random.Range(margin, m.v.GetLength(0) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(1) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(2) - margin)); //The entrance is set anywhere in the map???
            Path = Wilson(m, cur, new List<Vector3Int> { cur }); //Start wilisons with a mother path of only the random starting node
                                                                 //Should generate a loop starting and ending at random node 
            Path.RemoveAt(Path.Count - 1); //Remove the node which interseted the current node
            Debug.Log("Try Wilson Algorithm : " + Path.Count + " Until Path Count is : " + scale);
        } while (Path.Count > 3 * scale || Path.Count < scale); //Check is the loop size is as large as scale which is minimum loop size.

        //Once the path is initialized, intialize all voxels as empty
        for (int i = 0; i < m.v.GetLength(0); i++)
            for (int j = 0; j < m.v.GetLength(1); j++)
                for (int k = 0; k < m.v.GetLength(2); k++)
                {
                    m.v[i, j, k].b = Block.nothing;
                    m.v[i, j, k].t = new Tile[6]; //Figure out what the tile obj is supposed to do???
                }
        //Then mark the ones on the path as full
        foreach (var v in Path)
            m.v[v.x, v.y, v.z].b = Block.pipe;


        GenerateTrivialTopology(m);
        GenerateCyclePathTopology(m, Path);
        CreateLowerStation(m, Path);
        CreateUpperStation(m, Path);
        GenerateRandomBranch(m, Path);
        GenerateRandomBranch(m, Path);
        GenerateRandomBranch(m, Path);
        return m;

    }
    /// <summary>
    /// Implimentation of Wilsons Algorithm
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="cur">The first element of the path</param>
    /// <param name="Mother">The path which the new path spawned from</param>
    /// <returns>The entire path. The last element is either the first element if the path is cyclic, or the element of the mother path which the path is connected to.</returns>
    List<Vector3Int> Wilson(MapBundle m, Vector3Int cur, List<Vector3Int> Mother)
    {

        List<Vector3Int> Path = new List<Vector3Int>() { cur }; //Starting point for new path

        while (true)
        {
            Vector3Int d; //Direction of most recent  step
            do
            {
                d = Util.FaceToLHVector(UnityEngine.Random.Range(0, 6)); //Choose random cardinal direction
            }
            while (IsOutofRange(m, cur + d)); //Check where a step along direction exists limits of map

            var p = cur + d; //position of next node

            if (Mother.Contains(p)) //It seems like this should be checking the path that spawn this loop,
                                    //but this always set to a list contain only { cur }???
            {
                Path.Add(p); //If you contact the mother path add this node, so you know where you contacted the mother
                break; //and end the loop
            }
            if (Path.Contains(p)) //Checks when we hit the current path
            {
                var i = Path.IndexOf(p); //Find the node we hit
                Path.RemoveRange(i, Path.Count - i); //Remove all node between the node we and current
            }
            //If we didn't hit anything simiply add the node to the path
            //and move the current node to the new node
            cur = p;
            Path.Add(cur);
        }
        return Path;
    }
    /// <summary>
    /// function fills 
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="Path">The path which the new path spawned from</param>
    protected void GenerateCyclePathTopology(MapBundle m, List<Vector3Int> Path)
    {
        var p0 = Path[0];
        var f01 = Util.LHUnitVectorToFace(Path[Path.Count - 1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f01] = Tile.hole;
        var f02 = Util.LHUnitVectorToFace(Path[1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f02] = Tile.hole;
        for (int i = 1; i < Path.Count - 1; i++)
        {
            var p = Path[i];
            var f1 = Util.LHUnitVectorToFace(Path[i - 1] - Path[i]);
            m.v[p.x, p.y, p.z].t[f1] = Tile.hole;
            var f2 = Util.LHUnitVectorToFace(Path[i + 1] - Path[i]);
            m.v[p.x, p.y, p.z].t[f2] = Tile.hole;

        }
        var pf = Path[Path.Count - 1];
        var ff1 = Util.LHUnitVectorToFace(Path[Path.Count - 2] - Path[Path.Count - 1]);
        m.v[pf.x, pf.y, pf.z].t[ff1] = Tile.hole;
        var ff2 = Util.LHUnitVectorToFace(Path[0] - Path[Path.Count - 1]);
        m.v[pf.x, pf.y, pf.z].t[ff2] = Tile.hole;
    }
    /// <summary>
    /// FILLL IN
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="Mother">The path which the new path spawned from</param>
    /// <returns>FILL IN</returns>
    protected List<Vector3Int> GenerateRandomBranch(MapBundle m, List<Vector3Int> Mother)
    {
        List<Vector3Int> Path;
        Vector3Int cur;
        var scale = m.v.GetLength(0) + m.v.GetLength(1) + m.v.GetLength(2) - 6 * margin;
        do
        {
            do
            {
                cur = new Vector3Int(UnityEngine.Random.Range(margin, m.v.GetLength(0) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(1) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(2) - margin));
            } while (!Mother.Contains(cur));
            Path = Wilson(m, cur, Mother);
            for (int i = Path.Count - 2; i >= 0; i--)
            {
                var p = Path[i];
                if (Util.CompareBlocks(m.v[p.x, p.y, p.z].b, Block.pipe)) { Path = Path.GetRange(i + 1, Path.Count - i - 1); break; }
                m.v[p.x, p.y, p.z].b = Block.pipe;
            }
            Debug.Log("GenerateRandomBrange : Try Wilson Algorithm : " + Path.Count + " Until Path Count is : " + scale);
        } while (Path.Count > scale || Path.Count <= 1);
        GenerateTrivialTopology(m, Path.GetRange(0, Path.Count - 1));
        var p0 = Path[0];
        var f02 = Util.LHUnitVectorToFace(Path[1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f02] = Tile.hole;
        m.v[p0.x, p0.y, p0.z].t[2 * (f02 / 2) + 1 - f02 % 2] = Tile.vendingMachine;

        for (int i = 1; i < Path.Count - 1; i++)
        {
            var p = Path[i];
            var f1 = Util.LHUnitVectorToFace(Path[i - 1] - Path[i]);
            m.v[p.x, p.y, p.z].t[f1] = Tile.hole;
            var f2 = Util.LHUnitVectorToFace(Path[i + 1] - Path[i]);
            m.v[p.x, p.y, p.z].t[f2] = Tile.hole;

        }
        var pf = Path[Path.Count - 1];
        var ff1 = Util.LHUnitVectorToFace(Path[Path.Count - 2] - Path[Path.Count - 1]);
        m.v[pf.x, pf.y, pf.z].t[ff1] = Tile.hole;
        return Path;
    }
    /// <summary>
    /// Function fills all nodes with default tile info
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    protected virtual void GenerateTrivialTopology(MapBundle m)
    {
        for (int i = 0; i < m.v.GetLength(0); i++)
            for (int j = 0; j < m.v.GetLength(1); j++)
                for (int k = 0; k < m.v.GetLength(2); k++)
                    if (m.v[i, j, k].b == Block.nothing)
                        for (int f = 0; f < 6; f++)
                            m.v[i, j, k].t[f] = Tile.nothing;
                    else
                    {
                        for (int f = 0; f < 6; f++)
                        {

                            m.v[i, j, k].t[f] = Tile.wall;
                        }
                    }
    }
    /// <summary>
    /// Function fills domain nodes with default tile info
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="domain">The voxels which need tile info provided</param>
    protected virtual void GenerateTrivialTopology(MapBundle m, List<Vector3Int> domain)
    {
        foreach (var x in domain)
            if (m.v[x.x, x.y, x.z].b == Block.nothing)
                for (int f = 0; f < 6; f++)
                    m.v[x.x, x.y, x.z].t[f] = Tile.nothing;
            else
            {
                for (int f = 0; f < 6; f++)
                {

                    m.v[x.x, x.y, x.z].t[f] = Tile.wall;
                }
            }

    }
    /// <summary>
    /// Place a lower entrance on the bottom of the lowest node in the map
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="Path">Full list of pipe nodes</param>
    protected void CreateLowerStation(MapBundle m, List<Vector3Int> Path)
    {
        Vector3Int infPoint = Path[0];
        foreach (var x in Path)
            if (x.y < infPoint.y)
                infPoint = x;
        m.v[infPoint.x, infPoint.y, infPoint.z].t[3] = Tile.hole;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].b = Block.pipe;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].t[3] = Tile.station;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].t[2] = Tile.stationHole;
    }
    /// <summary>
    /// Place a upper entrace on the highest node in the map
    /// </summary>
    /// <param name="m">A 3D-Voxel array of occupancy</param>
    /// <param name="Path">The path which the new path spawned from</param>
    protected void CreateUpperStation(MapBundle m, List<Vector3Int> Path)
    {
        Vector3Int supPoint = Path[0];
        foreach (var x in Path)
            if (x.y > supPoint.y)
                supPoint = x;
        m.v[supPoint.x, supPoint.y, supPoint.z].t[2] = Tile.hole;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].b = Block.pipe;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].t[2] = Tile.station;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].t[3] = Tile.stationHole;
    }


    //Short utility functions
    static protected bool IsOutofRange(MapBundle m, int i, int j, int k) //Checks weather this voxel position is inside the map
    {

        return !(m.v.GetLength(0) - margin > i && margin <= i && m.v.GetLength(1) - margin > j && margin <= j && m.v.GetLength(2) - margin > k && margin <= k);
    }
    static protected bool IsOutofRange(MapBundle m, Vector3Int v) //Checks weather this voxel position is inside the map
    {
        return IsOutofRange(m, v.x, v.y, v.z);

    }

}
