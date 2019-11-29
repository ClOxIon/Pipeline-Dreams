using UnityEngine;
using UnityEngine.UI;
public class HPBarUI : MonoBehaviour
{
    [SerializeField] RectTransform HPBar;
    [SerializeField] Image HPBarImage;
    [SerializeField] RectTransform HPBarFull;
    private void Awake() {
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>().Player.GetComponent<EntityHealth>().OnHpModified += (v) => { var s = HPBarFull.rect;HPBar.sizeDelta = new Vector2(s.width * v, s.height); HPBarImage.color = new Color(v>0.5?0:1,v>0.5?1:0,0,160f/255); };
    }

}
