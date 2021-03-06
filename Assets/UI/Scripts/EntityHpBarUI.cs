﻿using UnityEngine;

namespace PipelineDreams {
    public class EntityHpBarUI : MonoBehaviour {
        [SerializeField] RectTransform HPBar;
        [SerializeField] RectTransform HPBarBackground;
        [SerializeField] RectTransform HPBarFull;
        [SerializeField] DamageText DT;
        private void Awake() {
            var esb = GetComponentInParent<EntityStatusBar>();
            esb.OnInit += () => {
                var health = esb.entity.GetComponent<Entity.Health>();
                if (health == null)
                {
                    gameObject.SetActive(false);
                    return;
                }
                esb.entity.OnParamChange += (name, hp) => {
                    if (name != "HP") return;
                    var v = Mathf.Clamp01(hp / esb.entity.Stats["MaxHP"].Value);
                    var s = HPBarFull.rect; HPBar.sizeDelta = new Vector2(s.width * v, 0);
                    HPBarBackground.sizeDelta = new Vector2(s.width * (1 - v), 0); HPBar.gameObject.SetActive(v != 1);
                };
                health.OnDamaged += (d, e) => 
                {
                    var obj = Instantiate(DT, transform, false);
                    obj.Init((int)d);
                    obj.transform.localPosition = new Vector3(0, 100, 0);

                };
            };
        }
    }
}