using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PipelineDreams {
    public class UIPanelController : MonoBehaviour {
        [SerializeField] GameObject HUD;
        [SerializeField] PanelUI DUI;
        [SerializeField] PanelUI IUI;//Currently Not Used
        [SerializeField] PanelUI OUI;
        [SerializeField] PanelUI MUI;
        [SerializeField] PanelUI CUI;
        [SerializeField] Image HUDIcon;
        [SerializeField] Text HUDHotkey;
        [SerializeField] Image InstIcon;
        [SerializeField] Text InstHotkey;
        [SerializeField] Image MapIcon;
        [SerializeField] Text MapHotkey;
        [SerializeField] Image DialIcon;
        [SerializeField] Text DialHotkey;
        [SerializeField] float MoveAmount;

        [SerializeField] float Offset;
        [SerializeField] float ScaleChange;
        PlayerInput PI;
        PlayerInputBroadcaster PC;
        private void Awake() {
            PC = FindObjectOfType<PlayerInputBroadcaster>();
            PC.Subscribe(gameObject);

            PI = GetComponent<PlayerInput>();
            Refresh();
            HUDIcon.GetComponent<Button>().onClick.AddListener(OnHUD);

            InstIcon.GetComponent<Button>().onClick.AddListener(OnInstructionMenu);

            MapIcon.GetComponent<Button>().onClick.AddListener(OnMinimap);

            DialIcon.GetComponent<Button>().onClick.AddListener(OnInteraction);
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
            Focus(HUDIcon);
            DeFocus(InstIcon);
            DeFocus(MapIcon);
            DeFocus(DialIcon);
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
            DeFocus(HUDIcon);
            Focus(InstIcon);
            DeFocus(MapIcon);
            DeFocus(DialIcon);
        }
        void OnMinimap() {
            HUD.SetActive(false);
            IUI.HidePanel();
            OUI.HidePanel();
            MUI.ShowPanel();
            DUI.HidePanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
            DeFocus(HUDIcon);
            DeFocus(InstIcon);
            Focus(MapIcon);
            DeFocus(DialIcon);
        }
        void OnInteraction() {
            HUD.SetActive(false);
            IUI.HidePanel();
            OUI.HidePanel();
            MUI.HidePanel();
            DUI.ShowPanel();
            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, false);
            DeFocus(HUDIcon);
            DeFocus(InstIcon);
            DeFocus(MapIcon);
            Focus(DialIcon);
        }
        void OnConsole() {

            if (CUI.visible)
                CUI.HidePanel();
            else
                CUI.ShowPanel();

            PC.SetPlayerInputEnabled(PlayerInputFlag.UIPANEL, !CUI.visible);
        }
        void Focus(Component rect) {
            var rec = rect.GetComponent<RectTransform>();
            rec.anchoredPosition = new Vector3(Offset+MoveAmount, rec.anchoredPosition.y);

            rec.localScale = new Vector3(1 + ScaleChange, 1 + ScaleChange, 1);
        }
        void DeFocus(Component rect)
        {
            var rec = rect.GetComponent<RectTransform>();
            rec.anchoredPosition = new Vector3(Offset, rec.anchoredPosition.y);

            rec.localScale = new Vector3(1, 1, 1);
        }
        public void Refresh() {
            var PA = FindObjectOfType<PlayerInput>().actions;
            
        HUDHotkey.text = PA.FindAction("HUD").bindings[0].path.Split('/')[1].ToUpper();//.Substring(0, 1).ToUpper()
            InstHotkey.text = PA.FindAction("InstructionMenu").bindings[0].path.Split('/')[1].ToUpper();
            MapHotkey.text = PA.FindAction("Minimap").bindings[0].path.Split('/')[1].ToUpper();
            DialHotkey.text = PA.FindAction("Interaction").bindings[0].path.Split('/')[1].ToUpper();
        }
    }
}