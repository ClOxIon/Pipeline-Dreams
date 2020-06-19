using PipelineDreams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContinueButton : MonoBehaviour
{
    [SerializeField] Yarn.Unity.DialogueUI dialogueUI;

    /// <summary>
    /// Required only when this button is the part of a World Space UI.
    /// </summary>
    [SerializeField] WorldSpaceUIBehaviour WorldUI;
    
    private void Awake()
    {
        if(WorldUI!=null)
            WorldUI.OnUIEnable += WorldUI_OnUIEnable;
        else
            FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
    }

    private void WorldUI_OnUIEnable(bool obj)
    {
        if(obj)
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
        else
            FindObjectOfType<PlayerInputBroadcaster>().UnSubscribe(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDialogueContinue() {
        dialogueUI.MarkLineComplete();
    
    }
}
