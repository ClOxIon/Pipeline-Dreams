namespace PipelineDreams.Instruction
{
    public class Oscillate : Instruction {
        
        public override IClockTask Operation(float startClock)
        {

            return PassParam(new OscillateTask(), startClock);
        }
        
    }
    public abstract partial class Instruction
    {
        /// <summary>
        /// Field instruction task used above.
        /// </summary>
        protected class OscillateTask : InstructionTask
        {
            protected override void OnRunStart()
            {
                for (int i = 0; i < 6; i++)
                {
                    var _entity = Op.EM.FindEntityRelative(Util.FaceToUVector(i), Op.Holder);
                    if (_entity != null)
                        Op.Holder.GetComponent<Entity.WeaponHolder>().PerformAttack(_entity, StartClock, 0, 0, Op.OpData.FieldCoef, Accuracy);
                }
            }
        }
    }

}
