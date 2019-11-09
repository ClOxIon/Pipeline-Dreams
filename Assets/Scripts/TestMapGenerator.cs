using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapGenerator : MapGenerator
{
    public override MapBundle CreateMap() {
        var m = new MapBundle {
            v = new MapBoxel[1,1,1]

        };
        

        Debug.Log("Mapsize : " + m.v.GetLength(0) + "x" + m.v.GetLength(1) + "x" + m.v.GetLength(2));
        
        for (int i = 0; i < m.v.GetLength(0); i++)
            for (int j = 0; j < m.v.GetLength(1); j++)
                for (int k = 0; k < m.v.GetLength(2); k++) {
                    m.v[i, j, k].b = Block.pipe;
                    m.v[i, j, k].t = new Tile[6];
                }
        

        GenerateTrivialTopology(m);
        m.v[0, 0, 0].t[1] = Tile.vendingMachine;
        return m;

    }
}
