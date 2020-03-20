using System.Linq;

namespace PipelineDreams.Item.Weapon {
    public class Weapon : Item {
        
        public int MeleeDamage { get; protected set; }

        public int RangeDamage { get; protected set; }

        public int FieldDamage { get; protected set; }

        public override void Obtain(Entity.Entity holder, TaskManager cM)
        {

            base.Obtain(holder, cM);
            MeleeDamage = Data.FindParameterInt("MeleeDamage");
            RangeDamage = Data.FindParameterInt("RangeDamage");
            FieldDamage = Data.FindParameterInt("FieldDamage");
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