using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PipelineDreams {
    public class PipelineUI : MonoBehaviour {
        [SerializeField] GameObject Container;
        [SerializeField] GameObject SpacerContainer;
        [SerializeField] GameObject Spacer;
        [SerializeField] List<GameObject> CommandPrefabs;
        Queue<GameObject> CommandObjects = new Queue<GameObject>();
        List<GameObject> Spacers = new List<GameObject>();
        [SerializeField] CommandsContainer PC;
        private void Awake() {
            PC.OnDelCommandAt += PC_OnDelCommandAt;
            PC.OnDelCommandFromBack += PC_OnDelCommandFromBack;
            PC.OnDelCommandFromFront += PC_OnDelCommandFromFront;
            PC.OnFlush += PC_OnFlush;
            PC.OnPushCommand += PC_OnPushCommand;
            PC.OnLengthChange += PC_OnLengthChange;
            PC.OnPop += PC_OnPop;
        }
        private void Start() {
            PC.InvokeRefresh();
        }

        private void PC_OnPop(Command obj) {
            Destroy(CommandObjects.Dequeue());

        }

        private void PC_OnPushCommand(Command obj) {
            CommandObjects.Enqueue(Instantiate(CommandPrefabs[(int)obj], Container.transform, false));

        }

        private void PC_OnFlush() {
            CommandObjects.Clear();
        }

        private void PC_OnDelCommandFromFront(int obj) {
            while (obj > 0) {

                Destroy(CommandObjects.Dequeue());
                obj--;
            }
        }

        private void PC_OnDelCommandFromBack(int obj) {
            var CommandObjectsList = CommandObjects.ToList();
            var c = CommandObjects.Count;
            for (int i = c - obj; i < c; i++)
                Destroy(CommandObjectsList[i]);
            CommandObjectsList.RemoveRange(c - obj, obj);
            CommandObjects = new Queue<GameObject>(CommandObjectsList);
        }

        private void PC_OnDelCommandAt(int index, int number) {
            var CommandObjectsList = CommandObjects.ToList();
            for (int i = index; i < index + number; i++)
                Destroy(CommandObjectsList[i]);
            CommandObjectsList.RemoveRange(index, number);
            CommandObjects = new Queue<GameObject>(CommandObjectsList);

        }

        private void PC_OnLengthChange(int obj) {

            foreach (var o in Spacers)
                Destroy(o);
            Spacers.Clear();
            for (int i = 0; i < obj; i++)
                Spacers.Add(Instantiate(Spacer, SpacerContainer.transform, false));

        }

    }
}
