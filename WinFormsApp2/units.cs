using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class LightInfantry : Unit,  IHealable, IClonable, ISpecial
    {       
        public int maxHP { get; set; }
      
        public  int strength { get; set; } 
        public int range { get; set; } 
        public LightInfantry()
        {
            this.hp = 10;
            this.maxHP = 10;
            this.atk = 5;
            this.def = 4;;
            this.icon = new PictureBox();
            this.strength = 3;
            this.range = 1;
            this.price = atk+def+hp+(strength+range)*2;
            this.type = Type.LightInfantry;
        }


        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            List<IBuffable> isInRange = this.isInRange(unitsinrange(f_army, e_army));
            Random rnd = new Random();
            Buff buff;
            int chance = rnd.Next(0, 2);
            if (isInRange.Any())
            {
                int n = rnd.Next(0, isInRange.Count);
                int b = rnd.Next(0, 2);
                if (b==0)
                 buff = new Helmet(isInRange[n]);
                else if (b==1)
                     buff = new Horse(isInRange[n]);
                else  buff = new Armor(isInRange[n]);
                buff.GetBuff(strength);
                f_army.RemoveUnit(isInRange[n] as Unit);
                f_army.AddUnit(buff);
            }
        }
        public List<Unit> unitsinrange(Army f_army, Army e_army)
        {
            List<Unit> inrange = new List<Unit>();
            int ind = f_army.unitlist.IndexOf(this);
            if (ind + range < f_army.unitlist.Count)
                inrange.AddRange(f_army.unitlist.GetRange(ind, range));
            else inrange.AddRange(f_army.unitlist.GetRange(ind, f_army.unitlist.Count - ind));
            if (ind - range >= 0)
                inrange.AddRange(f_army.unitlist.GetRange(ind - range, range));
            else inrange.AddRange(f_army.unitlist.GetRange(0, ind - 1));
            return inrange;
        }
        public List<IBuffable> isInRange(List<Unit> ur)
        {
            List<IBuffable> buffable = new List<IBuffable>();
            foreach (Unit u in ur)
                if (u is IBuffable unit && !u.IsDead)
                    buffable.Add(unit);
            return buffable;
        }

        public void GetHealed(int heal)
        {
            if (hp + heal < maxHP) this.hp += heal;
            else hp = maxHP;
        }

        public IClonable Clone()
        {
            return (IClonable)this.MemberwiseClone();
        }
    }

    class HeavyInfantry : Unit, IBuffable
    {   public bool isbuffed { get; set; }
      
        public HeavyInfantry()
        {
            this.hp = 20;
            this.atk = 6;
            this.def = 8;
            this.price = hp+atk+def;    
            this.icon = new PictureBox();
            this.type= Type.HeavyInfantry;
        }

        public IBuffable GetBuff(int strength)
        {
            return this;
        }



    }


    class Knight : Unit
    {
        
        public Knight()
        {
            this.hp = 30;
            this.atk = 9;
            this.def = 9;
            this.icon = new PictureBox();
        }

        
      
    }

    class Healer : Unit, ISpecial, IHealable
    {
        public int maxHP { get; set; }
        public int strength { get; set; }
        public int range { get; set; }
        public Healer()
        {
            this.hp = 10;
            this.maxHP = 7;
            this.atk = 2;
            this.def = 3;
            this.strength = 3;
            this.range = 2;
            this.price = atk + hp + def + (strength + range) * 2;
            this.icon = new PictureBox();
            this.type = Type.Healer;
        }

        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            List<IHealable> isInRange = this.isInRange(unitsinrange(f_army, e_army));
            Random rnd = new Random();
            int n = rnd.Next(0, isInRange.Count);
            isInRange[n].GetHealed(strength);
        }

        public List<Unit> unitsinrange(Army f_army, Army e_army)
        {   List<Unit> inrange = new List<Unit>();
            int ind = f_army.unitlist.IndexOf(this);
            if (ind + range < f_army.unitlist.Count)
                inrange.AddRange(f_army.unitlist.GetRange(ind, range));
            else inrange.AddRange(f_army.unitlist.GetRange(ind, f_army.unitlist.Count - ind));
            if (ind - range >= 0)
                inrange.AddRange(f_army.unitlist.GetRange(ind - range, range));
            else inrange.AddRange(f_army.unitlist.GetRange(0, ind - 1));
            return inrange;
        }
        public List<IHealable> isInRange(List<Unit> ur)
        { List<IHealable> healable = new List<IHealable>();
            foreach (Unit u in ur)
                if (u is IHealable unit && !u.IsDead)
                    healable.Add(unit);
            return healable;
        }
        public void GetHealed(int heal)
        {
            if (hp + heal < maxHP) this.hp += heal;
            else hp = maxHP;
        }
    }

    class Archer : Unit, ISpecial, IHealable
    {
      
        public int maxHP { get; set; }
        public int strength { get; set; }
        public int range { get; set; }
        
        public Archer()
        {
            this.hp = 8;
            this.maxHP = 8;
            this.atk = 5;
            this.def = 3;
            this.strength = 3;
            this.range = 5;
            this.price = atk + def + hp + (strength + range) * 2;
            this.icon = new PictureBox();
            this.type = Type.Archer;
        }

        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            
            List<Unit> inrange = this.unitsinrange(f_army, e_army);
            Random rnd = new Random();
            int n = rnd.Next(0, inrange.Count);
            e_army.unitlist[n].TakeDamage(this, e_army);
        }
        public List<Unit> unitsinrange(Army f_army, Army e_army)
        {
            List<Unit> inrange = new List<Unit>();
            int ind = f_army.unitlist.IndexOf(this);
            if (range > ind)
            {
                if (range - ind >= e_army.unitlist.Count)
                    inrange.AddRange(e_army.unitlist.GetRange(0, e_army.unitlist.Count));
                else
                    inrange.AddRange(e_army.unitlist.GetRange(0, range - ind));
            }
                return inrange;
            
        }

        public void GetHealed(int heal)
        {
            if (hp + heal < maxHP) this.hp += heal;
            else hp = maxHP;
        }
    }

    class Warlock : Unit, ISpecial
    {
        public int strength { get; set; }
        public int range { get; set; }
        public Warlock()
        {
            this.hp = 7;
            this.atk = 3;
            this.def = 3;
            this.strength = 7;
            this.range = 2;
            this.price = atk + hp + def + (strength + range) * 2;
            this.icon = new PictureBox();
            this.type = Type.Warlock;
        }

        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            List<IClonable> isInRange = this.isInRange(unitsinrange(f_army, e_army));
            Random rnd = new Random();
            
            int chance = rnd.Next(0, 100);
            if (chance <= 100 && isInRange.Any())
            {
                int n = rnd.Next(0, isInRange.Count);
                f_army.AddUnit((Unit)isInRange[n].Clone());
            }
        }
        public List<Unit> unitsinrange(Army f_army, Army e_army)
        {
            List<Unit> inrange = new List<Unit>();
            int ind = f_army.unitlist.IndexOf(this);
            if (ind + range < f_army.unitlist.Count)
                inrange.AddRange(f_army.unitlist.GetRange(ind, range));
            else inrange.AddRange(f_army.unitlist.GetRange(ind, f_army.unitlist.Count - ind));
            if (ind - range >= 0)
                inrange.AddRange(f_army.unitlist.GetRange(ind - range, range));
            else inrange.AddRange(f_army.unitlist.GetRange(0, ind - 1));
            return inrange;
        }
        public List<IClonable> isInRange(List<Unit> ur)
        {
            List<IClonable> clonable = new List<IClonable>();
            foreach (Unit u in ur)
                if (u is IClonable unit && !u.IsDead)
                    clonable.Add(unit);
            return clonable;
        }

    }




}
