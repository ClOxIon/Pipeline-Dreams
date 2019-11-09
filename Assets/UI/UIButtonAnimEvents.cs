using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAnimEvents : MonoBehaviour
{
    [SerializeField] GameObject SelectionText;
    Button b;
    Text t;
    string originalText;
    // Start is called before the first frame update
    void Start()
    {
        b = GetComponent<Button>();
        t = GetComponentInChildren<Text>();
        if(t!=null)
        originalText = t.text;
    }
    void Highlighted() {
        SelectionText.SetActive(true);
    }
    void DeHighlighted() {
        SelectionText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
