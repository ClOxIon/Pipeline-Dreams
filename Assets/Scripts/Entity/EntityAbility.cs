using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams {
    public class EntityAbility : MonoBehaviour
    {
     [Tooltip("every entity, normally activated, will hold a unique copy of this Instruction Container.")] public InstructionContainer AbilityContainer;
        private void Awake() {
            GetComponent<Entity>().OnInit += (tm, mc, ec) => AbilityContainer = Instantiate(AbilityContainer);
        }
    }
}