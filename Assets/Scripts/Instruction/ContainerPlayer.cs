using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Instruction {
    [CreateAssetMenu(fileName = "InstructionContainerPlayer", menuName = "ScriptableObjects/Manager/InstructionContainerPlayer")]
    public class ContainerPlayer : Container
    {


        public override void Init(TaskManager tm, Entity.Entity holder, CommandsContainer pC)
        {
            base.Init(tm, holder, pC);
            ChangeActivatedSlots(3);
            AddItem("Inertia", "R");


        }
    }
}