using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipelineDreams {
    [CreateAssetMenu(fileName = "InstructionContainer", menuName = "ScriptableObjects/Manager/InstructionContainer")]
    public class InstructionContainer : ScriptableObject, IDataContainer<Instruction> {
        public event Action OnInstructionCollectionInitialized;
        public event Action<Instruction[]> OnRefreshItems;
        public event Action<int> OnChangeItemSlotAvailability;
        //[SerializeField]InstructionPromptUI InstructionDestroyPrompt;
        [SerializeField] InstructionDataset DataContainer;
        [SerializeField] EntityDataContainer EM;
        [SerializeField] CommandsContainer PC;
        Entity Player;
        List<Instruction> Instructions = new List<Instruction>();
        TaskManager TM;

        int MaximumInstructionCount = 10;
        int ActivatedSlots;


        public void Init(TaskManager tm, Entity player) {
            TM = tm;
            Player = player;
            for (int i = 0; i < MaximumInstructionCount; i++)
                Instructions.Add(null);
            ChangeActivatedSlots(3);
            AddItem("Inertia R");

            OnInstructionCollectionInitialized?.Invoke();
        }

        public void UseInstructionAt(int i) {
            if (Instructions.Count > i && Instructions[i] != null)
                if (Instructions[i].CheckCommand()) {
                    TM.AddSequentialTask(Instructions[i].Operation(TM.Clock));
                    Player.GetComponent<PlayerAI>().EntityClock += Instructions[i].OpData.Time * 12.5f;

                }
        }
        /// <summary>
        /// Adds the Instruction corresponding to the name to the inventory.
        /// </summary>
        /// <param name="name"></param>
        public void AddItem(string name) {

            Instruction AddedInstruction = null;
            InstructionData AddedInstructionData;
            var s = name.Split(' ');
            string variant = "";
            var name0 = s[0];
            if (s.Length > 1)
                variant = s[1];



            if (typeof(Instruction).Namespace != null) {
                if (Type.GetType(typeof(Instruction).Namespace + ".Instruction" + name0) != null)
                    AddedInstruction = (Instruction)Activator.CreateInstance(Type.GetType(typeof(Instruction).Namespace + ".Instruction" + name0), EM, Player, PC);
            } else {
                if (Type.GetType("Instruction" + name0) != null)
                    AddedInstruction = (Instruction)Activator.CreateInstance(Type.GetType("Instruction" + name0), EM, Player, PC);
            }
            AddedInstructionData = DataContainer.Dataset.Find((x) => { return x.Name == name0; });
            if (AddedInstructionData == null) {
                Debug.LogError("InstructionCollection.AddInstruction(): Cannot find Instruction named " + name0);
                return;
            }
            AddedInstruction.Activate(AddedInstructionData, variant);
            PushItem(AddedInstruction);

        }

        public void ChangeActivatedSlots(int after) {
            ActivatedSlots = after;
            if (ActivatedSlots < Instructions.Count) {

                Instructions.RemoveRange(after, Instructions.Count - after);
            }
            OnChangeItemSlotAvailability?.Invoke(after);
            OnRefreshItems?.Invoke(Instructions.ToArray());
        }
        /// <summary>
        /// This method is used to move an Instruction from inventory to somewhere else.
        /// 
        /// </summary>
        /// <param name="index"></param>
        public Instruction PopItem(int index) {
            var i = Instructions[index];
            return i;
        }
        /// <summary>
        /// This method is used to move an Instruction into inventory.
        /// </summary>
        /// <param name="Instruction"></param>
        public void PushItem(Instruction Instruction) {


            bool flag = true;
            for (int i = 1; i < Instructions.Count; i++) {
                if (Instructions[i] == null) {
                    Instructions[i] = Instruction;
                    flag = false;
                    break;
                }

            }
            if (flag) {

                //Instead of opening Instruction destroy prompt, the Instruction in the temporary slot will be overwritten without prompt.
                var i = 0;//Temporary slot

                Instructions[i] = Instruction;
            }
            OnRefreshItems?.Invoke(Instructions.ToArray());
        }
        public Data GetItemInfo(int index) {
            if (index < Instructions.Count)
                return Instructions[index]?.OpData;
            return null;
        }

        public void InvokeUIRefresh() {
            OnChangeItemSlotAvailability?.Invoke(ActivatedSlots);
            OnRefreshItems?.Invoke(Instructions.ToArray());

        }
        public void SwapItem(int index1, int index2) {
            var tmp = Instructions[index1];
            Instructions[index1] = Instructions[index2];
            Instructions[index2] = tmp;
            InvokeUIRefresh();
        }
    }
}