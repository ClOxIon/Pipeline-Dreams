using UnityEngine;
namespace PipelineDreams {
    public class PlayerInitializer : MonoBehaviour {
        public void InitPlayer(Entity Player, MapDataContainer mManager, EntityData EntityData) {
            Player.Initialize(mManager.GetPlayerSpawnPoint(), Util.FaceToLHQ(Random.Range(0, 6)), EntityData);
        }
    }
}
