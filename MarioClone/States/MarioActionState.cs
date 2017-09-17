using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    abstract class MarioActionState : State<MarioObject>
    {
        public MarioActionState(MarioObject context) : base(context)
        {
        }

        // mario based methods go here
        public abstract void Jump();
    }

    internal class MarioObject
    {
    }
}
