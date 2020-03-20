using UnityEngine;

namespace PipelineDreams {
    public class WeaponUI : ItemUI {
        Entity.WeaponHolder PW;
        [SerializeField] Entity.Entity Player;
        protected override void Awake() {
            base.Awake();
            PW = Player.GetComponent<Entity.WeaponHolder>();
            PW.OnRefreshWeapon += PW_OnRefreshWeapon;
            PW.InvokeRefresh();
        }

        private void WeaponUI_OnPlayerInit() {

        }

        private void PW_OnRefreshWeapon(Item.Weapon.Weapon obj) {

            Refresh(obj);
        }

        void Start() {


        }

        // Update is called once per frame
        void Update() {

        }
    }
}