using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity
{
    public class CommandReader : MonoBehaviour
    {
        Entity entity;
        CommandBroadcast targetCB;
        Container EC;
        public event Action<Command> OnCommandKeyPressed;
        // Start is called before the first frame update
        void Awake()
        {
            entity = GetComponent<Entity>();
            entity.GetComponent<AI>().OnClockInit += Entity_OnClockInit;
        }

        private void Entity_OnClockInit(TaskManager arg1, Container arg2)
        {
            EC = arg2;
            arg1.OnTaskEnd += Arg1_OnTaskEnd;
        }

        private void Arg1_OnTaskEnd()
        {
            if(targetCB!=null)
                targetCB.OnCommand -= OnCommandKeyPressed;
            var e = EC.FindLineOfSightEntityOnAxis(Util.QToFace(entity.IdealRotation), entity);
            if (e!=null)
            targetCB = e.GetComponent<CommandBroadcast>();
            if (targetCB != null)
                targetCB.OnCommand += OnCommandKeyPressed;
        }

    }
}