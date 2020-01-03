using UnityEngine;

namespace PipelineDreams {
    public class CamShakeOnHit : MonoBehaviour {
        [SerializeField] Entity Player;
        private void Awake() {
            Player.GetComponent<EntityHealth>().OnDamagedAmount += CamShakeOnHit_OnDamaged;
        }

        private void CamShakeOnHit_OnDamaged(float obj, float max, Entity e) {
            var r = obj / max;
            EZCameraShake.CameraShaker.Instance.ShakeOnce(r * 10, 1f, 0.2f, 2 * r);
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}