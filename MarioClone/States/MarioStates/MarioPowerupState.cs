using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class MarioPowerupState : State
    {
        public MarioPowerupState(IGameObject context) : base(context)
        {
        }

        // Behavior/actions

        public abstract void BecomeNormal();
        public abstract void BecomeSuper();
    }
}
