using System.Collections;
using System.Linq;
using UnityEngine;

namespace PipelineDreams.Instruction
{
    public abstract partial class Instruction {
        /// <summary>
        /// The most basic example of melee instruction task.
        /// </summary>
        protected class BasicMeleeTask : InstructionTask {
            protected override IEnumerator OnRun()
            {
                ///Attack priority is given by OrderBy.
                var _entity = Op.EM.FindEntities((x)=>x.IdealPosition == Util.QToUVector(Op.Holder.IdealRotation) + Op.Holder.IdealPosition).OrderBy((x)=>x.Data.Type).FirstOrDefault();
                if (_entity != null)
                {
                    var anim = Op.Holder.GetComponent<Entity.Animator>();
                    bool flag1 = false;
                    bool flag2 = false;
                    anim.OnAnimEvent += (name) => { if (name == "MeleeAttack") flag1 = true; if (name == "ClipEnd") flag2 = true; };
                    anim.InvokeAnimation("MeleeAttack");
                    while (!flag1)
                        yield return null;
                    Op.Holder.GetComponent<Entity.WeaponHolder>().PerformAttack(_entity, StartClock, (Op.Data as Data).MeleeCoef, 0, 0, Accuracy);
                    while (!flag2)
                        yield return null;
                }
                else
                    Debug.LogWarning("InstructionTask target entity is null!");
            }


        }
    }

}