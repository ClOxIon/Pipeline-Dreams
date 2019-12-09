using UnityEngine;

namespace PipelineDreams {
    public class EntityHpBarUI : MonoBehaviour {
        [SerializeField] RectTransform HPBar;
        [SerializeField] RectTransform HPBarBackground;
        [SerializeField] RectTransform HPBarFull;
        [SerializeField] DamageText DT;
        private void Awake() {
            var esb = GetComponentInParent<EntityStatusBar>();
            esb.OnInit += () => {
                esb.entity.GetComponent<EntityHealth>().OnHpModified += (v) => {
                    v = Mathf.Clamp01(v);
                    var s = HPBarFull.rect; HPBar.sizeDelta = new Vector2(s.width * v, 0);
                    HPBarBackground.sizeDelta = new Vector2(s.width * (1 - v), 0); HPBar.gameObject.SetActive(v != 1);
                };
                esb.entity.GetComponent<EntityHealth>().OnDamagedAmount += (d, h, e) => {
                    var obj = Instantiate(DT, transform, false);
                    obj.Init(d);
                    obj.transform.localPosition = new Vector3(0, 100, 0);

                };
            };
        }
    }
}