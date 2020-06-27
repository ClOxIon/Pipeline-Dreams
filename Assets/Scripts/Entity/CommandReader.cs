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
        public event Action<Command> OnCommandKeyPressed;
        // Start is called before the first frame update
        void Awake()
        {
            entity = GetComponent<Entity>();
            entity.GetComponent<AI>().OnClockInit += Entity_OnClockInit;
        }

        private void Entity_OnClockInit(TaskManager arg1, Container arg2)
        {
            GetComponent<Sight>().OnTargetChange += Sight_OnTargetChange;
        }

        private void Sight_OnTargetChange(Entity NewTarget)
        {
            if(targetCB!=null)
                targetCB.OnCommand -= OnCommandKeyPressed;
            if (NewTarget!=null)
            targetCB = NewTarget.GetComponent<CommandBroadcast>();
            if (targetCB != null)
                targetCB.OnCommand += OnCommandKeyPressed;
        }

    }
}