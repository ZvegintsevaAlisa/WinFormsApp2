using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace game
{

    public abstract class Proxy : Unit
    {
        public string logAtk = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log_atk.txt");
        public string logSa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log_sa.txt");
        public string logDeath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log_death.txt");
        public void ClearFiles()
        {
            File.WriteAllText(logAtk, string.Empty);
            File.WriteAllText(logSa, string.Empty);
            File.WriteAllText(logDeath, string.Empty);
        }
        public void LogAtk(string log)
        {
            File.AppendAllText(logAtk, log);
        }
        public void LogSa(string log)
        {
            File.AppendAllText(logSa, log);
        }
        public void LogDeath(string log)
        {
            File.AppendAllText(logDeath, log);
        }
    }

    class HealerProxy : Proxy, ISpecial, IHealable
    {
        Healer healer;
        public int hp { get => healer.hp; set => healer.hp=value; }
        public int maxHP { get => healer.maxHP; set => healer.maxHP=value; }
        public int atk { get => healer.atk; set=>healer.atk=value; }
        public int def { get => healer.def; set => healer.def=value; }
        public int strength { get => healer.strength; set=>healer.hp=value; }
        public int range { get=>healer.range; set=>healer.range=value; }
       // public PictureBox icon { get => healer.icon; set=> healer.icon=value; }
        public Type type { get => healer.type; set => healer.type=value; }
  
        public HealerProxy(Healer healer)
        {
            this.healer = healer;
            this.icon = healer.icon;
        }
        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            healer.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (healer.hp <= 0) LogDeath($"{this} died");
        }
        public void UseSpecialAbility(Army f_army, Army e_army)
        {
                healer.UseSpecialAbility(f_army, e_army);
                LogSa($"{this} healed ");
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
        public List<IHealable> isInRange(List<Unit> ur)
        {
            List<IHealable> healable = new List<IHealable>();
            foreach (Unit u in ur)
                if (u is IHealable unit && !u.IsDead)
                    healable.Add(unit);
            return healable;
        }
        public void GetHealed(int heal)
        {
            healer.GetHealed(heal);
        }
    }


    class LightInfantryProxy : Proxy, IHealable, IClonable, ISpecial
    //ISpecial,
    {
        LightInfantry lightinfantry;
        public int hp { get => lightinfantry.hp; set => lightinfantry.hp = value; }
        public int maxHP { get => lightinfantry.maxHP; set => lightinfantry.maxHP = value; }
        public int atk { get => lightinfantry.atk; set => lightinfantry.atk = value; }
        public int def { get => lightinfantry.def; set => lightinfantry.def = value; }
        public int strength { get => lightinfantry.strength; set => lightinfantry.hp = value; }
        public int range { get => lightinfantry.range; set => lightinfantry.range = value; }
        //public PictureBox icon { get => lightinfantry.icon; set => lightinfantry.icon = value; }
        public Type type { get => lightinfantry.type; set => lightinfantry.type = value; }
        public LightInfantryProxy(LightInfantry lightInfantry)
        {
           this.lightinfantry = lightInfantry;
            this.icon = lightInfantry.icon;
        }

        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            lightinfantry.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (lightinfantry.hp <= 0) LogDeath($"{this} died");
        }
        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            lightinfantry.UseSpecialAbility(f_army, f_army);
            LogSa($"{this} buffed");
        }
        public List<Unit> unitsinrange(Army army, Army army2) { return null; }
        public void GetHealed(int heal)
        {
            lightinfantry.GetHealed(heal);
        }

        public IClonable Clone()
        {
            return new LightInfantryProxy(lightinfantry.Clone() as LightInfantry);
        }
    }


    class HeavyInfantryProxy : Proxy, IBuffable
    {
        HeavyInfantry heavyinfantry;
        public int hp { get => heavyinfantry.hp; set =>     heavyinfantry.hp = value; }
        public int atk { get => heavyinfantry.atk; set => heavyinfantry.atk = value; }
        public int def { get => heavyinfantry.def; set => heavyinfantry.def = value; }
        //public PictureBox icon { get => heavyinfantry.icon; set => heavyinfantry.icon = value; }
        public Type type { get => heavyinfantry.type; set => heavyinfantry.type = value; }
        public HeavyInfantryProxy(HeavyInfantry heavyInfantry)
        {
            this.heavyinfantry = heavyInfantry;
            this.icon = heavyInfantry.icon;
        }
        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            heavyinfantry.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (heavyinfantry.hp <= 0) LogDeath($"{this} died");
        }
        public IBuffable GetBuff(int strength)
        {
            return this;
        }

    }
    class KnightProxy : Proxy
    {
        Knight knight;

        public int hp { get => knight.hp; set => knight.hp = value; }
        public int atk { get => knight.atk; set => knight.atk = value; }
        public int def { get => knight.def; set => knight.def = value; }
       // public PictureBox icon { get => knight.icon; set => knight.icon = value; }
        public Type type { get => knight.type; set => knight.type = value; }
        public KnightProxy(Knight knight)
        {
            this.knight = knight;
            this.icon = knight.icon;
        }

        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            knight.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (knight.hp <= 0) LogDeath($"{this} died");
        }

    }
    class ArcherProxy : Proxy, ISpecial, IHealable
    {
        Archer archer;
        public int hp { get => archer.hp; set => archer.hp = value; }
        public int maxHP { get => archer.maxHP; set => archer.maxHP = value; }
        public int atk { get => archer.atk; set => archer.atk = value; }
        public int def { get => archer.def; set => archer.def = value; }
        public int strength { get => archer.strength; set => archer.hp = value; }
        public int range { get => archer.range; set => archer.range = value; }
       // public PictureBox icon { get => archer.icon; set => archer.icon = value; }
        public Type type { get => archer.type; set => archer.type = value; }
        public ArcherProxy(Archer archer)
        {
            this.archer = archer;
            this.icon = archer.icon;
        }
        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            archer.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (archer.hp <= 0) LogDeath($"{this} died");
        }
        public void UseSpecialAbility(Army f_army, Army e_army)
        {

            archer.UseSpecialAbility(f_army, e_army);
            LogSa($"{this} shot ");
        }
        public List<Unit> unitsinrange(Army army, Army army2) { return null; }
        public void GetHealed(int heal)
        {
            archer.GetHealed(heal);
        }
    }
    class WarlockProxy : Proxy, ISpecial
    {
        Warlock warlock;
        public int hp { get => warlock.hp; set => warlock.hp = value; }
        public int atk { get => warlock.atk; set => warlock.atk = value; }
        public int def { get => warlock.def; set => warlock.def = value; }
        public int strength { get => warlock.strength; set => warlock.hp = value; }
        public int range { get => warlock.range; set => warlock.range = value; }
      //  public PictureBox icon { get => warlock.icon; set => warlock.icon = value; }
        public Type type { get => warlock.type; set => warlock.type = value; }

        public WarlockProxy(Warlock warlock)
        {
            this.warlock = warlock;
            this.icon = warlock.icon;
        }
        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            warlock.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (warlock.hp <= 0) LogDeath($"{this} died");
        }
        public void UseSpecialAbility(Army f_army, Army e_army)
        {
            warlock.UseSpecialAbility(f_army, e_army);
            LogSa($"{this} cloned ");
        }
        public List<Unit> unitsinrange(Army army, Army army2) { return null; }

    }
    public class GGAdapterProxy : Proxy
    {
        GGAdapter ggadapter;
        public int hp { get => ggadapter.hp; set => ggadapter.hp = value; }
        public int atk { get => ggadapter.atk; set => ggadapter.atk = value; }
        public int def { get => ggadapter.def; set => ggadapter.def = value; }
        //public PictureBox icon { get => ggadapter.icon; set => ggadapter.icon = value; }
        public Type type { get => ggadapter.type; set => ggadapter.type = value; }
        public GGAdapterProxy(GGAdapter ggadapter)
        {
           this.ggadapter = ggadapter;
            this.icon = ggadapter.icon;
        }

        public void TakeDamage(Unit opnt, Army thisarmy)
        {
            ggadapter.TakeDamage(opnt, thisarmy);
            LogAtk($"{this} took {(opnt.atk * thisarmy.armyprice / (thisarmy.armyprice + this.def))} damage from {opnt}");
            if (ggadapter.hp <= 0) LogDeath($"{this} died");
        }

    }


}
