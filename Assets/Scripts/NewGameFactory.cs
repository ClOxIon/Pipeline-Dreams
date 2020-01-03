using UnityEngine;
namespace PipelineDreams {
    public class NewGameFactory : MonoBehaviour {
        [SerializeField] EntityDataContainer EM;
        [SerializeField] MapDataContainer mManager;
        [SerializeField] CommandsContainer PC;
        [SerializeField] TileContainer SC;
        [SerializeField] Entity Player;
        [SerializeField] PlayerMove PM;
        [SerializeField] PlayerInitializer PI;
        private void Awake() {
            mManager.CreateNewMap();
            PC.Init(PM);
            SC.Init(mManager);
            PI.InitPlayer(Player, mManager, EM.GetEntityDataFromName("Player"));
        }
    }
}
