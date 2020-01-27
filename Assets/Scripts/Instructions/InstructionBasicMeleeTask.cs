namespace PipelineDreams
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of melee instruction task.
        /// </summary>
        protected class InstructionBasicMeleeTask : InstructionTask {
            protected override void OnRunStart()
            {
                var _entity = Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.Holder.IdealRotation), Op.Holder);
                if (_entity != null && _entity == Op.EM.FindEntityOnAxis(Util.LHQToFace(Op.Holder.IdealRotation), Op.Holder))
                    Op.Holder.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, (Op.Data as InstructionData).MeleeCoef, 0, 0, Accuracy);
            }


        }
    }

}