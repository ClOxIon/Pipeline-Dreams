namespace PipelineDreams
{
    public class InstructionOscillate : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new InstructionOscillateTask());
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class InstructionOscillateTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                for (int i = 0; i < 6; i++)
                {
                    var _entity = Op.EM.FindEntityInRelativePosition(Util.FaceToLHVector(i), Op.Holder);
                    if (_entity != null)
                        Op.Holder.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, 0, Op.OpData.FieldCoef, Accuracy);
                }
            }
        }
    }

}
