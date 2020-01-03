using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "InstructionContainer", menuName = "ScriptableObjects/Manager/InstructionContainer")]
    public class InstructionContainerPlayer : PDObjectContainerLimitedCapacity<Instruction> {
        public event Action<int> OnChangeItemSlotAvailability;

        int MaximumInstructionCount = 10;
        int ActivatedSlots;

        public void UseInstructionByName(string name)
        {
            var inst = objs.Find((x) => x.Data.Name == name) as Instruction;
            if (inst != null)
            {
                TM.AddSequentialTask(inst.Operation(TM.Clock));
                Holder.GetComponent<PlayerAI>().EntityClock += inst.TimeCost.Value;
            }
        }


        public override void Init(TaskManager tm, Entity holder) {
            base.Init(tm, holder);
            for (int i = 0; i < MaximumInstructionCount; i++)
                objs.Add(null);
            ChangeActivatedSlots(3);
            AddItem("Inertia", "R");

        }

        public void UseInstructionAt(int i) {
            if (objs.Count > i && objs[i] != null)
                if (objs[i].ReadCommand()) {
                    TM.AddSequentialTask(objs[i].Operation(TM.Clock));
                    Holder.GetComponent<PlayerAI>().EntityClock += objs[i].TimeCost.Value;

                }
        }
        

        public void ChangeActivatedSlots(int after) {
            ActivatedSlots = after;
            if (ActivatedSlots < objs.Count) {

                objs.RemoveRange(after, objs.Count - after);
            }
            OnChangeItemSlotAvailability?.Invoke(after);
            InvokeUIRefresh();
        }
        
    }
}