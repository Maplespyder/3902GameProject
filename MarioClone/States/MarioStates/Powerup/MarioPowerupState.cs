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
        public enum MarioPowerup
        {
            Dead,
            Normal,
            Super,
            Fire
        }

        public MarioPowerup Powerup { get; set; }

        protected Mario Context { get; set; }

        protected MarioPowerupState(Mario context)
        {
            Context = context;
        }

        // Behavior/actions

        public abstract void BecomeDead();
        public abstract void BecomeNormal();
        public abstract void BecomeSuper();
        public abstract void BecomeFire();
        public abstract void TakeDamage();
    }
}
