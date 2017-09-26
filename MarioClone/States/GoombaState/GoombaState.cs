using MarioClone.GameObjects;

namespace MarioClone.States
{
    public abstract class GoombaState
    {
        protected GoombaObject Context { get; set; }

		protected GoombaState(GoombaObject context)
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
