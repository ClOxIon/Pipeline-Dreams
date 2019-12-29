using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{
    public abstract partial class Instruction {
        public InstructionData OpData;
        protected CommandsContainer PC;
        EntityDataContainer EM;
        Entity Player;
        public string Variant;
        List<Command> Commands;
        [SerializeField] GunController gun;
        public abstract IClockTask Operation(float startClock);
        /// <summary>
        /// The most basic example of melee instruction task.
        /// </summary>
        protected class MeleeInstructionTask : IClockTask {
            public Priority Priority { get; set; }
            public Instruction Op;
            public float StartClock { get; set; }
            public float Accuracy = 0;
            /// <summary>
            /// Duration of the animation, seconds
            /// </summary>
            float Duration = 1f;
            public IEnumerator Run() {
                var _entity = Op.EM.FindEntityInRelativePosition(Util.LHQToLHUnitVector(Op.Player.IdealRotation), Op.Player);
                if (_entity != null && _entity == Op.EM.FindEntityInLine(Util.LHQToFace(Op.Player.IdealRotation), Op.Player))
                    Op.Player.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, Op.OpData.meleeCoef, 0, 0, Accuracy);
                if (Op.gun != null)
                    Op.gun.trigger = true;
                float time = 0;
                //Animation events could be called here.
                while (time < Duration) {

                    yield return null;
                    time += Time.deltaTime;
                }
            }


        }
        /// <summary>
        /// The most basic example of ranged instruction task.
        /// </summary>
        protected class RangedInstructionTask : IClockTask
        {
            public Priority Priority { get; set; }
            public Instruction Op;
            public float StartClock { get; set; }
            public float Accuracy = 0;
            /// <summary>
            /// Duration of the animation, seconds
            /// </summary>
            float Duration = 1f;
            public IEnumerator Run()
            {
                var _entity = Op.EM.FindEntityInLine(Util.LHQToFace(Op.Player.IdealRotation), Op.Player);
                if (_entity != null)
                Op.Player.GetComponent<EntityWeapon>().PerformAttack(_entity, StartClock, 0, Op.OpData.rangeCoef, 0, Accuracy);
                if (Op.gun != null)
                    Op.gun.trigger = true;
                float time = 0;
                //Animation events could be called here.
                while (time < Duration)
                {

                    yield return null;
                    time += Time.deltaTime;
                }
            }


        }
        /// <summary>
        /// Called when this instruction is added to an instructionContainer.
        /// </summary>
        /// <param name="eM"></param>
        /// <param name="player"></param>
        /// <param name="pC"></param>
        /// <param name="data"></param>
        /// <param name="variant"></param>
        public Instruction(EntityDataContainer eM, Entity player, CommandsContainer pC, InstructionData data, string variant) {
            EM = eM;
            Player = player;
            PC = pC;
            OpData = data;
            Variant = variant;
            Commands = GetCommandsVariant();
        }
        /// <summary>
        /// Inteprets variant string.
        /// </summary>
        /// <returns></returns>
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
                 switch(c)
                {
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
                };
            }
        }
        /// <summary>
        /// Reads Commands from the current pipeline.
        /// </summary>
        /// <returns>If successfully read matching command, deletes corresponding commands, and returns true. Otherwise, leaves all commands at the same place of the pipeline, and returns false.</returns>
        public virtual bool ReadCommand() {
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

}