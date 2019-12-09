using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class InstructionUI : MonoBehaviour, IIndividualUI<Instruction> {
        public event Action OnClick;

        [SerializeField] Image OpIcon;
        [SerializeField] GameObject CommandImagesContainer;
        [SerializeField] List<GameObject> CommandImages;
        [SerializeField] List<GameObject> CommandImagePrefabs;
        [SerializeField] List<Sprite> DirectionPrefabs;
        [SerializeField] GameObject Time1o;
        [SerializeField] GameObject Time2o;
        [SerializeField] GameObject Time3o;
        [SerializeField] GameObject Time4o;
        [SerializeField] Image Time1;
        [SerializeField] Image Time2;
        [SerializeField] Image Time3;
        [SerializeField] Image Time4;
        [SerializeField] Image Direction;
        [SerializeField] Text Hotkey;
        [SerializeField] Text OpName;
        Button b;
        protected Instruction _operator;
        private void Awake() {
            b = GetComponent<Button>();
            b.onClick.AddListener(() => OnClick?.Invoke());
        }
        public void Refresh(Instruction _o) {
            if (_o == null) {
                Clear();
                return;
            }
            SetVisible(true);
            var d = _o.OpData;
            _operator = _o;
            OpIcon.sprite = d.Icon;
            OpName.text = d.Name + " " + _o.Variant;
            foreach (var o in CommandImages)
                Destroy(o);
            CommandImages = new List<GameObject>();
            foreach (var c in _operator.GetCommandsVariant()) {

                CommandImages.Add(Instantiate(CommandImagePrefabs[(int)c], CommandImagesContainer.transform, false));
            }
            Direction.sprite = DirectionPrefabs[(int)d.Direction];
            Time1.fillAmount = Mathf.Clamp(d.Time / 8f, 0, 1);
            Time2o.SetActive(d.Time > 8);
            Time2.fillAmount = Mathf.Clamp(d.Time / 8f - 1, 0, 1);
            Time3o.SetActive(d.Time > 16);
            Time3.fillAmount = Mathf.Clamp(d.Time / 8f - 2, 0, 1);
            Time4o.SetActive(d.Time > 24);
            Time4.fillAmount = Mathf.Clamp(d.Time / 8f - 3, 0, 1);



        }
        public void AssignHotkeyUI(string keypath) {

            Hotkey.text = keypath.Split('/')[1].Substring(0, 1).ToUpper();
        }

        public void Clear() {
            SetVisible(false);
        }

        public void SetVisible(bool b) {
            gameObject.SetActive(b);
        }
    }
}