using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionInfoUI : MonoBehaviour
{
    public event Action<string> OnButtonPressed;
    public event Action<int> OnItemSelected;
    [SerializeField] Text DescriptionText;

    [SerializeField] Text AdditionalDescriptionText;
    List<SelectableItemUI> ItemUIs;
    ItemCollection IC;
    [SerializeField] Text TitleText;

    [SerializeField] List<Button> ItemButtons;
    Item Selected;
    private void Awake() {
        IC = FindObjectOfType<ItemCollection>();
        OnItemSelected += RefreshSelectionMarker;
        ItemUIs = new List<SelectableItemUI>(GetComponentsInChildren<SelectableItemUI>());
        
    }
    private void Start() {
        RefreshInfo();
        OnItemSelected?.Invoke(0);
    }
    void RefreshInfo() {
        if (Selected == null) {
            DescriptionText.text = "";
            AdditionalDescriptionText.text = "";
            TitleText.text = "Please Select an Item to Examine It.";
        } else {

            DescriptionText.text = Selected.ItData.Description;
            TitleText.text = Selected.ItData.Name;
        }
        SetItemButtons();
    }
    void RefreshSelectionMarker(int i) {
        for (int j = 0; j < ItemUIs.Count; j++)
            ItemUIs[j].SetSelection(i == j);

    }
    void SetItemButtons() {
        if (Selected == null) {
            for (int i = 0; i < ItemButtons.Count; i++) {

                ItemButtons[i].gameObject.SetActive(false);
            }
            return;
        }
        else
        for (int i = 0; i < ItemButtons.Count; i++)
            if (Selected.ItData.HasParameter("Button" + i)) {
                ItemButtons[i].gameObject.SetActive(true);
                var t = Selected.ItData.FindParameterString("Button" + i);
                ItemButtons[i].onClick.AddListener(() => OnButtonPressed?.Invoke(t));
                ItemButtons[i].GetComponentInChildren<Text>().text = t;
            } else
                ItemButtons[i].gameObject.SetActive(false);
    }
    void OnItemSlot0() {
        Selected = IC.GetItem(0);
        OnItemSelected?.Invoke(0);
        RefreshInfo();
    }
    void OnItemSlot1() {
        Selected = IC.GetItem(1);
        OnItemSelected?.Invoke(1);
        RefreshInfo();
    }
    void OnItemSlot2() {
        Selected = IC.GetItem(2);
        OnItemSelected?.Invoke(2);
        RefreshInfo();
    }
    void OnItemSlot3() {
        Selected = IC.GetItem(3);
        OnItemSelected?.Invoke(3);
        RefreshInfo();
    }
    void OnItemSlot4() {
        Selected = IC.GetItem(4);
        OnItemSelected?.Invoke(4);
        RefreshInfo();
    }
    void OnItemSlot5() {
        Selected = IC.GetItem(5);
        OnItemSelected?.Invoke(5);
        RefreshInfo();
    }
    void OnItemSlot6() {
        Selected = IC.GetItem(6);
        OnItemSelected?.Invoke(6);
        RefreshInfo();
    }
    void OnItemSlot7() {
        Selected = IC.GetItem(7);
        OnItemSelected?.Invoke(7);
        RefreshInfo();
    }
    void OnItemSlot8() {
        Selected = IC.GetItem(8);
        OnItemSelected?.Invoke(8);
        RefreshInfo();
    }
    void OnItemSlot9() {
        Selected = IC.GetItem(9);
        OnItemSelected?.Invoke(9);
        RefreshInfo();
    }
}
