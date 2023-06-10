using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public enum Type
    {
        LightInfantry = 0,
        HeavyInfantry,
        Knight,
        Healer,
        Archer,
        Warlock,
        GulyayGorod
    }
    public abstract class Unit
    {
        public Type type { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int price { get; set; }

        public PictureBox icon {get; set;}
        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            this.hp -= (opnt.atk*thisarmy.armyprice / (thisarmy.armyprice + this.def));
        }

        public bool IsDead
        {
            get { return this.hp <= 0; }
        }
    }

    interface ISpecial
    {
         int strength { get; set; }
         int range { get; set; }
        void UseSpecialAbility(Army army, Army army2);
        List<Unit> unitsinrange(Army f_army, Army e_army);
    }

    interface IHealable
    {
         int maxHP { get; set; }
        void GetHealed(int heal);
    }

    public interface IBuffable
    {   
 
        public IBuffable GetBuff(int strength);
    }

    interface IClonable
    {
        IClonable Clone();
    }
}
