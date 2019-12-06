using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorUI : MonoBehaviour
{
    [SerializeField] List<DamageIndicatorAnimation> Frames;
    [SerializeField] List<Text> Texts;

    EntityManager EM;
    private void Awake() {
        EM = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<EntityManager>();

        EM.Player.GetComponent<EntityHealth>().OnDamagedAmount += DamageIndicatorUI_OnDamaged;
    }

    private void DamageIndicatorUI_OnDamaged(int arg1,int MaxHP, Entity arg2) {
        var v = Vector3Int.RoundToInt(Quaternion.Inverse(EM.Player.IdealRotation)*(arg2.IdealPosition - EM.Player.IdealPosition));
        var f = Util.LHUnitVectorToFace(v);
        Texts[f].text = arg1.ToString();
        Frames[f].Show(Mathf.Clamp(0.7f+0.6f*arg1/MaxHP,0,1), Mathf.Clamp(1.0f - 0.6f * arg1 / MaxHP, 0.4f, 1));

    }
}
