using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public abstract class Buff : Unit, IBuffable
    {
        public IBuffable buffable;

        public Buff(IBuffable buffable) { this.buffable = buffable; }
        public virtual IBuffable GetBuff(int strength)
        {
            return buffable.GetBuff(strength);
        }
    
    }

    public class Helmet :  Buff
    {
        public Helmet(IBuffable buffable): base(buffable) { }
       
        public override IBuffable GetBuff(int strentgh)
        {
         buffable.GetBuff(strentgh);
            BuffDef(buffable, strentgh);
            return buffable;
        }
        public void BuffDef(IBuffable buffable, int strength)
        {
            (buffable as Unit).def += strength;
        }
    }
    public class Horse : Buff
    {
        public Horse(IBuffable buffable) : base(buffable) { }

        public override IBuffable GetBuff(int strentgh)
        {
            buffable.GetBuff(strentgh);
            BuffDef(buffable, strentgh);
            return buffable;
        }
        public void BuffDef(IBuffable buffable, int strength)
        {
            (buffable as Unit).atk += strength;
        }
    }
    public class Armor : Buff
    {
        public Armor(IBuffable buffable) : base(buffable) { }

        public override IBuffable GetBuff(int strentgh)
        {
            buffable.GetBuff(strentgh);
            BuffDef(buffable, strentgh);
            return buffable;
        }
        public void BuffDef(IBuffable buffable, int strength)
        {
            (buffable as Unit).def += strength;
            (buffable as Unit).atk += strength;
        }
    }
}
