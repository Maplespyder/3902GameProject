using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class MarioPowerupState
    {
        protected Mario Context { get; set; }

        public MarioPowerupState(Mario context)
        {
            Context = context;
        }

        // Behavior/actions

        public abstract void BecomeNormal();
        public abstract void BecomeSuper();
        public abstract void BecomeFire();
    }
}