using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public class MarioIdle : MarioState
    {
        public MarioIdle(Mario context) : base(context)
        {
        }

        protected override void CheckNextState()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        public override void RunLeft()
        {
            throw new NotImplementedException();
        }
    }
}
