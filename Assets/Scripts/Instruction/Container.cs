using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Instruction {
    [CreateAssetMenu(fileName = "InstructionContainer", menuName = "ScriptableObjects/Manager/InstructionContainer")]
    public class Container : PDObjectContainerLimitedCapacity<Instruction> {
        public event Action<int> OnChangeItemSlotAvailability;
        
        int MaximumInstructionCount = 10;
        int ActivatedSlots;
        CommandsContainer TargetContainer;
        public void UseInstructionByName(string name)
        {
            var inst = objs.Find((x) => x.Data.Name == name) as Instruction;
            if (inst != null)
            {
                TM.AddSequentialTask(inst.Operation(TM.Clock));
                Holder.GetComponent<Entity.AI>().EntityClock += inst.TimeCost.Value;
            }
            else
                Debug.LogWarning($"No Instruction Found!: {name}");
        }


        public virtual void Init(TaskManager tm, Entity.Entity holder, CommandsContainer pC) {
            base.Init(tm, holder);
            TargetContainer = pC;
            for (int i = 0; i < MaximumInstructionCount; i++)
                objs.Add(null);
            

        }
        public override void PushItem(Instruction item)
        {
            item.Obtain(Holder, TM, EM, TargetContainer);
            objs.Add(item);
        }
        public void UseInstructionAt(int i) {
            if (objs.Count > i && objs[i] != null)
                if (objs[i].ReadCommand()) {
                    TM.AddSequentialTask(objs[i].Operation(TM.Clock));
                    Holder.GetComponent<Entity.PlayerAI>().EntityClock += objs[i].TimeCost.Value;

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