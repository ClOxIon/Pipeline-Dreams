namespace PipelineDreams.Instruction
{
    public class Manipulate : Instruction {
        

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
                var _entity = Op.EM.FindVisibleEntityOnAxis(Util.LHQToFace(Op.Holder.IdealRotation), Op.Holder);
                if (_entity != null)
                    Op.Holder.GetComponent<Entity.WeaponHolder>().PerformAttack(_entity, StartClock, 0, 0, (Op.Data as Data).FieldCoef, Accuracy);
                var b = Op.Holder.GetComponent<Entity.BuffContainerHolder>();
                if(b!=null)
                b.BuffContainer.AddItem("InstManipulate");
            }
        }
    }
}