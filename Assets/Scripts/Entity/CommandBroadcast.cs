using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity
{
    public class CommandBroadcast : MonoBehaviour
    {
        public Action<Command> OnCommand;
        // Start is called before the first frame update
        private void Awake()
        {
            GetComponent<Entity>().OnInit += CommandBroadcast_OnInit;
        }

        private void CommandBroadcast_OnInit(TaskManager arg1, Container arg2)
        {
            GetComponent<Move>()?.SubscribeOnMove(OnMove);
            GetComponent<Move>()?.SubscribeOnRotate(OnRotate);
        }
        IEnumerator OnMove(Vector3Int i, Vector3Int f) {
            BroadcastCommand(Command.space);
            return null;
        }
        IEnumerator OnRotate(Quaternion i, Quaternion f)
        {
            BroadcastCommand(Command.rotate);
            return null;
        }
        public void BroadcastCommand(Command c) {
            OnCommand?.Invoke(c);
        }
    }
}