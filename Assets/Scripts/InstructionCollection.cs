using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionCollection : MonoBehaviour {
    //[SerializeField] OperatorPromptUI OperatorDestroyPrompt;
    public event Action<Instruction[]> OnRefreshItems;
    [SerializeField]public InstructionDataset DataContainer;
    public List<Instruction> Instructions = new List<Instruction>();
    int MaxInstruction = 6;
    TaskManager CM;
    EntityManager EM;
    private void Awake() {
        CM = FindObjectOfType<TaskManager>();
        EM = FindObjectOfType<EntityManager>();
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
    }
    public void OnInstruction(object value) {
        var i = (int)value;
        if (Instructions.Count > i && Instructions[i] != null)
            if (Instructions[i].CheckCommand()) {
                CM.AddSequentialTask(Instructions[i].Operation(CM.Clock));
                EM.Player.GetComponent<PlayerAI>().EntityClock += Instructions[i].OpData.Time * 12.5f;
            }
    }
    public void AddInst(string cname) {
        Instruction testOp;
        var s = cname.Split(' ');
        string variant = "";
        var name = s[0];
        if(s.Length>1)
         variant = s[1];
        if (typeof(Instruction).Namespace!=null)
            testOp = (Instruction)Activator.CreateInstance(Type.GetType(typeof(Instruction).Namespace+".Instruction"+name));
        else
            testOp = (Instruction)Activator.CreateInstance(Type.GetType("Instruction" + name));
        var testOpD = DataContainer.Dataset.Find((x) => { return x.Name == name; });
        testOp.Activate(testOpD, variant);
        if (Instructions.Count < MaxInstruction) {
            Instructions.Add(testOp);
        } else {
            Instructions[MaxInstruction - 1] = testOp;
        }
        OnRefreshItems?.Invoke(Instructions.ToArray());
        
    }
    // Start is called before the first frame update
    void Start() {
        AddInst("Inertia R");
        AddInst("Thrust U");
        AddInst("Manipulate RU");
    }
    

}
