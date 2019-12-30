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
                var _entity = Op.EM.FindEntityInLine(Util.LHQToFace(Op.Subject.IdealRotation), Op.Subject);
                if (_entity != null)
                    Op.Subject.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, Op.OpData.rangeCoef, 0, Accuracy);
            }

        }
    }

}