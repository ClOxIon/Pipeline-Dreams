using UnityEngine;
namespace PipelineDreams {
    public class NewGameFactory : MonoBehaviour {
        [SerializeField] TaskManager TM;
        [SerializeField] EntityDataContainer EM;
        [SerializeField] InstructionContainerPlayer ICP;
        [SerializeField] CommandsContainer PC;
        [SerializeField] Entity Player;
        [SerializeField] PlayerMove PM;
        [SerializeField] PlayerInitializer PI;
        [SerializeField] MapGenerator MG;
        [SerializeField] MapRenderer MR;
        private void Awake() {
            //mManager.CreateNewMap();
            EM.Initialize();
            MR.Initialize(TM);
            MR.RenderMap(MG.GenerateMap(0, 0.2f));
            PC.Init(PM);
            PI.InitPlayer(Player, EM.GetEntityDataFromName("Player"));

            ICP.Init(TM, Player, PC);
        }
    }
}
