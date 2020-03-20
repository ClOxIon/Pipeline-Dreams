using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Instruction {
    [CreateAssetMenu(fileName = "InstructionContainerMob", menuName = "ScriptableObjects/Manager/InstructionContainerMob")]
    public class ContainerMob : Container
    {
        [SerializeField] List<string> AbilitySet;

        public override void Init(TaskManager tm, Entity.Entity holder)
        {
            base.Init(tm, holder);
            ChangeActivatedSlots(AbilitySet.Count);
            foreach (var x in AbilitySet)
                AddItem(x);


        }
    }
}