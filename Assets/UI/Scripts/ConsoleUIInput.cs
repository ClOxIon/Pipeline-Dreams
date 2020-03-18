using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace PipelineDreams
{
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
            txt.text += IF.text + "\n";
            var commands = IF.text.Split(' ');
            if (commands.Length != 0)
            {
                string[] param = new string[commands.Length - 1];
                for (int i = 0; i < commands.Length - 1; i++)
                    param[i] = commands[i + 1];
                MethodInfo mi = exe.GetType().GetMethod(commands[0]);
                mi.Invoke(exe, param);
            }
            IF.text = "";
            IF.Select();
        }
    }
}