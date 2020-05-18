using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscreteScrollUI : MonoBehaviour
{
    [SerializeField] RectTransform ItemPrefab;
    [SerializeField] Scrollbar scrollbar;
    public int NumItemsInView;
    public void Refresh() {
        scrollbar.numberOfSteps = transform.childCount - NumItemsInView+1;
        scrollbar.size = ((float)NumItemsInView) / transform.childCount;
        scrollbar.onValueChanged.RemoveAllListeners();
        scrollbar.value = 0;
        SetChildrenActive(0);
        scrollbar.onValueChanged.AddListener(SetChildrenActive);

        //Only activate scrollbar when childcound exceeds the number of items in a single view.
        scrollbar.gameObject.SetActive(transform.childCount > NumItemsInView);
        /*
         * This works!
        foreach (Transform t in transform) { 
        
        }
        */
    }
    private void SetChildrenActive(float pos) {
        int step = (int)(pos * (scrollbar.numberOfSteps));
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(step <= i && i < step + NumItemsInView);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
