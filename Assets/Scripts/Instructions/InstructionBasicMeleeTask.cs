namespace PipelineDreams
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of melee instruction task.
        /// </summary>
        protected class InstructionBasicMeleeTask : InstructionTask {
            protected override void OnRunStart()
            {
                var _entity = Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.Subject.IdealRotation), Op.Subject);
                if (_entity != null && _entity == Op.EM.FindEntityInLine(Util.LHQToFace(Op.Subject.IdealRotation), Op.Subject))
                    Op.Subject.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, Op.OpData.meleeCoef, 0, 0, Accuracy);
            }


        }
    }

}