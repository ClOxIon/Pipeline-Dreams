using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscreteScrollUI : MonoBehaviour
{
    [SerializeField] RectTransform ItemPrefab;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField]int maxfit;
    public void Refresh() {
        scrollbar.numberOfSteps = transform.childCount - maxfit+1;
        scrollbar.size = ((float)maxfit) / transform.childCount;
        scrollbar.onValueChanged.RemoveAllListeners();
        scrollbar.value = 0;
        SetChildrenActive(0);
        scrollbar.onValueChanged.AddListener(SetChildrenActive);
        foreach (Transform t in transform) { 
        
        }
    }
    private void SetChildrenActive(float pos) {
        int step = (int)(pos * (scrollbar.numberOfSteps));
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(step <= i && i < step + maxfit);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
