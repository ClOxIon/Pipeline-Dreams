using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSwapButtonUI : MonoBehaviour
{
    [SerializeField] Text Hotkey;
    Button b;
    private void Awake() {
        b = GetComponent<Button>();
        b.onClick.AddListener(FindObjectOfType<ItemCollectionInfoUI>().OnItemSwap);
    }

    public void AssignHotkeyUI(string keypath) {

        Hotkey.text = keypath.Split('/')[1].Substring(0, 1);
    }
}
