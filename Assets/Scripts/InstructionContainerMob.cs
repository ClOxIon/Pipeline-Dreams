using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "InstructionContainerMob", menuName = "ScriptableObjects/Manager/InstructionContainerMob")]
    public class InstructionContainerMob : InstructionContainer
    {
        [SerializeField] List<string> AbilitySet;

        public override void Init(TaskManager tm, Entity holder)
        {
            base.Init(tm, holder);
            ChangeActivatedSlots(AbilitySet.Count);
            foreach (var x in AbilitySet)
                AddItem(x);


        }
    }
}