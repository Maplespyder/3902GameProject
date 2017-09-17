using MarioClone.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States
{
    public abstract class State
    {
        protected IGameObject Context { get; set; }

        public State(IGameObject context)
        {
            Context = context;
        }

        protected abstract void CheckNextState();
    }
}
