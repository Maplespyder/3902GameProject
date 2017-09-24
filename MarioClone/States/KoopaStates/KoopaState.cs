using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class KoopaState
    {
        protected KoopaObject Context { get; set; }

        public KoopaState(KoopaObject context)
        {
            Context = context;
        }

        // Behavior/actions

        public void Move()
        {
            // koopa cannot move currently
        }

        public abstract void BecomeRunLeft();
        public abstract void BecomeRunRight();
        public abstract void BecomeDead();
    }
}
