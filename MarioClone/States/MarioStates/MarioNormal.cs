using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public class MarioNormal : MarioPowerupState
    {
        public MarioNormal(Mario context) : base(context)
        {
        }

        public override void BecomeNormal()
        {
            CheckNextState();
        }

        public override void BecomeSuper()
        {
            Context.PowerupState = new MarioSuper(Context);
            CheckNextState();
        }

        public override void BecomeFire()
        {
            Context.PowerupState = new MarioFire(Context);
            CheckNextState();
        }

        protected override void CheckNextState()
        {
            // check to see if mario is hit maybe? not sure what goes in here
        }
    }
}
