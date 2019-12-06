using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : ItemUI
{
    EntityWeapon PW;
    protected override void Awake() {
        base.Awake();
        PW = FindObjectOfType<EntityManager>().Player.GetComponent<EntityWeapon>();
        PW.OnRefreshWeapon += PW_OnRefreshWeapon;
        
    }

    private void WeaponUI_OnPlayerInit() {
        
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
