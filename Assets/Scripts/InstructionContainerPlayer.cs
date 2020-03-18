using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "InstructionContainerPlayer", menuName = "ScriptableObjects/Manager/InstructionContainerPlayer")]
    public class InstructionContainerPlayer : InstructionContainer
    {


        public override void Init(TaskManager tm, Entity holder, CommandsContainer pC)
        {
            base.Init(tm, holder, pC);
            ChangeActivatedSlots(3);
            AddItem("Inertia", "R");


        }
    }
}