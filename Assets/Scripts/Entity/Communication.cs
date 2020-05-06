using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity
{
    enum CommunicationMode {vision, audio, radio }
    public class Communication : MonoBehaviour
    {
        //SOURCE
        //strength, type, isRadar,
        //Turning on and off takes one turn!

        //RECIEVER
        //Whenever a source is nearby, it is regietared in the visible entities list of this entity.

        //Dialogue
        //player can start dialogue when player is visible for the target, target is actual entity source not reflected signal, and the target has dialogue.

        //Reflective Property (Maybe on another script)
        //Whenever a radar source is nearby, this entity became a source for the radar. 
        //Note that this entity isnt a source for other entities, simply because that does not make much gameplay sense.


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
