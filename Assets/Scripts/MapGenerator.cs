using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MapGenerator
{
    const int margin = 3;
    public virtual MapBundle CreateMap() {
        var m = new MapBundle {
            v = new MapBoxel[UnityEngine.Random.Range(6+2*margin, 10 + 2 * margin), UnityEngine.Random.Range(6 + 2 * margin, 10 + 2 * margin), UnityEngine.Random.Range(6 + 2 * margin, 10 + 2 * margin)]

        };
        var scale = m.v.GetLength(0) + m.v.GetLength(1) + m.v.GetLength(2)- 6 * margin;
        Vector3Int cur;
        List<Vector3Int> Path;
        Debug.Log("Mapsize : "+ m.v.GetLength(0)+"x"+ m.v.GetLength(1) + "x" + m.v.GetLength(2));
        do {
            cur = new Vector3Int(UnityEngine.Random.Range(margin, m.v.GetLength(0) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(1) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(2) - margin));
            Path = Wilson(m,cur,new List<Vector3Int> { cur });
            Path.RemoveAt(Path.Count - 1);
            Debug.Log("Try Wilson Algorithm : "+Path.Count+" Until Path Count is : "+scale);
        } while (Path.Count > 3 * scale || Path.Count < scale);

        for (int i = 0; i < m.v.GetLength(0); i++)
            for (int j = 0; j < m.v.GetLength(1); j++)
                for (int k = 0; k < m.v.GetLength(2); k++) {
                    m.v[i, j, k].b = Block.nothing;
                    m.v[i, j, k].t = new Tile[6];
                }
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
    /// 
    /// </summary>
    /// <param name="m"></param>
    /// <param name="cur"></param>
    /// <param name="Mother"></param>
    /// <returns>The entire path. The last element is either the first element if the path is cyclic, or the element of the motehr path which the path is connected to.</returns>
    List<Vector3Int> Wilson(MapBundle m, Vector3Int cur, List<Vector3Int> Mother) {

        List<Vector3Int> Path = new List<Vector3Int>() { cur };
        
        while (true) {
            Vector3Int d;
            do {
                d = Util.FaceToLHVector(UnityEngine.Random.Range(0, 6));
            }
            while (IsOutofRange(m, cur + d));

            var p = cur + d;

            if (Mother.Contains(p)) {
                Path.Add(p);
                break;
            }
            if (Path.Contains(p)) {
                var i = Path.IndexOf(p);
                Path.RemoveRange(i, Path.Count - i);
            }
            
            cur = p;
            Path.Add(cur);
        }
        return Path;
    }
    protected void GenerateCyclePathTopology(MapBundle m, List<Vector3Int> Path) {
        var p0 = Path[0];
        var f01 = Util.LHUnitVectorToFace(Path[Path.Count - 1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f01] = Tile.hole;
        var f02 = Util.LHUnitVectorToFace(Path[1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f02] = Tile.hole;
        for (int i = 1; i < Path.Count - 1; i++) {
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
    protected List<Vector3Int> GenerateRandomBranch(MapBundle m, List<Vector3Int> Mother) {
        List<Vector3Int> Path;
        Vector3Int cur;
        var scale = m.v.GetLength(0) + m.v.GetLength(1) + m.v.GetLength(2) - 6 * margin;
        do {
            do {
                cur = new Vector3Int(UnityEngine.Random.Range(margin, m.v.GetLength(0) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(1) - margin), UnityEngine.Random.Range(margin, m.v.GetLength(2) - margin));
            } while (!Mother.Contains(cur));
            Path = Wilson(m, cur, Mother);
            for (int i = Path.Count - 2; i >= 0; i--) {
                var p = Path[i];
                if (Util.CompareBlocks(m.v[p.x, p.y, p.z].b,Block.pipe)) { Path = Path.GetRange(i+1, Path.Count - i-1); break; }
                m.v[p.x, p.y, p.z].b = Block.pipe;
            }
            Debug.Log("GenerateRandomBrange : Try Wilson Algorithm : " + Path.Count + " Until Path Count is : " + scale);
        } while (Path.Count > scale || Path.Count <= 1);
        GenerateTrivialTopology(m, Path.GetRange(0,Path.Count-1));
        var p0 = Path[0];
        var f02 = Util.LHUnitVectorToFace(Path[1] - Path[0]);
        m.v[p0.x, p0.y, p0.z].t[f02] = Tile.hole;
        m.v[p0.x, p0.y, p0.z].t[2*(f02/2)+1-f02%2] = Tile.vendingMachine;

        for (int i = 1; i < Path.Count - 1; i++) {
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
    protected virtual void GenerateTrivialTopology(MapBundle m) {
        for (int i = 0; i < m.v.GetLength(0); i++)
            for (int j = 0; j < m.v.GetLength(1); j++)
                for (int k = 0; k < m.v.GetLength(2); k++)
                    if (m.v[i, j, k].b == Block.nothing)
                        for (int f = 0; f < 6; f++)
                            m.v[i, j, k].t[f] = Tile.nothing;
                    else {
                        for (int f = 0; f < 6; f++) {
                           
                            m.v[i, j, k].t[f] = Tile.wall;
                        }
                    }
    }
    protected virtual void GenerateTrivialTopology(MapBundle m, List<Vector3Int> domain) {
        foreach (var x in domain)
            if (m.v[x.x, x.y, x.z].b == Block.nothing)
                for (int f = 0; f < 6; f++)
                    m.v[x.x, x.y, x.z].t[f] = Tile.nothing;
            else {
                for (int f = 0; f < 6; f++) {

                    m.v[x.x, x.y, x.z].t[f] = Tile.wall;
                }
            }
                    
    }
    protected void CreateLowerStation(MapBundle m, List<Vector3Int> Path) {
        Vector3Int infPoint = Path[0];
        foreach (var x in Path)
            if (x.y < infPoint.y)
                infPoint = x;
        m.v[infPoint.x, infPoint.y, infPoint.z].t[3] = Tile.hole;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].b = Block.pipe;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].t[3] = Tile.station;
        m.v[infPoint.x, infPoint.y - 1, infPoint.z].t[2] = Tile.stationHole;
    }
    protected void CreateUpperStation(MapBundle m, List<Vector3Int> Path) {
        Vector3Int supPoint = Path[0];
        foreach (var x in Path)
            if (x.y > supPoint.y)
                supPoint = x;
        m.v[supPoint.x, supPoint.y, supPoint.z].t[2] = Tile.hole;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].b = Block.pipe;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].t[2] = Tile.station;
        m.v[supPoint.x, supPoint.y + 1, supPoint.z].t[3] = Tile.stationHole;
    }
    protected bool IsOutofRange(MapBundle m, int i, int j, int k) {

        return !(m.v.GetLength(0) - margin > i && margin <= i && m.v.GetLength(1) - margin > j && margin <= j && m.v.GetLength(2) - margin > k && margin <= k);
    }
    protected bool IsOutofRange(MapBundle m, Vector3Int v) {
        return IsOutofRange(m, v.x, v.y, v.z);

    }
    
}
