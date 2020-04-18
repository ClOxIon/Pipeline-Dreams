using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams
{
    /// <summary>
    /// Only one instance of this object may exist in the scene. Prints logs and executes commands.
    /// </summary>
    public class ConsoleUIInput : MonoBehaviour
    {
        [SerializeField] PanelUI UI;
        [SerializeField] InputField IF;
        [SerializeField] Text txt;
        [SerializeField] Executer exe;
        bool suspend = false;
        // Start is called before the first frame update
        private void Awake()
        {
            UI.OnVisibilityChange += UI_OnVisibilityChange;
            IF.onEndEdit.AddListener(OnEndEdit);
        }
        void Start()
        {
            
           
        }

        private void UI_OnVisibilityChange(bool obj)
        {
            if (obj)
            {
                suspend = false;
                IF.Select();
                IF.ActivateInputField();
            }
            else
            {
                suspend = true;
                IF.DeactivateInputField();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnEndEdit(string text) {
            if (suspend)
                return;
            var commands = IF.text.Split(' ');
            if (commands.Length != 0)
            {
                string[] param = new string[commands.Length - 1];
                for (int i = 0; i < commands.Length - 1; i++)
                    param[i] = commands[i + 1];
                MethodInfo mi = exe.GetType().GetMethod(commands[0]);
                if (mi != null)
                    mi.Invoke(exe, param);
                else
                    AppendText("No Command Found: "+commands[0]);
            }
            IF.text = "";
            IF.ActivateInputField();
            IF.Select();
        }
        public static void AppendText(string text) {
            FindObjectOfType<ConsoleUIInput>().txt.text += text + "\n";
        }
    }
}