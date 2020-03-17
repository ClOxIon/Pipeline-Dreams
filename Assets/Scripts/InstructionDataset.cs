using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PipelineDreams {
    public enum OpDirection { Front, Back, Omni }

    [CreateAssetMenu(fileName = "OpData", menuName = "ScriptableObjects/OperatorData", order = 1)]
    public class InstructionDataset : ScriptableObject, IPDDataSet
    {
        public List<PDData> DataSet
        {
            get
            {
                var d = new List<PDData>();
                foreach (var x in dataSet)
                    d.Add(x);
                return d;
            }
        }

        [SerializeField] private List<InstructionData> dataSet;
    }
    [System.Serializable]
    public class InstructionData : PDData {

        [SerializeField] private float time;
        [SerializeField] private OpDirection direction;
        [SerializeField] private List<Command> commands;
        [SerializeField] private List<string> variants;
        [SerializeField] private float meleeCoef;

        [SerializeField] private float rangeCoef;

        [SerializeField] private float fieldCoef;

        [SerializeField] private float baseAccuracy;
        [SerializeField] private EffectVisualizer gun;
        [SerializeField] private float effectDuration;

        public float Time => time;
        public OpDirection Direction => direction;
        public Command[] Commands => commands.ToArray();
        public string[] Variants => variants.ToArray();
        public float MeleeCoef => meleeCoef;
        public float RangeCoef => rangeCoef;
        public float FieldCoef => fieldCoef;
        public float BaseAccuracy => baseAccuracy;
        public EffectVisualizer Gun => gun;
        public float EffectDuration => effectDuration;
    }
}