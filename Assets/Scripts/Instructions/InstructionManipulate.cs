namespace PipelineDreams
{
    public class InstructionManipulate : Instruction {
        public InstructionManipulate(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) : base(eM, player, pC, data, variant) {
        }

        public override IClockTask Operation(float startClock)
        {
            return new DirectionalFieldInstructionTask(){ Op = this, StartClock = startClock, Priority = Priority.PLAYER, EffectDuration = OpData.effectDuration };
        }
    }
    public abstract partial class Instruction {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class DirectionalFieldInstructionTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                var _entity = Op.EM.FindEntityInLine(Util.LHQToFace(Op.Subject.IdealRotation), Op.Subject);
                if (_entity != null)
                    Op.Subject.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, 0, Op.OpData.fieldCoef, Accuracy);
                Op.Subject.GetComponent<EntityBuff>().AddItem("InstManipulate");
            }
        }
    }
}