namespace PipelineDreams
{
    public class InstructionManipulate : Instruction {
        

        public override IClockTask Operation(float startClock)
        {
            return PassParam(new DirectionalFieldInstructionTask(), startClock);
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
                var _entity = Op.EM.FindEntityInLine(Util.LHQToFace(Op.Holder.IdealRotation), Op.Holder);
                if (_entity != null)
                    Op.Holder.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, 0, (Op.Data as InstructionData).FieldCoef, Accuracy);
                var b = Op.Holder.GetComponent<EntityBuff>();
                if(b!=null)
                b.BuffContainer.AddItem("InstManipulate");
            }
        }
    }
}