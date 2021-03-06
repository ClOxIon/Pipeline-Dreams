﻿using UnityEngine;
namespace PipelineDreams {
    public class NewGameFactory : MonoBehaviour {
        [SerializeField] TaskManager TM;
        [SerializeField] Entity.Container EM;
        [SerializeField] Instruction.ContainerPlayer ICP;
        [SerializeField] CommandsContainer PC;
        [SerializeField] Entity.Entity Player;
        [SerializeField] PlayerMove PM;
        [SerializeField] Entity.CommandReader CR;
        [SerializeField] PlayerInitializer PI;
        [SerializeField] Map.Generator MG;
        [SerializeField] Map.Renderer MR;
        private void Awake() {
            //mManager.CreateNewMap();
            TM.Initialize();
            EM.Initialize();
            MR.Initialize(TM);
            MR.RenderMap(MG.GenerateMap(0, 1f));
            PC.Init(PM, CR);
            PI.InitPlayer(Player, EM.GetEntityDataFromName("Player"));

            ICP.Init(TM, Player, PC);
            ConsoleUIInput.AppendText("New Game Started!");
        }
    }
}
