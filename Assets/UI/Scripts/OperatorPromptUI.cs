using System;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class OperatorPromptUI : MonoBehaviour {
        public event Action<int> OnDestroyButtonClicked;
        [SerializeField] Text DescriptionText;
        [SerializeField] Text TitleText;
        [SerializeField] Image Icon;
        Instruction.ContainerPlayer OC;
        Button destroyButton;
        Instruction.Data defaultData;
        int Selection;
        public void Activate(Instruction.Data data) {
            gameObject.SetActive(true);
            defaultData = data;
            ShowOperatorInfo(data);
        }
        private void Awake() {
            OC = GetComponentInParent<Instruction.ContainerPlayer>();
            destroyButton = GetComponentInChildren<Button>();
            destroyButton.onClick.AddListener(() => OnDestroyButtonClicked(Selection));
        }
        // Start is called before the first frame update
        void Start() {

        }
        private void ShowOperatorInfo(Instruction.Data data) {
            DescriptionText.text = data.Description;
            TitleText.text = data.Name;
            Icon.sprite = data.Icon;
        }
        // Update is called once per frame
        void Update() {
            if (Input.GetButtonDown("Item0")) { Selection = 0; ShowOperatorInfo(defaultData); }
            for (int i = 1; i <= 9; i++)
                if (Input.GetButtonDown("Item" + i)) { Selection = i; ShowOperatorInfo(OC.GetItemInfo(Selection - 1) as Instruction.Data); }

        }
    }
}