using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public class MarioIdle : MarioActionState
    {
        public MarioNormal(Mario context) : base(context)
        {
        }

        public override void Move()
        {
            // change values of mario like velocity
            // then do something like Context.MarioActionState = new MarioRunning(this);
        }

        protected override void CheckNextState()
        {
            
        }
    }
}
