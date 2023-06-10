using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class State {
        public List<Unit> UnitList;
       public List <Unit> UnitList2;
        public int move;

        public State(Army larmy, Army rarmy, int move)
        {    UnitList = new List<Unit>(); UnitList2 = new List<Unit>();
            foreach (Unit l in larmy.unitlist) { UnitList.Add(l); }
            foreach (Unit r in rarmy.unitlist) { UnitList.Add (r); }
            this.move = move;
        }
    }

   public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo(Battle battle);

    }

    public class ChangeState : Command

    {
        public Battle battle;
        public ChangeState(Battle battle)
        {
            this.battle = battle;
        }

        public override void Execute()
        {
            battle.Operation();
        }
       public override void Undo(Battle battle)
        {
            //battle.leftarmy = battle.states[battle.move + 1].larmy;
          //  battle.rightarmy = battle.states[battle.move + 1].rarmy;
            battle.move++;
            battle.leftarmy.Reposition();
            battle.rightarmy.Reposition();
        }
    
    }
}
