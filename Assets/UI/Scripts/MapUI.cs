using PipelineDreams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    void Awake()
    {
        GetComponent<PanelUI>().OnVisibilityChange += MapUI_OnVisibilityChange;
    }

    private void MapUI_OnVisibilityChange(bool obj)
    {
        if (obj)
        {
            foreach (var u in GetComponentsInChildren<Minimap2DUI>())
                u.Redraw();
            foreach (var u in GetComponentsInChildren<Minimap2DPlayerUI>())
                u.Redraw();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
