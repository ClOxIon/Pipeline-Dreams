using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class Instruction {
    public InstructionData OpData;
    protected PipelineController PC;
    EntityManager EM;
    ClockManager CM;
    Entity Player;
    public string Variant;
    List<Command> Commands;
    [SerializeField] GunController gun;
    public virtual IClockTask Operation(float startClock) {
        return new OperationTask() {Op = this, StartClock = startClock , Priority = (int)EntityType.PLAYER };

    }
    protected class OperationTask : IClockTask {
        public int Priority { get; set; }
        public Instruction Op;
        public bool HasIteration { get; } = true;
        public float StartClock { get; set; }

        public IEnumerator Run() {
            Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.EM.Player.IdealRotation), Op.EM.Player)?.GetComponent<EntityHealth>().RecieveDamage(Op.OpData.Value1, Op.Player);
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
        CM = EM.GetComponent<ClockManager>();
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

public class OperatorMomentum: Instruction
{
   
}
public class OperatorInertia : Instruction {
    
}
public class OperatorThrust : Instruction {
    
}
public class OperatorManipulate : Instruction {

}
public class OperatorRearrange : Instruction {

}
public class OperatorCircuitBreaker : Instruction {

}
public class OperatorTrace : Instruction {

}
public class OperatorOscillate : Instruction {

}
//Tier 2
public class OperatorOrbit : Instruction {

}
public class OperatorSpatialRotation : Instruction {

}
public class OperatorCharge : Instruction {

}
public class OperatorGradualCharge : Instruction {

}
public class OperatorWarp : Instruction {

}
public class OperatorRotationalCharge : Instruction {

}
public class OperatorBypass : Instruction {

}
public class OperatorRecursion : Instruction {

}
public class OperatorRestore : Instruction {

}
public class OperatorFluctuation : Instruction {

}
public class OperatorEject : Instruction {

}
public class OperatorTranslate : Instruction {

}
public class OperatorConversion : Instruction {

}
public class OperatorMerge: Instruction {

}
public class OperatorClone : Instruction {

}
//Tier 3
public class OperatorIntersect : Instruction {

}
public class OperatorDistort : Instruction {

}
public class OperatorInfinity : Instruction {

}
public class OperatorTrigger : Instruction {

}
public class OperatorInfuse : Instruction {

}
public class OperatorAdvance : Instruction {

}
public class OperatorConnect : Instruction {

}
public class OperatorBackendConnect : Instruction {

}
public class OperatorRead : Instruction {

}
public class OperatorChartR : Instruction {

}
public class OperatorChartL : Instruction {

}
public class OperatorReiterate : Instruction {

}
public class OperatorModify : Instruction {

}
//Tier 4
public class OperatorOvercharge : Instruction {

}
public class OperatorReciprocation : Instruction {

}
public class OperatorReverse : Instruction {

}
public class OperatorCrystal : Instruction {

}
public class OperatorSubstitute : Instruction {

}
public class OperatorOverdrive : Instruction {

}
public class OperatorInvariant : Instruction {

    
}
public class OperatorDerive : Instruction {

}