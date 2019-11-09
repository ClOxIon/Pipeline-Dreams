using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPromptUI : MonoBehaviour
{
    public event Action<int> OnDestroyButtonClicked;
    [SerializeField] Text DescriptionText;
    [SerializeField] Text TitleText;
    [SerializeField] Image Icon;
    PlayerItem PI;
    Button destroyButton;
    ItemData defaultData;
    int Selection;
    public void Activate(ItemData data) {
        gameObject.SetActive(true);
        defaultData = data;
        ShowItemInfo(data);
    }
    private void Awake() {
        PI = GetComponentInParent<PlayerItem>();
        destroyButton = GetComponentInChildren<Button>();
        destroyButton.onClick.AddListener( () => OnDestroyButtonClicked(Selection));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void ShowItemInfo(ItemData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.sprite = data.Icon;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("ItemTemp")) { Selection = -1; ShowItemInfo(defaultData); }
        for (int i = 0;i<=9;i++)
        if (Input.GetButtonDown("Item"+i)&&PI.IsItemFrameActivated[i]) { Selection = i; ShowItemInfo(PI.Items[Selection].ItData); }
        
    }
}
