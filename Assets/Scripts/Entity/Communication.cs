using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace PipelineDreams.Entity
{
    interface ISensoryDevice {
        CommunicationMode mode { get; }
        bool IsVisible(Entity e);
        //When activated, sensory devices could see otherwise invisible entities in exchange of some noise radiation.
        //Vision: flashlight. use light field.


    }
    public enum CommunicationMode {vision, audio, radio }
    public class SignalSource {
        public CommunicationMode mode;
        public float strength;
    
    }
    public class Communication : MonoBehaviour
    {
        TaskManager TM;
        float lastClock = 0;
        List<SignalSource> Sources = new List<SignalSource>();
        //SOURCE
        //List<Source> <-sources could be added by other behaviours with duration. 
        //strength, type, duration
        //Turning on and off takes one turn!

        //RECIEVER-> ISensoryDevice: look at the scripts sight, radio, audio
        //Whenever a source is nearby, it become visible to ISensoryDevice objects,

        //Dialogue
        //player can start dialogue when player is visible for the target, target is actual entity source not reflected signal, and the target has dialogue.

        //Radar is implemented in separate script, with produces some signal sources in exchange to spotting nearby entities.
        private void Awake()
        {
            GetComponent<Entity>().OnInit += Communication_OnInit;
        }

        private void Communication_OnInit(TaskManager arg1, Container arg2)
        {
            TM = arg1;
            TM.OnClockModified += TM_OnClockModified;
            Sources.Add(new SignalSource() { mode = CommunicationMode.audio, strength = 0 });
            Sources.Add(new SignalSource() { mode = CommunicationMode.radio, strength = 0 });
            Sources.Add(new SignalSource() { mode = CommunicationMode.vision, strength = 0 });
        }

        private void TM_OnClockModified(float obj)
        {
            var deltaClock = obj - lastClock;
            foreach (var x in Sources)
                x.strength -= deltaClock;
            lastClock = obj;
        }
        public void Addsource(SignalSource newSource) {
            float tmp = 0;
            for(int i = 0;i<Sources.Count;i++)
                if (Sources[i].mode== newSource.mode) {
                    tmp = Sources[i].strength;
                    Sources.RemoveAt(i);
                    i--;
                }
            newSource.strength += tmp;
            Sources.Add(newSource);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
