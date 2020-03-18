using UnityEngine;
using UnityEngine.InputSystem;

namespace PipelineDreams {
    public class UIPanelController : MonoBehaviour {
        [SerializeField] GameObject HUD;
        [SerializeField] PanelUI DUI;
        [SerializeField] PanelUI IUI;//Currently Not Used
        [SerializeField] PanelUI OUI;
        [SerializeField] PanelUI MUI;
        [SerializeField] PanelUI CUI;
        PlayerInput PI;
        PlayerInputBroadcaster PC;
        private void Awake() {
            PC = FindObjectOfType<PlayerInputBroadcaster>();
            PC.Subscribe(gameObject);

            PI = GetComponent<PlayerInput>();

        }
        // Start is called before the first frame update
        void Start() {


            //We sequancially activate all UI panels to initialize them.
            IUI.ShowPanel();
            OUI.ShowPanel();
            DUI.ShowPanel();
            MUI.ShowPanel();
            CUI.ShowPanel();
            CUI.HidePanel();
            OnHUD();
        }


        void OnHUD() {
            HUD.SetActive(true);
            IUI.HidePanel();
            OUI.HidePanel();
            MUI.HidePanel();
            DUI.HidePanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, true);
        }
        void OnItemMenu() {
            HUD.SetActive(false);
            IUI.ShowPanel();
            OUI.HidePanel();
            MUI.HidePanel();
            DUI.HidePanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
        }
        void OnInstructionMenu() {
            HUD.SetActive(false);
            IUI.HidePanel();
            OUI.ShowPanel();
            MUI.HidePanel();
            DUI.HidePanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
        }
        void OnMinimap() {
            HUD.SetActive(false);
            IUI.HidePanel();
            OUI.HidePanel();
            MUI.ShowPanel();
            DUI.HidePanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
        }
        void OnInteraction() {
            HUD.SetActive(false);
            IUI.HidePanel();
            OUI.HidePanel();
            MUI.HidePanel();
            DUI.ShowPanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
        }
        void OnConsole() {

            if (CUI.visible)
                CUI.HidePanel();
            else
                CUI.ShowPanel();

            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, !CUI.visible);
        }
    }
}