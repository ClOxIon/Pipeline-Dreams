using UnityEngine;

namespace PipelineDreams {
    public class WeaponUI : ItemUI {
        EntityWeapon PW;
        [SerializeField] Entity Player;
        protected override void Awake() {
            base.Awake();
            PW = Player.GetComponent<EntityWeapon>();
            PW.OnRefreshWeapon += PW_OnRefreshWeapon;
            PW.InvokeRefresh();
        }

        private void WeaponUI_OnPlayerInit() {

        }

        private void PW_OnRefreshWeapon(ItemWeapon obj) {

            Refresh(obj);
        }

        void Start() {


        }

        // Update is called once per frame
        void Update() {

        }
    }
}