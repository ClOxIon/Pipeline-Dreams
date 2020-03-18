using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class DamageText : MonoBehaviour {
        [Range(0, 100)] [SerializeField] float speed;
        [Range(0, 5)] [SerializeField] float fadeSpeed;
        CanvasRenderer CR;
        float t;
        private void Awake() {
            CR = GetComponent<CanvasRenderer>();
        }
        public void Init(int damage) {
            GetComponent<Text>().text = damage.ToString();
        }
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            t += Time.deltaTime;
            transform.localPosition += new Vector3(0, 10 * speed * Time.deltaTime);
            CR.SetAlpha(1 - t * fadeSpeed);
            if (1 - t * fadeSpeed < 0)
                Destroy(gameObject);
        }
    }
}