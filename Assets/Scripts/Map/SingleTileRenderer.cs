using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace PipelineDreams.Map
{
    /// <summary>
    /// Used for debug purpose. Renders a single given tile.
    /// </summary>

    [CreateAssetMenu(fileName = "SingleTileRenderer", menuName = "ScriptableObjects/Renderer/SingleTileRenderer")]
    public class SingleTileRenderer : Renderer
    {
        [SerializeField]string TargetTile;
        /// <summary>
        /// Ignores given data.
        /// </summary>
        /// <param name="data"></param>
        public override void RenderMap(MapFeatData data)
        {

            
            for(int f = 0;f<6;f++)
                enDataContainer.AddEntityInScene(Vector3Int.zero, Util.FaceToLHQ(f), TargetTile, TM,0);


        }
    }
}