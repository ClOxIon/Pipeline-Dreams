using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item {
    public ItemData ItData;
    public event Action OnRemove;
    protected TaskManager CM;
    
    public Item() {
       
    }
    public virtual string[] ItemActions { get {

            return new string[] {"Destroy"};
        } }
    public virtual string DefaultAction {
        get {

            return null;
        }
    }
    /// <summary>
    /// Called when the item is moved into player's inventory.
    /// </summary>
    /// <param name="data"></param>
    public virtual void Obtain(ItemData data) {
        ItData = data;
        ItData.ItemActions = ItemActions;
        ItData.DefaultAction = DefaultAction;
        CM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TaskManager>();
    }
    public virtual void EffectByTime(float time) { }
    /// <summary>
    /// Called when the item is removed from player's inventory.
    /// </summary>
    public virtual void Remove() {
        OnRemove?.Invoke();
    }
    public virtual void InvokeItemAction(string actionName) {
        if (actionName == "DEFAULT" && DefaultAction != null)
            InvokeItemAction(DefaultAction);
        if (actionName == "Destroy")
            Remove();
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

    public override void Obtain(ItemData data) {
        base.Obtain(data);

        PlayerMove = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>().Player.GetComponent<EntityMove>();
        PlayerMove.SpeedModifier /= 1.5f;
    }
    public override void Remove() {
        base.Remove();
        PlayerMove.SpeedModifier *= 1.5f;
        

    }


}

public class ItemWeaponRebar : ItemWeapon {

    


}


public class ItemCollection : MonoBehaviour
{
    public event Action OnItemCollectionInitialized;
    public event Action<Item[]> OnRefreshItems;
    public event Action<int> OnChangeItemSlotAvailability;
    //[SerializeField]ItemPromptUI ItemDestroyPrompt;
    [SerializeField]ItemDataset DataContainer;
    List<Item> Items = new List<Item>();
    int MaximumItemCount = 10;
    int ActivatedSlots;
    
    private void Awake() {
        
        for(int i=0;i<MaximumItemCount;i++)
        Items.Add(null);
        //CM.OnClockModified += CM_OnClockModified;
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeActivatedSlots(3);
        OnRefreshItems(Items.ToArray());
        AddItem("WeaponRebar");
        OnItemCollectionInitialized?.Invoke();
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
        if (typeof(Item).Namespace != null) {
            if (Type.GetType(typeof(Item).Namespace + ".Item" + name) != null)
                AddedItem = (Item)Activator.CreateInstance(Type.GetType(typeof(Item).Namespace + ".Item" + name));
        } else {
            if (Type.GetType("Item" + name) != null)
                AddedItem = (Item)Activator.CreateInstance(Type.GetType("Item" + name));
        }
            AddedItemData = DataContainer.Dataset.Find((x) => { return x.Name == name; });
        if (AddedItemData == null) {
            Debug.LogError("ItemCollection.Additem(): Cannot find item named "+name);
            return;
        }
        AddedItem.Obtain(AddedItemData);
        PushItem(AddedItem);

    }
    /// <summary>
    /// Subscribe to the destroy event of an item.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="position"></param>
    public void SetRemoveCallback(Item i, int position) {
        i.OnRemove += () => {
            
            Items[position] = null;
            OnRefreshItems(Items.ToArray());
        };
    }
    public void ChangeActivatedSlots(int after) {
        ActivatedSlots = after;
        if (ActivatedSlots < Items.Count) {
            for (int i = ActivatedSlots; i < Items.Count; i++) {
                Items[i]?.Remove();
            }
            Items.RemoveRange(after, Items.Count - after);
        }
        OnChangeItemSlotAvailability?.Invoke(after);
        OnRefreshItems(Items.ToArray());
    }
    /// <summary>
    /// This method is used to move an item from inventory to somewhere else.
    /// Notice that Destroy() is still called.
    /// </summary>
    /// <param name="index"></param>
    public Item PopItem(int index) {
        var i = Items[index];
        i.Remove(); 
        
        return i;
    }
    /// <summary>
    /// This method is used to move an item into inventory.
    /// </summary>
    /// <param name="item"></param>
    public void PushItem(Item item) {

       
        bool flag = true;
        for (int i = 1; i < Items.Count; i++) {
            if (Items[i] == null) {
                Items[i] = item;

                OnRefreshItems(Items.ToArray());
                SetRemoveCallback(item, i);
                flag = false;
                break;
            }

        }
        if (flag) {

            //Instead of opening Item destroy prompt, the item in the temporary slot will be overwritten without prompt.
            var i = 0;//Temporary slot
            Items[i]?.Remove();
            Items[i] = item;
            SetRemoveCallback(item, i);
            OnRefreshItems(Items.ToArray());
        }
        OnRefreshItems?.Invoke(Items.ToArray());
    }
    public ItemData GetItemInfo(int index) {
        if(index<Items.Count)
        return Items[index]?.ItData;
        return null;
    }

    internal void InvokeUIRefresh() {
        OnChangeItemSlotAvailability?.Invoke(ActivatedSlots);
        OnRefreshItems?.Invoke(Items.ToArray());

    }
    internal void InvokeItemAction(int itemIndex, string actionName) {
        if (itemIndex < Items.Count && Items[itemIndex] != null)
            Items[itemIndex].InvokeItemAction(actionName);
    }
    internal void SwapItem(int index1, int index2) {
        var tmp = Items[index1];
        Items[index1] = Items[index2];
        Items[index2] = tmp;
        InvokeUIRefresh();
    }
}
