using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Map.Feature
{
    /// <summary>
    /// Do not serialize any of the fields of this feature.
    /// </summary>
    [CreateAssetMenu(fileName = "Feature", menuName = "ScriptableObjects/Feature/SquareRoom")]
    public class SquareRoom : Feature {
        int size;
        public override List<Vector3Int> OccupiedCells
        {
            get
            {
                if (size == 0)
                {
                    size = 5;
                    occupiedCells = new List<Vector3Int>();
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                            for (int k = 0; k < size; k++)
                            {
                                occupiedCells.Add(new Vector3Int(i, j, k));
                            }
                }
                return occupiedCells;
            }
        }
    }
}