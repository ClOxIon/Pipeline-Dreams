using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    public abstract class Instruction {
        public InstructionData OpData;
        protected CommandsContainer PC;
        EntityDataContainer EM;
        Entity Player;
        public string Variant;
        List<Command> Commands;
        [SerializeField] GunController gun;
        public virtual IClockTask Operation(float startClock) {
            return new AttackInstructionTask() { Op = this, StartClock = startClock, Priority = (int)EntityType.PLAYER };

        }
        protected class AttackInstructionTask : IClockTask {
            public int Priority { get; set; }
            public Instruction Op;
            public float StartClock { get; set; }

            public IEnumerator Run() {

                Op.Player.GetComponent<EntityWeapon>().TryAttack(Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.Player.IdealRotation), Op.Player), StartClock, Op.OpData.meleeCoef, Op.OpData.rangeCoef, Op.OpData.fieldCoef);
                if (Op.gun != null)
                    Op.gun.trigger = true;
                float time = 0;
                while (time < 1f) {

                    yield return null;
                    time += Time.deltaTime;
                }
            }


        }
        public Instruction(EntityDataContainer eM, Entity player, CommandsContainer pC) {
            EM = eM;
            Player = player;
            PC = pC;
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
            int d = TC(Variant[0]) - OpData.Commands[0];
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
                if (c == Command.space || c == Command.left || c == Command.right) return c;
                return (Command)(((int)c + 2) % 4);
            }
            Command TC(char c) {
                switch (c) {
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

            if (VL < OC)
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
                    if (v[VL - OC + i] != Commands[i])
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
                        PC.DeleteCommandAt(ch, OC);
                        return true;

                    }
                }
                return false;
            default:
                return false;
            }

        }
    }

    public class InstructionMomentum : Instruction {
        public InstructionMomentum(EntityDataContainer eM, Entity player, CommandsContainer pC) : base(eM, player, pC) {
        }
    }
    public class InstructionInertia : Instruction {
        public InstructionInertia(EntityDataContainer eM, Entity player, CommandsContainer pC) : base(eM, player, pC) {
        }
    }
    public class InstructionThrust : Instruction {
        public InstructionThrust(EntityDataContainer eM, Entity player, CommandsContainer pC) : base(eM, player, pC) {
        }
    }
    public class InstructionManipulate : Instruction {
        public InstructionManipulate(EntityDataContainer eM, Entity player, CommandsContainer pC) : base(eM, player, pC) {
        }
    }

}