using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperatorPromptUI : MonoBehaviour {
    public event Action<int> OnDestroyButtonClicked;
    [SerializeField] Text DescriptionText;
    [SerializeField] Text TitleText;
    [SerializeField] Image Icon;
    InstructionCollection OC;
    Button destroyButton;
    InstructionData defaultData;
    int Selection;
    public void Activate(InstructionData data) {
        gameObject.SetActive(true);
        defaultData = data;
        ShowOperatorInfo(data);
    }
    private void Awake() {
        OC = GetComponentInParent<InstructionCollection>();
        destroyButton = GetComponentInChildren<Button>();
        destroyButton.onClick.AddListener(() => OnDestroyButtonClicked(Selection));
    }
    // Start is called before the first frame update
    void Start() {

    }
    private void ShowOperatorInfo(InstructionData data) {
        DescriptionText.text = data.Description;
        TitleText.text = data.Name;
        Icon.sprite = data.Icon;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Item0")) { Selection = 0; ShowOperatorInfo(defaultData); }
        for (int i = 1; i <= 9; i++)
            if (Input.GetButtonDown("Item" + i)) { Selection = i; ShowOperatorInfo(OC.Instructions[Selection - 1].OpData); }

    }
}

