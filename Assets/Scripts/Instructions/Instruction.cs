using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{
    public abstract partial class Instruction : PDObject {
        protected CommandsContainer PC;
        protected EntityDataContainer EM;
        /// <summary>
        /// Type conversion macro
        /// </summary>
        protected InstructionData OpData => Data as InstructionData;
        public string Variant;
        Command[] Commands;
        EffectVisualizer _instance;
        public MutableValue.FunctionChain TimeCost { get; } = new MutableValue.FunctionChain();
        public abstract IClockTask Operation(float startClock);
        /// <summary>
        /// Called when this instruction is added to an instructionContainer. To make the factory pattern less wordy, we use this function instead of a constructor to initialize objects.
        /// </summary>
        /// <param name="eM"></param>
        /// <param name="pC"></param>
        /// <param name="data"></param>
        /// <param name="variant"></param>
        public virtual void Obtain(Entity holder, TaskManager cM, EntityDataContainer eM, CommandsContainer pC) {
            Obtain(holder, cM);
            PC = pC;
            EM = eM;
        }
        public override void Init(PDData data, params object[] args) {
            base.Init(data, args);
            Variant = args[0] as string;
            Commands = GetCommandsVariant();

            TimeCost.OnEvalRequest += () => { TimeCost.AddFunction(new MutableValue.Constant() { Value = (Data as InstructionData).Time }); };
        }
        /// <summary>
        /// Inteprets variant string.
        /// </summary>
        /// <returns></returns>
        public Command[] GetCommandsVariant() {
            InstructionData OpData = Data as InstructionData;
            if (Variant == "")
                return OpData.Commands;
            if (Variant == "S")
                return new Command[] { Command.space };
            int d = InstUtil.TC(Variant[0]) - OpData.Commands[0];
            var r = new List<Command>();
            foreach (var x in OpData.Commands)
                r.Add(InstUtil.RC(x, d));
            if (Variant.Length == 1) {
                return r.ToArray();
            } else {//Variant.length == 2
                if (InstUtil.TC(Variant[1]) != r[1]) {
                    r.Clear();
                    foreach (var x in OpData.Commands)
                        r.Add(InstUtil.RC(InstUtil.MC(x), d));
                }
                return r.ToArray();

            }
            
        }
        protected static class InstUtil
        {
            public static Command RC(Command c, int i)
            {
                if (c == Command.space) return c;
                return (Command)(((int)c + i + 4) % 4);
            }
            public static Command MC(Command c)
            {
                if (c == Command.space || c == Command.left || c == Command.right) return c;
                return (Command)(((int)c + 2) % 4);
            }
            public static Command TC(char c)
            {
                switch (c)
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
            var OC = (Data as InstructionData).Commands.Length;

            if (VL < OC)
                return false;
            switch ((Data as InstructionData).Direction) {
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
        protected void TriggerEffect(bool b) {
            if (b) {
                if ((Data as InstructionData).Gun != null)
                {
                    _instance = GameObject.Instantiate((Data as InstructionData).Gun, Holder.transform);
                    _instance.trigger = b;
                }
            }
            else if (_instance != null)
            {
                
                GameObject.Destroy(_instance.gameObject);
                _instance.trigger = b;
                _instance = null;
            }
        }
        /// <summary>
        /// In favor of the factory pattern, this substitute InstructionTask's constructor.
        /// </summary>
        protected InstructionTask PassParam(InstructionTask x, float startClock) {
            x.Op = this;
            TaskPriority _p = TaskPriority.ENEMY;
            switch (Holder.Data.Type)
            {
                case EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case EntityType.NPC: _p = TaskPriority.NPC; break;

                case EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            x.Priority = _p;
            x.EffectDuration = (Data as InstructionData).EffectDuration;
            x.Accuracy = (Data as InstructionData).BaseAccuracy;
            x.StartClock = startClock;
            return x;
        }
    }

}