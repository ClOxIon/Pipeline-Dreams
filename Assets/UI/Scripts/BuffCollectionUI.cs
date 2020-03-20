using UnityEngine;

namespace PipelineDreams {
    public class BuffCollectionUI : ObjectContainerUI<Buff.Buff> {

        [SerializeField] Entity.Entity Player;
        [SerializeField] BuffUI BuffUIPrefab;
        protected override void Awake() {
            PI = Player.GetComponent<Entity.BuffContainerHolder>().BuffContainer;
            PI.OnRefreshItems += PI_OnRefreshUI;
        }


        protected override void PI_OnRefreshUI(Buff.Buff[] obj) {

            if (ItemUIs.Count > obj.Length)
                for (int i = obj.Length; i < ItemUIs.Count; i++) {

                    ItemUIs[i].Clear();
                }
            if (ItemUIs.Count < obj.Length)
                for (int i = ItemUIs.Count; i < obj.Length; i++) {

                    ItemUIs.Add(Instantiate(BuffUIPrefab, transform));
                }

            for (int i = obj.Length - 1; i >= 0; i--) {
                ItemUIs[i].Refresh(obj[i]);
            }
        }
    }
}