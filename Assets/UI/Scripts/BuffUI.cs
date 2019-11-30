using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    [SerializeField] Image Icon;

     Buff item;
    private void Awake() {
        Icon.enabled = false;
    }
    internal void Refresh(Buff b) {
        item = b;

        if (item == null) {
            Clear();
            return;
        }
        Icon.sprite = b.BuData.Icon;
        Icon.color = new Color(1, 1, 1, 0.7f);
        Icon.enabled = true;
        if(typeof(Item)==typeof(TimedBuff))
        Icon.fillAmount = ((TimedBuff)item).TimeLeft / item.BuData.baseDuration;
    }
    public void Clear() {
        Icon.sprite = null;
        Icon.enabled = false;
    }
}
