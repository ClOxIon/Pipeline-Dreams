using UnityEngine;

namespace PipelineDreams {
    public class CamShakeOnHit : MonoBehaviour {
        [SerializeField] Entity.Entity Player;
        private void Awake() {
            Player.GetComponent<Entity.Health>().OnDamaged += CamShakeOnHit_OnDamaged;
        }

        private void CamShakeOnHit_OnDamaged(float obj, Entity.Entity e) {
            var r = obj / Player.Stats["MaxHP"].Value;
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