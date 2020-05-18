namespace PipelineDreams.Instruction
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of ranged instruction task.
        /// </summary>
        protected class BasicRangedTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                var _entity = Op.EM.FindLineOfSightEntityOnAxis(Util.LHQToFace(Op.Holder.IdealRotation), Op.Holder);
                if (_entity != null)
                    Op.Holder.GetComponent<Entity.WeaponHolder>().PerformAttack(_entity, StartClock, 0, (Op.Data as Data).RangeCoef, 0, Accuracy);
            }

        }
    }

}