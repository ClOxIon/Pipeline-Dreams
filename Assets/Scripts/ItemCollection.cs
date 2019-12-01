using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item {
    public ItemData ItData;
    public event Action OnDestroy;
    protected ClockManager CM;
    public Item() {
       
    }
    public virtual void Activate(ItemData data) {
        ItData = data;
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

    public override void Activate(ItemData data) {
        base.Activate(data);

        PlayerMove = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>().Player.GetComponent<EntityMove>();
        PlayerMove.SpeedModifier /= 1.5f;
    }
    public override void Destroy() {
        PlayerMove.SpeedModifier *= 1.5f;
        base.Destroy();

    }


}


public class ItemCollection : MonoBehaviour
{
    const int TEMPSLOT = 0;

    public event Action<Item[]> OnRefreshItems;
    public event Action<int> OnChangeItemSlotAvailability;
    InstructionCollection OC;
    //[SerializeField]ItemPromptUI ItemDestroyPrompt;
    [SerializeField]ItemDataset DataContainer;
    List<Item> Items = new List<Item>();
    int MaximumItemCount = 10;
    int ActivatedSlots;
    
    PlayerController PC;
    ClockManager CM;
    private void Awake() {
        PC = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<PlayerController>();
        CM = PC.GetComponent<ClockManager>();
        //CM.OnClockModified += CM_OnClockModified;
        OC = GameObject.FindGameObjectWithTag("OperatorRack").GetComponent<InstructionCollection>();
        
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
        ChangeActivatedSlots(3);
        OnRefreshItems(Items.ToArray());
    }
    /// <summary>
    /// Adds the item corresponding to the name to the inventory.
    /// </summary>
    /// <param name="name"></param>
    public void AddItem(string name) {
        var Disktest = name.Split(' ')[0];
        Item AddedItem;
        ItemData AddedItemData;

        AddedItem = new Item();//If the item class for the specific item is not defined, we will just use the base class.
        if (typeof(Item).Namespace != null)
            if(Type.GetType(typeof(Item).Namespace + ".Item" + name)!=null)
            AddedItem = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + ".Item" + name));
        else {
            if (Type.GetType("Item" + name) != null)
            AddedItem = (Item)Activator.CreateInstance(Type.GetType("Item" + name));
        }
            AddedItemData = DataContainer.Dataset.Find((x) => { return x.Name == name; });


        if (Items.Count<ActivatedSlots) {
            Items.Add(AddedItem);
            AddedItem.Activate(AddedItemData);
            
            OnRefreshItems(Items.ToArray());
            SetDestroyCallback(AddedItem, Items.Count - 1);
            

        }
           else {
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
            var i = TEMPSLOT;//Temporary slot
                Items[i].Destroy();
                Items[i] = AddedItem;
                AddedItem.Activate(AddedItemData);
                SetDestroyCallback(AddedItem, i);
                OnRefreshItems(Items.ToArray());
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
            OnRefreshItems(Items.ToArray());
        };
    }
    public void ChangeActivatedSlots(int after) {
        ActivatedSlots = after;
        if (ActivatedSlots < Items.Count) {
            for (int i = ActivatedSlots; i < Items.Count; i++) {
                Items[i].Destroy();
            }
            Items.RemoveRange(after, Items.Count - after - 1);
        }
        OnChangeItemSlotAvailability?.Invoke(after);
        OnRefreshItems(Items.ToArray());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
