using UnityEngine;

namespace PipelineDreams {
    public class DamageIndicatorAnimation : MonoBehaviour {
        float alpha = 0;
        [SerializeField] Vector2 variation;
        Vector2 position;
        [Range(0, 5)] [SerializeField] public float moveSpeed = 1;
        [Range(0, 5)] [SerializeField] public float speed = 1;
        CanvasGroup CG;
        float p;
        RectTransform RT;
        private void Awake() {
            RT = GetComponent<RectTransform>();
            position = RT.localPosition;
            CG = GetComponent<CanvasGroup>();
        }
        public void Show(float a, float s) {
            gameObject.SetActive(true);
            alpha = a;
            speed = s;
            RT.localPosition = position + variation;
            p = 1;
        }
        // Update is called once per frame
        void Update() {
            if (alpha > 0) {
                RT.localPosition = position + variation * Mathf.Clamp(p, 0, 1);
                p -= moveSpeed;

                alpha -= speed * Time.deltaTime;
                if (alpha <= 0) {
                    alpha = 0;
                    gameObject.SetActive(false);
                }
                CG.alpha = alpha;

            }
        }
    }
}