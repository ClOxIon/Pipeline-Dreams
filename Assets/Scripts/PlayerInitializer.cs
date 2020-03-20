using UnityEngine;
namespace PipelineDreams {
    public class PlayerInitializer : MonoBehaviour {
        [SerializeField] TaskManager tm;
        [SerializeField] Entity.Container ec;
        public void InitPlayer(Entity.Entity Player, Entity.Data EntityData) {
            //TODO : Specify the spawning point!
            Player.Initialize(Vector3Int.zero, Util.FaceToLHQ(Random.Range(0, 6)), EntityData, tm, ec);
            ec.AddPlayer(Player);
        }
    }
}
