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

    [SerializeField] Text AdditionalDescriptionText;
    List<SelectableItemUI> ItemUIs;
    ItemCollection IC;
    [SerializeField] Text TitleText;

    [SerializeField] Button ItemActionButtonPrefab;
    [SerializeField] Transform ItemActionButtonPanel;
    List<Button> ItemActionButtons= new List<Button>();
    ItemData SelectedItemData;
    int SelectedItemIndex;
    private void Awake() {
        IC = FindObjectOfType<ItemCollection>();
        OnItemSelected += RefreshSelectionMarker;
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        ItemUIs = new List<SelectableItemUI>(GetComponentsInChildren<SelectableItemUI>());
        
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
    void OnItemSlot(object value) {
        var i = (int)value;
        SelectedItemIndex = i;
        SelectedItemData = IC.GetItemInfo(i);
        OnItemSelected?.Invoke(i);
        RefreshInfo();
    }
}
