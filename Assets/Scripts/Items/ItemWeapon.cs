using System.Linq;

namespace PipelineDreams {
    public class ItemWeapon : Item {
        public ItemWeapon(Entity player, TaskManager cM, ItemData data) : base(player, cM, data) {
        }

        public override string[] ItemActions => base.ItemActions.Concat(new string[] { "Equip", "UnEquip" }).ToArray();
        public int MeleeDamage { get; protected set; }

        public int RangeDamage { get; protected set; }

        public int FieldDamage { get; protected set; }
        // Start is called before the first frame update
        public override void Obtain() {
            base.Obtain();
            MeleeDamage = ItData.FindParameterInt("MeleeDamage");
            RangeDamage = ItData.FindParameterInt("RangeDamage");
            FieldDamage = ItData.FindParameterInt("FieldDamage");
        }
        /// <summary>
        /// Called when the weapon is equipped.
        /// </summary>
        public virtual void Equip() {

        }
        /// <summary>
        /// Called when the weapon is unequipped.
        /// </summary>
        public virtual void Unequip() {

        }
    }
}