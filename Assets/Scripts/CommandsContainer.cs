using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    public enum Command {
        left = 0, up, right, down, space
    }
    [CreateAssetMenu(fileName = "CommandsContainer", menuName = "ScriptableObjects/Manager/CommandsContainer")]
    public class CommandsContainer : ScriptableObject {
        int Length = 3;
        PlayerMove PM;
        Queue<Command> Commands = new Queue<Command>();
        public event Action<Command> OnPop;
        public event Action<Command> OnPushCommand;
        public event Action<int> OnDelCommandFromFront;
        public event Action<int> OnDelCommandFromBack;
        public event Action<int, int> OnDelCommandAt;
        public event Action OnFlush;
        public event Action<int> OnLengthChange;
        // Start is called before the first frame update
        private void Awake() {


        }
        public void Init(PlayerMove pm) {
            PM = pm;
            PM.OnCommandKeyPressed += PushCommand;
            LengthChange(6);
        }
        public void LengthChange(int length) {
            Length = length;

            OnLengthChange?.Invoke(Length);
        }
        void PushCommand(Command i) {

            Commands.Enqueue(i);
            if (Commands.Count > Length) {
                var v = Commands.Dequeue();
                OnPop?.Invoke(v);

            }
            OnPushCommand?.Invoke(i);
        }
        public void DeleteCommandFromFront(int number) {
            while (number > 0) {
                Commands.Dequeue();

                number--;
            }
            OnDelCommandFromFront?.Invoke(number);
        }
        public void DeleteCommandFromBack(int number) {

            var CommandsList = Commands.ToList();
            var c = Commands.Count;
            CommandsList.RemoveRange(c - number, number);
            Commands = new Queue<Command>(CommandsList);
            OnDelCommandFromBack?.Invoke(number);
        }
        public void DeleteCommandAt(int index, int number) {
            var CommandsList = Commands.ToList();
            var c = Commands.Count;
            CommandsList.RemoveRange(index, number);
            Commands = new Queue<Command>(CommandsList);
            OnDelCommandAt?.Invoke(index, number);
        }
        public void Flush() {
            Commands.Clear();
            OnFlush?.Invoke();
        }
        public void InvokeRefresh() {

            OnLengthChange?.Invoke(Length);
        }
        public Command[] CurrentPipeline() {
            return Commands.ToArray();

        }

    }
}