using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{

    class UnitCreator
    {
        public static Unit CreateUnit(int id)
        {
            switch (id)
            {
                case (int)Type.LightInfantry: return new LightInfantryProxy(new LightInfantry());
                case (int)Type.HeavyInfantry: return new HeavyInfantryProxy(new HeavyInfantry());
                case (int)Type.Knight: return new KnightProxy(new Knight());
                case (int)Type.Archer: return new ArcherProxy(new Archer());
                case (int)Type.Warlock: return new WarlockProxy(new Warlock());
                case (int)Type.Healer: return new HealerProxy(new Healer());
                case (int)Type.GulyayGorod: return new GGAdapterProxy(new GGAdapter(new GulyayGorod(20, 35, 55)));
            }
            return null;

        }

    }
    public interface Army
    {
        public List<Unit> unitlist { get; set; }
        public int armyprice { get; set; }
        public void AddUnit(Unit unit);
        public void RemoveDead();
        public void RemoveUnit(Unit unit);
        public string GetStats();
        public void Reposition();

    }
    public abstract class ArmyCreator
    {
        public abstract Army CreateArmy(List<int> idlist, int armyprice);
        public abstract void PlaceGrid();

    }

    public class LeftArmyCreator : ArmyCreator
    {
        public override Army CreateArmy(List<int> idlist, int armyprice)
        {
            Army army = new LeftArmy();
            army.armyprice = armyprice;
            foreach (int id in idlist)
            {
                army.AddUnit(UnitCreator.CreateUnit(id));
            }
            return army;
        }
        public override void PlaceGrid()
        {
            for (int i = 0; i < 13; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = 21;
                pictureBox.Height = 21;
                pictureBox.Location = new Point(361 - i * 27, 229);
                pictureBox.BackColor = Color.PaleVioletRed;
                menu.form1.Controls.Add(pictureBox);
                pictureBox.SendToBack();
            }

        }
    }
    public class RightArmyCreator : ArmyCreator
    {
        public override Army CreateArmy(List<int> idlist, int armyprice)
        {
            Army army = new RightArmy();
            army.armyprice = armyprice;
            foreach (int id in idlist)
            {
                army.AddUnit(UnitCreator.CreateUnit(id));
            }
            return army;
        }
        public override void PlaceGrid()
        {
            for (int i = 0; i < 13; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = 21;
                pictureBox.Height = 21;
                pictureBox.Location = new Point(398 + i * 27, 229);
                pictureBox.BackColor = Color.PaleVioletRed;
                menu.form1.Controls.Add(pictureBox);
                pictureBox.SendToBack();
            }
        }


    }
        public class LeftArmy : Army
        {
            public List<Unit> unitlist { get; set; }
            public int armyprice { get; set; }
            public LeftArmy()
            {
                unitlist = new List<Unit>();
            }
            public void AddUnit(Unit unit)
            {
                unitlist.Add(unit);
                unit.icon.Image = Properties.Resources.c1;
                unit.icon.Width = 21;
                unit.icon.Height = 21;
                unit.icon.Location = new Point(361 - unitlist.IndexOf(unit) * 27, 229);
                menu.form1.Controls.Add(unit.icon);
                unit.icon.BringToFront();
            }
            public void RemoveUnit(Unit unit)
            {
                unitlist.RemoveAt(unitlist.IndexOf(unit));
            }
            public void Reposition()

            {
                if (unitlist.Count > 0)
                {
                    int i = 0;
                    foreach (Unit unit in unitlist)
                    {
                        unit.icon.Location = new Point(361 - i * 27, 229);
                        i++;
                    }
                }
            }
            public void RemoveDead()
            {
                unitlist.RemoveAll(x => x.IsDead);
            }
            public string GetStats()
            {
                string s = " ";
                foreach (Unit unit in unitlist)
                    s += $"\n{unit} + {unit.hp} + {unit.atk} + {unit.def}";
                return s;
            }

        }
        public class RightArmy : Army
        {
            public List<Unit> unitlist { get; set; }
            public int armyprice { get; set; }
            public RightArmy()
            {
                unitlist = new List<Unit>();
            }
            public void AddUnit(Unit unit)
            {
                unitlist.Add(unit);
                unit.icon.Image = Properties.Resources.c2;
                unit.icon.Width = 21;
                unit.icon.Height = 21;
                unit.icon.Location = new Point(398 + unitlist.IndexOf(unit) * 27, 229);
                unit.icon.BringToFront();
                menu.form1.Controls.Add(unit.icon);

            }
            public void RemoveUnit(Unit unit)
            {
                unitlist.RemoveAt(unitlist.IndexOf(unit));
            }
            public void Reposition()
            {
                if (unitlist.Count > 0)
                {
                    int i = 0;
                    foreach (Unit unit in unitlist)
                    {

                        unit.icon.Location = new Point(398 + i * 27, 229);
                        unit.icon.BringToFront();
                        i++;
                    }
                }
            }

            public string GetStats()
            {
                string s = " ";
                foreach (Unit unit in unitlist)
                    s += $"{unit} + {unit.hp} + {unit.atk} + {unit.def}";
                return s;
            }
            public void RemoveDead()
            {
                unitlist.RemoveAll(x => x.IsDead);
            }

        }


    }

   

        
    

