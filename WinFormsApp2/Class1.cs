using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Battle
    {
        public Army leftarmy;
       public Army rightarmy;
        bool leftwon;
        public List<State> states;
        public int move;

       public string g, k;
        StringBuilder sb = new StringBuilder();
        public Battle (Army leftarmy, Army rightarmy)
        {
            this.leftarmy = leftarmy;
            this.rightarmy = rightarmy;
            states = new List<State>();

        }
        public bool IsDone()
        {
            if(leftarmy.unitlist.Count == 0 || rightarmy.unitlist.Count == 0)
                return true;
            return false;
        }
        public void Move()
        {
            State state = new State(leftarmy, rightarmy, move);
           // sb.Append(state.larmy.unitlist[0].ToString());
            states.Add(state);  
           // sb.Append("keft army: " + leftarmy.GetStats() + "\nright army" + rightarmy.GetStats() + "\n");
            move++;
          //  states.Add(new State(leftarmy, rightarmy, move));
                leftarmy.RemoveDead();
                rightarmy.RemoveDead();
            if (!this.IsDone())
            {
                leftarmy.Reposition();
                rightarmy.Reposition();
                leftarmy.unitlist[0].TakeDamage(rightarmy.unitlist[0], leftarmy);
                rightarmy.unitlist[0].TakeDamage(leftarmy.unitlist[0], rightarmy);
                leftarmy.unitlist[0].hp -= 2;
                for (int i = 1; i < Math.Max(leftarmy.unitlist.Count, rightarmy.unitlist.Count); i++)
                {
                    if (i < leftarmy.unitlist.Count && leftarmy.unitlist[i] is ISpecial unit)
                        unit.UseSpecialAbility(leftarmy, rightarmy);
                }
                // rightarmy.unitlist[0].hp -= 2;
                g = $" current hp = {leftarmy.unitlist[0].hp}, atk = {rightarmy.unitlist[0].atk} ";
                //g = $" current hp = {rightarmy.unitlist[0].hp}";
              //  sb.Append(leftarmy.GetStats() + "\n" + rightarmy.ToString() + "\n");
                File.AppendAllText("log1.txt", sb.ToString());
                sb.Clear();
            }
            else
            {
                if (rightarmy.unitlist.Count == 0)
                    File.AppendAllText("log.txt", "left won");
                menu.form1.Close();
            }


        }  // leftarmy.RemoveDead();
        public void Operation()
        {
            sb.Append($"{leftarmy.unitlist.Count} + {states[move - 1].UnitList.Count}");
            leftarmy.unitlist.Clear();
            for (int i = 0; i < states[move - 1].UnitList.Count; i++) {
                leftarmy.unitlist.Add(states[move - 1].UnitList[i]); }
          //  leftarmy.unitlist = states[move - 1].larmy.unitlist;
          //  rightarmy = states[move - 1].rarmy;
            //foreach (State state in states)
            //{
            //    state.larmy.GetStats();
            //    sb.Append(state.larmy.GetStats() + "\nhhhhhhh");
            //}
            sb.Append($"");
            move--;
            leftarmy.Reposition();
            rightarmy.Reposition();
            File.AppendAllText("log.txt", sb.ToString());
        }
    }

}
