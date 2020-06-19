using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace PipelineDreams {
    public class ExamUI : MonoBehaviour {

        PanelUI p;
        [SerializeField] int FOVDialogue;
        [SerializeField] int FOVNormal;
        [SerializeField] Text TitleText;
        [SerializeField] Text DescriptionText;
        [Range(0, 1)] [SerializeField] float LerpSpeed;
        [SerializeField] Entity.Container EM;
        [SerializeField] Entity.Entity Player;
        [SerializeField] Camera FrontCam;
        bool isMoving = true;
        // Start is called before the first frame update
        private void Awake() {
            p = GetComponent<PanelUI>();
            p.OnVisibilityChange += P_OnVisibilityChange;
        }

        private void P_OnVisibilityChange(bool obj) {
            if (obj)
                ShowDescription();
            else
                HideDescription();
        }

        // Update is called once per frame
        void Update() {
            if (isMoving) {
                
                FrontCam.fieldOfView = Mathf.Lerp(FrontCam.fieldOfView, p.visible ? FOVDialogue : FOVNormal, LerpSpeed);
                if (Mathf.Abs(FrontCam.fieldOfView - (p.visible ? FOVDialogue : FOVNormal)) < 0.1) {
                    FrontCam.fieldOfView = p.visible ? FOVDialogue : FOVNormal;
                    isMoving = false;
                }
            }

        }


        private void ShowEntityDescription(Entity.Data data) {
            TitleText.text = data.NameInGame;
            
                DescriptionText.text = data.Description;
            
            ConsoleUIInput.AppendText($"You examined {data.NameInGame}.");
        }
       
        public void HideDescription() {
            isMoving = true;
        }
        public void ShowDescription() {
            isMoving = true;






            var e = EM.FindEntityRelative(Vector3Int.zero,Util.LHQToFace(Player.GetComponent<Entity.SightWithRotation>().IdealRotation), Entity.EntityType.TILE, Player);
            if (e != null)
                ShowEntityDescription(e.Data);
            


        }
    }
}