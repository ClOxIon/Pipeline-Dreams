using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : ItemUI
{
    EntityWeapon PW;
    protected override void Awake() {
        base.Awake();
        FindObjectOfType<EntityManager>().OnPlayerReferenceSet += WeaponUI_OnPlayerInit;
    }

    private void WeaponUI_OnPlayerInit() {
        PW = FindObjectOfType<EntityManager>().Player.GetComponent<EntityWeapon>();
        PW.OnRefreshWeapon += PW_OnRefreshWeapon;
    }

    private void PW_OnRefreshWeapon(ItemWeapon obj) {
        
        Refresh(obj);
    }

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
