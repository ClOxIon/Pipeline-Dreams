using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams {
    public class SoulUI : MonoBehaviour
    {
        [SerializeField] Text SoulText;
        [SerializeField] Entity Player;
        // Start is called before the first frame update
    void Awake()
        {
            Player.OnParamChange += Player_OnParamChange;
            SoulText.text = "Souls: 0";
        }

        private void Player_OnParamChange(string arg1, float arg2)
        {
            if (arg1 != "Souls") return;
            SoulText.text = "Souls: " + arg2;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}