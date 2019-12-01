using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeath : MonoBehaviour
{
    public event Action<Entity> OnEntityDeath;
    private void Awake() {
        GetComponent<EntityHealth>().OnZeroHP += Death;
    }
    void Death() {
        OnEntityDeath?.Invoke(GetComponent<Entity>());
        GetComponent<Entity>().IsActive = false;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
