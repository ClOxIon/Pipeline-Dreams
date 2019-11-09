using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Item {
    public ItemData ItData;
    public event Action OnDestroy;
    protected ItemSlot Slot;
    protected ClockManager CM;
    public Item() {
       
    }
    public virtual void Activate(ItemData data, ItemSlot slot) {
        ItData = data;
        Slot = slot;
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ClockManager>();
    }
    public virtual void EffectByTime(float time) { }
    public void Destroy() {
        OnDestroy?.Invoke();
    }
}

public class Disk : Item {
    public event Action<string> OnDecode;
    public float InitTime;
    public float decode;
    public string Opdata;
    
    public Disk() : base() {
       
    }
    public override void EffectByTime(float time) {
        base.EffectByTime(time);
        if (Slot == ItemSlot.Analyzer1 || Slot == ItemSlot.Analyzer2) {
            decode = time-InitTime; if (decode >= ItData.value1) { OnDecode?.Invoke(Opdata); Destroy(); }
        }
    }
    public override void Activate(ItemData data, ItemSlot slot) {
        base.Activate(data, slot);
        InitTime = CM.Clock;
    }


}
public class ItemTest : Item {

}
public enum ItemSlot {
Core1, Core2, Emitter, Collector, Register, Exterior, Streamer, Engine, Analyzer2, Analyzer1

}
public class PlayerItem : MonoBehaviour
{
    OperatorContainer OC;
    [SerializeField]ItemPromptUI ItemDestroyPrompt;
    [SerializeField]ItemDataset DataContainer;
    public Item[] Items = new Item[10];
    public bool[] IsItemFrameActivated = new bool[10];
    [SerializeField] List<ItemUI> ItemUIs;
    PlayerController PC;
    ClockManager CM;
    private void Awake() {
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        CM = PC.GetComponent<ClockManager>();
        CM.OnClockModified += CM_OnClockModified;
        OC = GameObject.FindGameObjectWithTag("OperatorRack").GetComponent<OperatorContainer>();
        for (int i = 0; i < IsItemFrameActivated.Length; i++) {
            SetActiveSlot((ItemSlot)i, false);
        }
        SetActiveSlot(ItemSlot.Core1, true);//Engine1
        SetActiveSlot(ItemSlot.Analyzer1, true);//Analyzer1
    }

    private void CM_OnClockModified(float obj) {
        for (int i = 0; i < IsItemFrameActivated.Length; i++) {
            if (Items[i] != null&& IsItemFrameActivated[i]) {
                Items[i].EffectByTime(obj);
                ItemUIs[i].Refresh(Items[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < IsItemFrameActivated.Length; i++) {
            if (Items[i] != null)
                ItemUIs[i].Refresh(Items[i]);
        }
    }
    public void AddIt(string name) {
        var Disktest = name.Split(' ')[0];
        Item testIt;
        ItemData testItD;
        if (Disktest == "Disk") {

            var OpName = name.Substring(5);
            var disk = new Disk() { Opdata = OpName };
            disk.OnDecode += Disk_OnDecode;
            testIt = disk;
            testItD = DataContainer.Dataset.Find((x) => { return x.Name == "Disk"; });
            
            
        } else {
            
            if (typeof(Item).Namespace != null)
                testIt = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + ".Item" + name));
            else
                testIt = (Item)Activator.CreateInstance(Type.GetType("Item" + name));
            testItD = DataContainer.Dataset.Find((x) => { return x.Name == name; });
           
        }
        try {
            
            bool flag = false;
            for (int position = 0; position < Items.Length; position++)
                if (Items[position] == null&&IsItemFrameActivated[position]) {
                    testIt.Activate(testItD, (ItemSlot)position);
                    Items[position] = testIt;
                    ItemUIs[position].Refresh(testIt);
                    SetDestroyCallback(Items[position], position);
                    flag = true;
                    break;
                    
                }
            if (!flag) {
                //No Itemslot available
                PC.DisableInput(PlayerInputFlag.PLAYERITEM);
                ItemDestroyPrompt.Activate(testItD);
                ItemDestroyPrompt.OnDestroyButtonClicked += (i) => {
                    if (i != -1) {
                        
                        Items[i].Destroy();
                        
                        Items[i] = testIt;
                        testIt.Activate(testItD, (ItemSlot)i);
                        SetDestroyCallback(testIt, i);
                        ItemUIs[i].Refresh(testIt);
                    }
                    ItemDestroyPrompt.gameObject.SetActive(false);
                    PC.EnableInput(PlayerInputFlag.PLAYERITEM);
                    
                };
            }
            
        }
        catch (System.IndexOutOfRangeException e) {
            Debug.LogError("Tried to add Item at an invalid index.");
        }

    }

    private void Disk_OnDecode(string opname) {
        OC.AddOp(opname);
    }

    public void SetDestroyCallback(Item i, int position) {
        i.OnDestroy += () => {
            Items[position] = null;
            ItemUIs[position].Clear();
        };
    }
    public void SetActiveSlot(ItemSlot i, bool b) { IsItemFrameActivated[(int)i] = b; ItemUIs[(int)i].gameObject.SetActive(b); }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
