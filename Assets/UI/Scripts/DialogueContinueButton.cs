using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContinueButton : MonoBehaviour
{
    [SerializeField] Yarn.Unity.DialogueUI dialogueUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        FindObjectOfType<PlayerInputBroadcaster>().Subscribe(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDialogueContinue() {
        dialogueUI.MarkLineComplete();
    
    }
}
