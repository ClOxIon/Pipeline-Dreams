using PipelineDreams.Entity;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Yarn.Unity;

namespace PipelineDreams {
    public class CommunicationUI : MonoBehaviour {

        PanelUI p;
        [SerializeField]PlayerInputBroadcaster PI;
        [SerializeField] Container EM;
        [SerializeField] Entity.Entity Player;

        //Dialogue---------------------------------------------------------------------
        [SerializeField] GameObject DialoguePanel;
        [SerializeField] Text TitleText;
        [SerializeField] Text DescriptionText;
        [SerializeField] Text PressSpaceToContinue;
        [SerializeField] DialogueRunner dialogueRunner;
        [SerializeField] DialogueUI dialogueUI;

        //Entify Selection-------------------------------------------------------------
        [SerializeField] GameObject EntitySelectionPanel;
        [SerializeField] DiscreteScrollUI EntitySelectionScrollUI;
        [SerializeField] Button EntitySelectionPrefab;
        [SerializeField] Text EntitySelectionSeparatorPrefab;


        //Signal Info------------------------------------------------------------------
        [SerializeField] GameObject ConnectionInfoPanel;
        [SerializeField] Text ConnectionIdentification;//Could be unknown
        [SerializeField] Text ConnectionStatus;//
        [SerializeField] Text ConnectionDirection;
        [SerializeField] Text ConnectionStrength;
        [SerializeField] Text ConnectionMessage;//Could be incomprehendable
        [SerializeField] Button OpenDialogueButton;
        //-----------------------------------------------------------------------------
        // Start is called before the first frame update
        private void Awake() {
            p = GetComponent<PanelUI>();
            p.OnVisibilityChange += P_OnVisibilityChange;
        }

        private void P_OnVisibilityChange(bool obj) {
            if (obj)
                PanelOpen();
            else
                PanelClose();
        }

        // Show the list of visible entities for each sensor.
        private void ShowEntityList() {
            while (0 < EntitySelectionPanel.transform.childCount)
            {
                var tr = EntitySelectionPanel.transform.GetChild(0);
                tr.SetParent(transform.parent,false);//We have to unparent it first, because otherwise childcount does not work properly.
                Destroy(tr.gameObject);
            }
            var sensors = Player.GetComponents<ISensoryDevice>();
            foreach (var x in sensors) {
                var t = Instantiate(EntitySelectionSeparatorPrefab, EntitySelectionPanel.transform);
                t.text = x.mode.ToString();
                foreach (var entity in EM.FindEntities((e) => x.IsVisible(e))) {
                    var b = Instantiate(EntitySelectionPrefab, EntitySelectionPanel.transform);
                    b.GetComponentInChildren<Text>().text = entity.Data.NameInGame;
                    //Add Some Additional Information
                    b.onClick.AddListener(() => ShowConnectionPanel(entity));
                }
            }

            //Create dummy buttons to fill the list;
            for (int i = EntitySelectionPanel.transform.childCount; i < EntitySelectionScrollUI.NumItemsInView; i++)
            {
                var b = Instantiate(EntitySelectionPrefab, EntitySelectionPanel.transform);
                b.GetComponentInChildren<Text>().text = "";
            }
            EntitySelectionScrollUI.Refresh();
        }
        private void ShowConnectionPanel(Entity.Entity e) {
            ConnectionInfoPanel.SetActive(true);
            ConnectionIdentification.text = e.Data.NameInGame;
            OpenDialogueButton.onClick.RemoveAllListeners();
            OpenDialogueButton.onClick.AddListener(() => ShowEntityDialogue(e.Data));

            //Randomly generate ConnectionMessage



            //Status: Connection Established if the opponent could see me& there is a dialogue.
            //Status: Unknown Protocol
            //Status: Noise
            //Status: No Response Otherwise.
        }

        private void ShowEntityDialogue(Entity.Data data) {
            DialoguePanel.SetActive(true);
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
                PI.SetInputEnabledDuringDialogue(false);
                dialogueRunner.StartDialogue(data.Dialogue.name);
            }
            else
            {
                DescriptionText.text = "This " + data.NameInGame + " does not seem to want to talk with me....";
                PressSpaceToContinue.text = "END OF COMMUNICATION";
            }
            ConsoleUIInput.AppendText($"You conversed with {data.NameInGame}.");
        }
        public void OnDialogueFinish() {
            PI.SetInputEnabledDuringDialogue(true);
        }
       
        public void PanelClose() {

            ClearPanels();
            
        }
        public void ClearPanels() {
            
            dialogueRunner.Stop();
            foreach (var button in dialogueUI.optionButtons)
                button.gameObject.SetActive(false);//I feel like dialogueUI should do this automatically when dialogueRunner stops, but it doesn't.
            ConnectionInfoPanel.SetActive(false);
            DialoguePanel.SetActive(false);
        }
        public void PanelOpen() {





            
            ShowEntityList();
            


        }
    }
}