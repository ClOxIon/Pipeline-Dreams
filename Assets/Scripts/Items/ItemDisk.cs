using System;

namespace PipelineDreams
{
    /// <summary>
    /// This item is not used anymore.
    /// </summary>
    public class ItemDisk : Item {
        public event Action<string> OnDecode;
        public float InitTime;
        public float decode;
        public string Opdata;

        public override void EffectByTime(float time) {
            base.EffectByTime(time);
            /*
            if (Slot == ItemSlot.Analyzer1 || Slot == ItemSlot.Analyzer2) {
                decode = time-InitTime; if (decode >= ItData.value1) { OnDecode?.Invoke(Opdata); Destroy(); }
            }
            */
        }


    }
}