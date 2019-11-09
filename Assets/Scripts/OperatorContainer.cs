using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorContainer : MonoBehaviour {
    [SerializeField] OperatorPromptUI OperatorDestroyPrompt;
    [SerializeField]public OperatorDataset DataContainer;
    public Operator[] Operators = new Operator[6];
    PlayerController PC;
    [SerializeField] List<OperatorUI> OperatorUIs;
    ClockManager CM;
    EntityManager EM;
    private void Awake() {
       
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        CM = PC.GetComponent<ClockManager>();
        EM = PC.GetComponent<EntityManager>();
        EM.OnPlayerInit+=()=>PC.OnOperatorKeyPressed += (i) => {
            if(Operators[i]!=null)
            if (Operators[i].CheckCommand()) {
                CM.AddSequentialTask(Operators[i].Operation(CM.Clock));
                EM.Player.GetComponent<PlayerAI>().EntityClock+=Operators[i].OpData.Time * 12.5f;
            }
            };
        AddOp("Inertia R");
        AddOp("Thrust U");
        AddOp("Manipulate RU");
    }
    public void AddOp(string cname) {
        Operator testOp;
        var s = cname.Split(' ');
        string variant = "";
        var name = s[0];
        if(s.Length>1)
         variant = s[1];
        if (typeof(Operator).Namespace!=null)
            testOp = (Operator)Activator.CreateInstance(Type.GetType(typeof(Operator).Namespace+".Operator"+name));
        else
            testOp = (Operator)Activator.CreateInstance(Type.GetType("Operator" + name));
        var testOpD = DataContainer.Dataset.Find((x) => { return x.Name == name; });
        testOp.Activate(testOpD, variant);
        try {
            int position = 0;
            bool flag = false;
            for (; position < Operators.Length; position++)
                if (Operators[position] == null) {
                    Operators[position] = testOp;
                    OperatorUIs[position].Refresh(testOp);
                    
                    flag = true;
                    break;

                }
            if (!flag) {
                //No Itemslot available
                PC.DisableInput(PlayerInputFlag.OPERATORCONTAINER);
                
                OperatorDestroyPrompt.Activate(testOpD);
                OperatorDestroyPrompt.OnDestroyButtonClicked += (i) => {
                    if (i != 0) {

                        Debug.Log("OpDestroyButton");
                        Operators[i - 1] = testOp;
                        OperatorUIs[i - 1].Refresh(testOp);
                    }
                        OperatorDestroyPrompt.gameObject.SetActive(false);
                        PC.EnableInput(PlayerInputFlag.OPERATORCONTAINER);
                    
                };
                
            }

           Refresh();
        }
        catch (System.IndexOutOfRangeException e) {
            Debug.LogError("Tried to add Operator at an invalid index.");
        }
        
    }
    // Start is called before the first frame update
    void Start() {
        Refresh();
    }
    public void Refresh() {
        for (int i = 0; i < 6; i++)
            if (Operators[i] == null) OperatorUIs[i].gameObject.SetActive(false);
            else {
                OperatorUIs[i].gameObject.SetActive(true);
                OperatorUIs[i].Refresh(Operators[i]);
            }
    }

}
