﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PipelineDreams.Entity {
    public class InstructionContainerHolder : MonoBehaviour
    {
        [Tooltip("every entity, normally activated, will hold a unique copy of this Instruction Container.")] public Instruction.Container AbilityContainer;

        [Tooltip("Share the assigned container, rather than copying it.")] public bool KeepPrefab;
        private void Awake()
        {
            GetComponent<Entity>().OnInit += (tm, ec) =>
            {
                if (!KeepPrefab)
                    AbilityContainer = Instantiate(AbilityContainer);
                AbilityContainer.Init(tm, GetComponent<Entity>());
            };
        }
    }
}