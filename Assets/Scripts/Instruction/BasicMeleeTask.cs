﻿using System.Linq;
using UnityEngine;

namespace PipelineDreams.Instruction
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of melee instruction task.
        /// </summary>
        protected class BasicMeleeTask : InstructionTask {
            protected override void OnRunStart()
            {
                ///Attack priority is given by OrderBy.
                var _entity = Op.EM.FindEntities((x)=>x.IdealPosition == Util.LHQToLHUnitVector(Op.Holder.IdealRotation) + Op.Holder.IdealPosition).OrderBy((x)=>x.Data.Type).FirstOrDefault();
                if (_entity != null)
                    Op.Holder.GetComponent<Entity.WeaponHolder>().PerformAttack(_entity, StartClock, (Op.Data as Data).MeleeCoef, 0, 0, Accuracy);
                else
                    Debug.LogWarning("InstructionTask target entity is null!");
            }


        }
    }

}