using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams
{
    public class HPBarUI : MonoBehaviour
    {
        [SerializeField] RectTransform HPBar;
        [SerializeField] Image HPBarImage;
        [SerializeField] RectTransform HPBarFull;
        [SerializeField] Entity Player;
        private void Awake() {
            
            Player.OnParamChange += (name, hp) => { if (name != "HP") return; var s = HPBarFull.rect; var v = hp / Player.Stats["MaxHP"].Value; HPBar.sizeDelta = new Vector2(s.width * v, s.height); HPBarImage.color = new Color(v > 0.5 ? 0 : 1, v > 0.5 ? 1 : 0, 0, 160f / 255); };
        }

    }
}