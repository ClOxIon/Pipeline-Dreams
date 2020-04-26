using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace PipelineDreams {
    public class CommunicationUI : MonoBehaviour {

        PanelUI p;
        [SerializeField] Text TitleText;
        [SerializeField] Text DescriptionText;
        [SerializeField] Text PressSpaceToContinue;
        [SerializeField] Entity.Container EM;
        [SerializeField] Entity.Entity Player;
        [SerializeField] DialogueRunner dialogueRunner;
        [SerializeField] Yarn.Unity.DialogueUI dialogueUI;
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
        


        private void ShowEntityDialogue(Entity.Data data) {
            TitleText.text = data.NameInGame;
            if (data.Dialogue != null)
            {
                try
                {
                    if (!dialogueRunner.Dialogue.allNodes.Contains(data.Dialogue.name))//load yarn program only if it is not already loaded.
                        dialogueRunner.Add(data.Dialogue);
                }
                catch (NullReferenceException) {//When allNodes is null, add anyway.
                    dialogueRunner.Add(data.Dialogue);
                }
                
                dialogueRunner.StartDialogue(data.Dialogue.name);
            }
            else
            {
                DescriptionText.text = "This " + data.NameInGame + " does not seem to want to talk with me....";
                PressSpaceToContinue.text = "END OF COMMUNICATION";
            }
            ConsoleUIInput.AppendText($"You conversed with {data.NameInGame}.");
        }
       
        public void HideDialogue() {

            dialogueRunner.Stop();
            foreach (var button in dialogueUI.optionButtons)
                button.gameObject.SetActive(false);//I feel like dialogueUI should do this automatically when dialogueRunner stops, but it doesn't.
        }
        public void ShowDialogue() {






            var e = EM.FindVisibleEntityOnAxis(Util.LHQToFace(Player.GetComponent<Entity.SightWithRotation>().IdealRotation), Player);
            if (e != null)
                ShowEntityDialogue(e.Data);
            


        }
    }
}