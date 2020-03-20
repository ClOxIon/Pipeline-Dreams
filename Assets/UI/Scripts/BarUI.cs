using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams
{
    public class BarUI : MonoBehaviour
    {
        [SerializeField] RectTransform BarTransform;
        [SerializeField] Image BarImage;
        [SerializeField] Image BarFrame;
        [SerializeField] RectTransform HPBarFull;
        [SerializeField] Entity.Entity Player;
        [SerializeField] string ParamName;
        [SerializeField] string MaxParamName;
        [SerializeField] Color NormalColor;
        [SerializeField] Color CriticalColor;
        [SerializeField] float CriticalRatio;
        private void Awake() {
            
            Player.OnParamChange += (name, energy) => { if (name != ParamName) return; var s = HPBarFull.rect; var v = energy / Player.Stats[MaxParamName].Value; BarTransform.sizeDelta = new Vector2(s.width * v, s.height); BarImage.color = v > CriticalRatio ? NormalColor : CriticalColor; BarFrame.color = v > CriticalRatio ? NormalColor : CriticalColor; };
        }

    }
}