
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Instruction
{
    [CreateAssetMenu(fileName = "InstructionContainer", menuName = "ScriptableObjects/Manager/InstructionContainer")]
    public partial class Container : PDObjectContainer<Instruction> {
        
        int CurrentEntryPoint = 0;
        CommandsContainer TargetContainer;
        /// <summary>
        /// This is for AIs.
        /// </summary>
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
            
        }
        public override void PushItem(Instruction item)
        {
            item.Obtain(Holder, TM, EM, TargetContainer);
            objs.Insert(CurrentEntryPoint, item);
            InvokeUIRefresh();
        }
        /// <summary>
        /// This method is used to move an item from a container to somewhere else.
        /// Notice that Remove() is still called.
        /// </summary>
        /// <param name="index"></param>
        public override Instruction PopItem(int index)
        {
            var b = objs[index];
            objs.Remove(b);
            b.Remove();
            if(CurrentEntryPoint>index)
                CurrentEntryPoint--;
            InvokeUIRefresh();
            return b;
        }

    }
}