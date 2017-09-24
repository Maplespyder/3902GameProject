using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Win32.SafeHandles;

namespace MarioClone.States.BlockStates
{
    public abstract class BreakableBrickState
    {
        protected BreakableBrickObject Context { get; set; }

        public BreakableBrickState(BreakableBrickObject context)
        {
            Context = context;
        }

        public abstract void Bounce();

        public abstract void Break();

        public abstract void Bump();
    }
}
