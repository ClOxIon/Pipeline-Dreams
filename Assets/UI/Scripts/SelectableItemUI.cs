using UnityEngine;

namespace PipelineDreams {
    public class SelectableItemUI : ItemUI, ISelectableIndividualUI<Item.Item> {
        [SerializeField] GameObject SelectionMarker;
        public void SetSelection(bool b) {
            SelectionMarker.SetActive(b);
        }
    }
}