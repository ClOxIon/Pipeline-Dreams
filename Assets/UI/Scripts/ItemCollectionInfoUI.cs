using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemCollectionInfoUI : MonoBehaviour
{
    public event Action<Item, string> OnButtonPressed;
    public event Action<int> OnItemSelected;
    [SerializeField] Text DescriptionText;
    [SerializeField] Text DirectionText;

    [SerializeField] Text AdditionalDescriptionText;
    List<SelectableItemUI> ItemUIs;
    [SerializeField] SelectableWeaponUI WeaponSlot;
    ItemCollection IC;
    ItemCollectionUI ICU;
    [SerializeField] Text TitleText;

    [SerializeField] Button ItemActionButtonPrefab;
    [SerializeField] Transform ItemActionButtonPanel;
    List<Button> ItemActionButtons= new List<Button>();
    EntityWeapon PW;
    ItemData SelectedItemData;
    int SelectedItemIndex;
    bool SwapMode=false;
    private void Awake() {
        IC = FindObjectOfType<ItemCollection>();
        ICU = GetComponent<ItemCollectionUI>();
        ICU.OnItemUIClick += (x) => OnItemSelect(x);
        PW = FindObjectOfType<EntityManager>().Player.GetComponent<EntityWeapon>();
        OnItemSelected += RefreshSelectionMarker;
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        ItemUIs = new List<SelectableItemUI>(GetComponentsInChildren<SelectableItemUI>());
        GetComponentInParent<PanelUI>().OnVisibilityChange += (b) => { if (!b) StopSwap(); };
    }
    private void Start() {
        RefreshInfo();
        OnItemSelected?.Invoke(0);
    }
    void RefreshInfo() {
        if (SelectedItemData == null) {
            DescriptionText.text = "";
            AdditionalDescriptionText.text = "";
            TitleText.text = "Please Select an Item to Examine It.";
        } else {

            DescriptionText.text = SelectedItemData.Description;
            TitleText.text = SelectedItemData.Name;
        }
        SetItemButtons();
    }
    void RefreshSelectionMarker(int i) {
        for (int j = 0; j < ItemUIs.Count; j++)
            ItemUIs[j].SetSelection(i == j);
        WeaponSlot.SetSelection(i == -1);

    }
    void SetItemButtons() {
        if (SelectedItemData == null) {
            for (int i = 0; i < ItemActionButtons.Count; i++) {

                ItemActionButtons[i].gameObject.SetActive(false);
            }
            return;
        } else {
            if (ItemActionButtons.Count < SelectedItemData.ItemActions.Length)
                for (int i = ItemActionButtons.Count; i < SelectedItemData.ItemActions.Length; i++)
                    ItemActionButtons.Add(Instantiate(ItemActionButtonPrefab, ItemActionButtonPanel));
            for (int i = 0; i < SelectedItemData.ItemActions.Length; i++) {
                ItemActionButtons[i].gameObject.SetActive(true);
                var t = SelectedItemData.ItemActions[i];
                ItemActionButtons[i].onClick.AddListener(() => IC.InvokeItemAction(SelectedItemIndex, t));
                ItemActionButtons[i].GetComponentInChildren<Text>().text = t;

                
            }
            for (int i = SelectedItemData.ItemActions.Length; i < ItemActionButtons.Count; i++) {
                ItemActionButtons[i].gameObject.SetActive(false);
            }
        }
    }
    void OnItemSelect(object value) {
        var i = (int)value;
        if (SwapMode) {
            StopSwap();
            IC.SwapItem(SelectedItemIndex, i);
        } 
            SelectedItemData = IC.GetItemInfo(i);
        
            SelectedItemIndex = i;
            
            OnItemSelected?.Invoke(i);  
        
        RefreshInfo();
    }
    void OnWeaponSlot(object value) {
        
            SelectedItemIndex = -1;
            SelectedItemData = PW.WeaponData;
            OnItemSelected?.Invoke(-1);
            RefreshInfo();
        
    }
    public void OnItemSwap() {
        SwapMode = true;
        DirectionText.text = "SELECT AN ITEM SLOT TO SWAP";
    }
    private void StopSwap() {
        SwapMode = false;
        DirectionText.text = "";
    }
}
