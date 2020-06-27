using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams.Instruction
{
    public abstract partial class Instruction : PDObject {
        protected CommandsContainer PC;
        protected Entity.Container EM;
        /// <summary>
        /// Type conversion macro
        /// </summary>
        protected Data OpData => Data as Data;
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
        public virtual void Obtain(Entity.Entity holder, TaskManager cM, Entity.Container eM, CommandsContainer pC) {
            Obtain(holder, cM);
            PC = pC;
            EM = eM;
        }
        public override void Init(PDData data, params object[] args) {
            base.Init(data, args);
            TimeCost.OnEvalRequest += () => { TimeCost.AddFunction(new MutableValue.Constant() { Value = (Data as Data).Time }); };
            TimeCost.EvalAtNextGet = true;
        }
        
        /// <summary>
        /// Reads Commands from the current pipeline.
        /// </summary>
        /// <returns>If successfully read matching command, deletes corresponding commands, and returns true. Otherwise, leaves all commands at the same place of the pipeline, and returns false.</returns>
        public virtual bool ReadCommand() {
            var v = PC.CurrentPipeline();
            var VL = v.Length;
            var OC = (Data as Data).Commands.Length;

            if (VL < OC)
                return false;
            

                for (int ch = 0; ch < VL - OC + 1; ch++) {
                    bool b = true;
                    for (int i = 0; i < OC; i++)
                        if (v[ch + i] != (Data as Data).Commands[i])
                            b = false;
                    if (b) {
                        PC.DeleteCommandAt(ch, OC);
                        return true;

                    }
                }
            return false;

        }
        protected void TriggerEffect(bool b) {
            if (b) {
                if ((Data as Data).Gun != null)
                {
                    _instance = GameObject.Instantiate((Data as Data).Gun, Holder.transform);
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
                case Entity.EntityType.ENEMY: _p = TaskPriority.ENEMY; break;

                case Entity.EntityType.NPC: _p = TaskPriority.NPC; break;

                case Entity.EntityType.PLAYER: _p = TaskPriority.PLAYER; break;
            }
            x.Priority = _p;
            x.EffectDuration = (Data as Data).EffectDuration;
            x.StartClock = startClock;
            return x;
        }
    }

}