using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams
{

    public class EntityMoveEnergyConsume : MonoBehaviour
    {

        Entity entity;
        EntityMove entityMove;
        private void Awake() {

            entity = GetComponent<Entity>();
            entityMove = GetComponent<EntityMove>();
            entity.OnInit += (tm, ec) => {
                var f = new MutableValue.FunctionChain();
                f.OnEvalRequest += () => f.AddFunction(new MutableValue.Constant() { Value = entity.Data.FindParameterFloat("MoveEnergyConsume") });
                f.EvalAtNextGet = true;
                entity.Stats.Add("MoveEnergyConsume", f);
                var g = new MutableValue.FunctionChain();
                g.OnEvalRequest += () => g.AddFunction(new MutableValue.Constant() { Value = entity.Data.FindParameterFloat("RotateEnergyConsume") });
                g.EvalAtNextGet = true;
                entity.Stats.Add("RotateEnergyConsume", g);
                entityMove.SubscribeOnMove((i,t) => { entity.Parameters["Energy"] -= entity.Stats["MoveEnergyConsume"].Value; return null; });
                entityMove.SubscribeOnRotate((i, t) => { entity.Parameters["Energy"] -= entity.Stats["RotateEnergyConsume"].Value; return null; });
            };
            
        }

        
    }
}