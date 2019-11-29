using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectionUI : MonoBehaviour
{
    List<ItemUI> ItemUIs;
    PlayerItem PI;
    private void Awake() {
        PI = (PlayerItem)FindObjectOfType(typeof(PlayerItem));
        ItemUIs = new List<ItemUI>(GetComponentsInChildren<ItemUI>());
        PI.OnRefreshUI += PI_OnRefreshUI;
        PI.OnRefreshItemSlotUI += PI_OnRefreshItemSlotUI;
    }

    private void PI_OnRefreshItemSlotUI(bool[] obj) {
        for (int i = obj.Length - 1; i >= 0; i--) {
            ItemUIs[i].gameObject.SetActive(obj[i]);
        }
    }

    private void PI_OnRefreshUI(Item[] obj) {
        for (int i = obj.Length-1; i >= 0; i--) {
            ItemUIs[i].Refresh(obj[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
