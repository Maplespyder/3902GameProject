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

		protected KoopaState(KoopaObject context)
        {
            Context = context;
        }

		// Behavior/actions

		static public void Move()
        {
            // koopa cannot move currently
        }

        public abstract void BecomeRun();
        public abstract void BecomeDead();
    }
}
