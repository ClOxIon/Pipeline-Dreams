using UnityEngine;
namespace PipelineDreams {
    public class NewGameFactory : MonoBehaviour {
        [SerializeField] EntityDataContainer EM;
        [SerializeField] MapDataContainer mManager;
        [SerializeField] ItemContainer itemC;
        [SerializeField] InstructionContainer instC;
        [SerializeField] CommandsContainer PC;
        [SerializeField] TileContainer SC;
        [SerializeField] TaskManager TM;
        [SerializeField] Entity Player;
        [SerializeField] PlayerMove PM;
        [SerializeField] PlayerInitializer PI;
        private void Awake() {
            mManager.CreateNewMap();
            itemC.Init(TM, Player);
            instC.Init(TM, Player);
            PC.Init(PM);
            SC.Init(mManager);
            PI.InitPlayer(Player, mManager, EM.GetEntityDataFromName("Player"));
        }
    }
}
