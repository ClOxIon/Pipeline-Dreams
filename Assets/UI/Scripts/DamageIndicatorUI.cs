using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class DamageIndicatorUI : MonoBehaviour {
        [SerializeField] List<DamageIndicatorAnimation> Frames;
        [SerializeField] List<Text> Texts;
        [SerializeField] Entity.Entity Player;
        private void Awake() {

            Player.GetComponent<Entity.Health>().OnDamaged += DamageIndicatorUI_OnDamaged;
        }

        private void DamageIndicatorUI_OnDamaged(float arg1, Entity.Entity arg2) {
            var v = Vector3Int.RoundToInt(Quaternion.Inverse(Player.IdealRotation) * (arg2.IdealPosition - Player.IdealPosition));
            var f = Util.LHUnitVectorToFace(v);
            Texts[f].text = arg1.ToString();
            Frames[f].Show(Mathf.Clamp(0.7f + 0.6f * arg1 / Player.Stats["MaxHP"].Value, 0, 1), Mathf.Clamp(1.0f - 0.6f * arg1 / Player.Stats["MaxHP"].Value, 0.4f, 1));

        }
    }
}