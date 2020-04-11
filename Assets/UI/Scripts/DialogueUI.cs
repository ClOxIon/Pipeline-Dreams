using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace PipelineDreams {
    public class DialogueUI : MonoBehaviour {

        PanelUI p;
        [SerializeField] int FOVDialogue;
        [SerializeField] int FOVNormal;
        [SerializeField] Text TitleText;
        [SerializeField] Text DescriptionText;
        [Range(0, 1)] [SerializeField] float LerpSpeed;
        [SerializeField] Entity.Container EM;
        [SerializeField] Entity.Entity Player;
        [SerializeField] DialogueRunner dialogueRunner;
        [SerializeField] Yarn.Unity.DialogueUI dialogueUI;
        [SerializeField] Camera FrontCam;
        bool visible = false;
        bool isMoving = true;
        // Start is called before the first frame update
        private void Awake() {
            p = GetComponent<PanelUI>();
            p.OnVisibilityChange += P_OnVisibilityChange;
        }

        private void P_OnVisibilityChange(bool obj) {
            if (obj)
                ShowDialogue();
            else
                HideDialogue();
        }

        // Update is called once per frame
        void Update() {
            if (isMoving) {
                FrontCam.fieldOfView = Mathf.Lerp(FrontCam.fieldOfView, visible ? FOVDialogue : FOVNormal, LerpSpeed);
                if (Mathf.Abs(FrontCam.fieldOfView - (visible ? FOVDialogue : FOVNormal)) < 0.1) {
                    FrontCam.fieldOfView = visible ? FOVDialogue : FOVNormal;
                    isMoving = false;
                }
            }



        }


        private void ShowEntityDialogue(Entity.Data data) {
            TitleText.text = data.Name;
            if (data.FindParameterString("Dialogue") != null)
                dialogueRunner.StartDialogue(data.Name);
            else {
                DescriptionText.text = "This " + data.Name + " does not seem to want to talk with me....";
            }

        }
       
        public void HideDialogue() {
            isMoving = true;
            visible = false;

            dialogueRunner.Stop();
            foreach (var button in dialogueUI.optionButtons)
                button.gameObject.SetActive(false);//I feel like dialogueUI should do this automatically when dialogueRunner stops, but it doesn't.
        }
        public void ShowDialogue() {
            isMoving = true;
            visible = true;






            var e = EM.FindEntityOnAxis(Util.LHQToFace(Player.GetComponent<Entity.SightWithRotation>().CurrentIdealRotation), Player);
            if (e != null)
                ShowEntityDialogue(e.Data);
            


        }
    }
}