using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class Instruction {
    public InstructionData OpData;
    protected PipelineController PC;
    EntityManager EM;
    TaskManager CM;
    Entity Player;
    public string Variant;
    List<Command> Commands;
    [SerializeField] GunController gun;
    public virtual IClockTask Operation(float startClock) {
        return new AttackInstructionTask() {Op = this, StartClock = startClock , Priority = (int)EntityType.PLAYER };

    }
    protected class AttackInstructionTask : IClockTask {
        public int Priority { get; set; }
        public Instruction Op;
        public bool HasIteration { get; } = true;
        public float StartClock { get; set; }

        public IEnumerator Run() {

            Op.Player.GetComponent<EntityWeapon>().TryAttack(Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.EM.Player.IdealRotation), Op.EM.Player), StartClock, Op.OpData.meleeCoef, Op.OpData.rangeCoef, Op.OpData.fieldCoef);
            if (Op.gun != null)
                Op.gun.trigger = true;
            float time = 0;
            while (time < 1f) {

                yield return null;
                time += Time.deltaTime;
            }
        }

        
    }
    public Instruction() {
        EM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>();
        CM = EM.GetComponent<TaskManager>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        PC = GameObject.FindGameObjectWithTag("Pipeline").GetComponent<PipelineController>();
    }
    public virtual void Activate(InstructionData data, string variant) {
        OpData = data;
        Variant = variant;
        Commands = GetCommandsVariant();
    }
    public List<Command> GetCommandsVariant() {
        if (Variant == "")
            return OpData.Commands;
        if (Variant == "S")
            return new List<Command>() { Command.space };
        int d = TC(Variant[0])- OpData.Commands[0];
        var r = new List<Command>();
        foreach (var x in OpData.Commands)
            r.Add(RC(x, d));
        if (Variant.Length == 1) {
            return r;
        } else {//Variant.length == 2
            if (TC(Variant[1]) != r[1]) {
                r.Clear();
                foreach (var x in OpData.Commands)
                    r.Add(RC(MC(x), d));
            }
            return r;

        }
        Command RC(Command c, int i) {
            if (c == Command.space) return c;
            return (Command)(((int)c + i + 4) % 4);
        }
        Command MC(Command c) {
            if (c == Command.space||c==Command.left||c==Command.right) return c;
            return (Command)(((int)c + 2) % 4);
        }
        Command TC(char c) {
            switch (c){
            case 'L':
                return Command.left;
                
            case 'R':
                return Command.right;
            case 'U':
                return Command.up;
            case 'D':
                return Command.down;
            default:
                return Command.space;
            }
           
        }
    }
    public virtual bool CheckCommand() {
        var v = PC.CurrentPipeline();
        var VL = v.Length;
        var OC = OpData.Commands.Count;
        
        if (VL<OC)
            return false;
        switch (OpData.Direction) {
        case OpDirection.Front:
            for (int i = 0; i < OC; i++)
                if (v[i] != Commands[i])
                    return false;
            PC.DeleteCommandFromFront(OC);
            return true;
        case OpDirection.Back:
            for (int i = 0; i < OC; i++)
                if (v[VL-OC+i] != Commands[i])
                    return false;
            PC.DeleteCommandFromBack(OC);
            return true;
        case OpDirection.Omni:
            
            for (int ch = 0; ch < VL - OC + 1; ch++) {
                bool b = true;
                for (int i = 0; i < OC; i++)
                    if (v[ch + i] != Commands[i])
                        b = false;
                if (b) {
                    PC.DeleteCommandAt(ch,OC);
                    return true;

                }
            }
            return false;
        default:
            return false;
        }
       
    }
}

public class InstructionMomentum: Instruction
{
   
}
public class InstructionInertia : Instruction {
    
}
public class InstructionThrust : Instruction {
    
}
public class InstructionManipulate : Instruction {

}
public class InstructionRearrange : Instruction {

}
public class InstructionCircuitBreaker : Instruction {

}
public class InstructionTrace : Instruction {

}
public class InstructionOscillate : Instruction {

}
//Tier 2
public class InstructionOrbit : Instruction {

}
public class InstructionSpatialRotation : Instruction {

}
public class InstructionCharge : Instruction {

}
public class InstructionGradualCharge : Instruction {

}
public class InstructionWarp : Instruction {

}
public class InstructionRotationalCharge : Instruction {

}
public class InstructionBypass : Instruction {

}
public class InstructionRecursion : Instruction {

}
public class InstructionRestore : Instruction {

}
public class InstructionFluctuation : Instruction {

}
public class InstructionEject : Instruction {

}
public class InstructionTranslate : Instruction {

}
public class InstructionConversion : Instruction {

}
public class InstructionMerge: Instruction {

}
public class InstructionClone : Instruction {

}
//Tier 3
public class InstructionIntersect : Instruction {

}
public class InstructionDistort : Instruction {

}
public class InstructionInfinity : Instruction {

}
public class InstructionTrigger : Instruction {

}
public class InstructionInfuse : Instruction {

}
public class InstructionAdvance : Instruction {

}
public class InstructionConnect : Instruction {

}
public class InstructionBackendConnect : Instruction {

}
public class InstructionRead : Instruction {

}
public class InstructionChartR : Instruction {

}
public class InstructionChartL : Instruction {

}
public class InstructionReiterate : Instruction {

}
public class InstructionModify : Instruction {

}
//Tier 4
public class InstructionOvercharge : Instruction {

}
public class InstructionReciprocation : Instruction {

}
public class InstructionReverse : Instruction {

}
public class InstructionCrystal : Instruction {

}
public class InstructionSubstitute : Instruction {

}
public class InstructionOverdrive : Instruction {

}
public class InstructionInvariant : Instruction {

    
}
public class InstructionDerive : Instruction {

}