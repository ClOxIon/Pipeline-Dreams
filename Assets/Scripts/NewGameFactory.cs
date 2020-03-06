using UnityEngine;
namespace PipelineDreams {
    public class NewGameFactory : MonoBehaviour {
        [SerializeField] EntityDataContainer EM;
        [SerializeField] CommandsContainer PC;
        [SerializeField] Entity Player;
        [SerializeField] PlayerMove PM;
        [SerializeField] PlayerInitializer PI;
        private void Awake() {
            //mManager.CreateNewMap();
            PC.Init(PM);
            PI.InitPlayer(Player, EM.GetEntityDataFromName("Player"));
        }
    }
}
