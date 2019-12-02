using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionInfoUI : MonoBehaviour
{
    public event Action<int> OnButtonPressed;
    [SerializeField] Text DescriptionText;

    [SerializeField] Text TitleText;

    [SerializeField] List<Button> ItemButtons;
    int SelectedItemIndex = 0;
    private void Awake() {
        for (int i = 0; i < ItemButtons.Count; i++) {
            ItemButtons[i].onClick.AddListener(() => OnButtonPressed?.Invoke(i));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
