using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItemUI : ItemUI
{
    [SerializeField] GameObject SelectionMarker;
    public void SetSelection(bool b) {
        SelectionMarker.SetActive(b&&item!=null);
    }
}
