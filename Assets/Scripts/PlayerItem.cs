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
    public virtual void Destroy() {
        OnDestroy?.Invoke();
    }
}

/// <summary>
/// This item is not used anymore.
/// </summary>
public class ItemDisk : Item {
    public event Action<string> OnDecode;
    public float InitTime;
    public float decode;
    public string Opdata;
    
    
    public ItemDisk() : base() {
       
    }
    public override void EffectByTime(float time) {
        base.EffectByTime(time);
        /*
        if (Slot == ItemSlot.Analyzer1 || Slot == ItemSlot.Analyzer2) {
            decode = time-InitTime; if (decode >= ItData.value1) { OnDecode?.Invoke(Opdata); Destroy(); }
        }
        */
    }
    

}
/// <summary>
/// Debug Item.
/// </summary>
public class ItemSpeedUp : Item {
    
    EntityMove PlayerMove;
    public ItemSpeedUp() : base() {

    }

    public override void Activate(ItemData data, ItemSlot slot) {
        base.Activate(data, slot);

        PlayerMove = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>().Player.GetComponent<EntityMove>();
        PlayerMove.SpeedModifier /= 1.5f;
    }
    public override void Destroy() {
        PlayerMove.SpeedModifier *= 1.5f;
        base.Destroy();

    }


}

/// <summary>
/// Once, each itemslot had different functions. Not anymore, but this enum is what remains of it.
/// </summary>
public enum ItemSlot {
Core1, Core2, Core3, Core4, Core5, Core6, Core7, Core8, Core9, Temporary

}
public class PlayerItem : MonoBehaviour
{
    public event Action<Item[]> OnRefreshUI;
    public event Action<bool[]> OnRefreshItemSlotUI;
    OperatorContainer OC;
    //[SerializeField]ItemPromptUI ItemDestroyPrompt;
    [SerializeField]ItemDataset DataContainer;
    Item[] Items = new Item[10];
    bool[] IsItemFrameActivated = new bool[10];
    
    PlayerController PC;
    ClockManager CM;
    private void Awake() {
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        CM = PC.GetComponent<ClockManager>();
        //CM.OnClockModified += CM_OnClockModified;
        OC = GameObject.FindGameObjectWithTag("OperatorRack").GetComponent<OperatorContainer>();
        
    }

    /*
    private void CM_OnClockModified(float obj) {
        for (int i = 0; i < IsItemFrameActivated.Length; i++) {
            if (Items[i] != null&& IsItemFrameActivated[i]) {
                Items[i].EffectByTime(obj);
                ItemUIs[i].Refresh(Items[i]);
            }
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < IsItemFrameActivated.Length; i++) {
            SetActiveSlot((ItemSlot)i, false);
        }
        SetActiveSlot(ItemSlot.Core1, true);
        SetActiveSlot(ItemSlot.Core2, true);
        SetActiveSlot(ItemSlot.Temporary, true);
        OnRefreshUI(Items);
    }
    /// <summary>
    /// Adds the item corresponding to the name to the inventory.
    /// </summary>
    /// <param name="name"></param>
    public void AddItem(string name) {
        var Disktest = name.Split(' ')[0];
        Item AddedItem;
        ItemData AddedItemData;
        
            
            if (typeof(Item).Namespace != null)
                AddedItem = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + ".Item" + name));
            else
                AddedItem = (Item)Activator.CreateInstance(Type.GetType("Item" + name));
            AddedItemData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
           
        
        try {
            
            bool flag = false;
            for (int position = 0; position < Items.Length; position++)
                if (Items[position] == null&&IsItemFrameActivated[position]) {
                    AddedItem.Activate(AddedItemData, (ItemSlot)position);
                    Items[position] = AddedItem;
                    OnRefreshUI(Items);
                    SetDestroyCallback(Items[position], position);
                    flag = true;
                    break;
                    
                }
            if (!flag) {
                //No Itemslot available
                /*
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
                */
                //Instead of opening Item destroy prompt, the item in the temporary slot will be overwritten without prompt.
                var i = (int)ItemSlot.Temporary;
                Items[i].Destroy();
                Items[i] = AddedItem;
                AddedItem.Activate(AddedItemData, (ItemSlot)i);
                SetDestroyCallback(AddedItem, i);
                OnRefreshUI(Items);
            }
            
        }
        catch (System.IndexOutOfRangeException e) {
            Debug.LogError("Tried to add Item at an invalid index.");
        }

    }
    /// <summary>
    /// Subscribe to the destroy event of an item.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="position"></param>
    public void SetDestroyCallback(Item i, int position) {
        i.OnDestroy += () => {
            Items[position] = null;
            OnRefreshUI(Items);
        };
    }
    public void SetActiveSlot(ItemSlot i, bool b) { IsItemFrameActivated[(int)i] = b; OnRefreshItemSlotUI(IsItemFrameActivated); }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
