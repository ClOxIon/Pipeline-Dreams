using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    Animator an;
    public event Action<string, bool> OnAnimate;
    public Action OnDeathClipExit;
    private void Awake() {
        an = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InvokeAnimation(string name, bool b) {
        OnAnimate?.Invoke(name, b);
        an.SetBool(name, b);
    }
}
