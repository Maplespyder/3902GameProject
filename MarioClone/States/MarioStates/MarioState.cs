using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class MarioState : State
    {
        public MarioState(IGameObject context) : base(context)
        {
        }

        // Behavior/actions

        public abstract void RunLeft();

        public abstract void Move();
    }
}
