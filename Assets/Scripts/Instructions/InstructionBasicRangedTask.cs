namespace PipelineDreams
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of ranged instruction task.
        /// </summary>
        protected class InstructionBasicRangedTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                var _entity = Op.EM.FindEntityOnAxis(Util.LHQToFace(Op.Holder.IdealRotation), Op.Holder);
                if (_entity != null)
                    Op.Holder.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, (Op.Data as InstructionData).RangeCoef, 0, Accuracy);
            }

        }
    }

}