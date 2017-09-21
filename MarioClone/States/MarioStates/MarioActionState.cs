using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class MarioActionState : State
    {
        public MarioActionState(IGameObject context) : base(context)
        {
        }

        // Behavior/actions

        public abstract void BecomeDead();
        public abstract void BecomeIdle();
    }
}
