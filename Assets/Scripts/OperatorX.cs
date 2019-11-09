using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class Operator {
    public OperatorData OpData;
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
        public Operator Op;
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
    public Operator() {
        EM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>();
        CM = EM.GetComponent<ClockManager>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        PC = GameObject.FindGameObjectWithTag("Pipeline").GetComponent<PipelineController>();
    }
    public virtual void Activate(OperatorData data, string variant) {
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

public class OperatorMomentum: Operator
{
   
}
public class OperatorInertia : Operator {
    
}
public class OperatorThrust : Operator {
    
}
public class OperatorManipulate : Operator {

}
public class OperatorRearrange : Operator {

}
public class OperatorCircuitBreaker : Operator {

}
public class OperatorTrace : Operator {

}
public class OperatorOscillate : Operator {

}
//Tier 2
public class OperatorOrbit : Operator {

}
public class OperatorSpatialRotation : Operator {

}
public class OperatorCharge : Operator {

}
public class OperatorGradualCharge : Operator {

}
public class OperatorWarp : Operator {

}
public class OperatorRotationalCharge : Operator {

}
public class OperatorBypass : Operator {

}
public class OperatorRecursion : Operator {

}
public class OperatorRestore : Operator {

}
public class OperatorFluctuation : Operator {

}
public class OperatorEject : Operator {

}
public class OperatorTranslate : Operator {

}
public class OperatorConversion : Operator {

}
public class OperatorMerge: Operator {

}
public class OperatorClone : Operator {

}
//Tier 3
public class OperatorIntersect : Operator {

}
public class OperatorDistort : Operator {

}
public class OperatorInfinity : Operator {

}
public class OperatorTrigger : Operator {

}
public class OperatorInfuse : Operator {

}
public class OperatorAdvance : Operator {

}
public class OperatorConnect : Operator {

}
public class OperatorBackendConnect : Operator {

}
public class OperatorRead : Operator {

}
public class OperatorChartR : Operator {

}
public class OperatorChartL : Operator {

}
public class OperatorReiterate : Operator {

}
public class OperatorModify : Operator {

}
//Tier 4
public class OperatorOvercharge : Operator {

}
public class OperatorReciprocation : Operator {

}
public class OperatorReverse : Operator {

}
public class OperatorCrystal : Operator {

}
public class OperatorSubstitute : Operator {

}
public class OperatorOverdrive : Operator {

}
public class OperatorInvariant : Operator {

    
}
public class OperatorDerive : Operator {

}