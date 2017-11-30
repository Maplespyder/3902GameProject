using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States.EnemyStates.Powerup
{
    public enum BowserPowerup
    {
        Dead,
        Alive,
        Invincible
    }

    public abstract class BowserPowerupState
    {
        public BowserPowerup Powerup { get; set; }

        protected BowserObject Context { get; set; }

        public Mario Killer { get; set; }


        protected BowserPowerupState(BowserObject context)
        {
            Context = context;
        }
        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario && !(((Mario)gameObject).PowerupState is MarioInvincibility2) 
				&& !(Context.PowerupStateBowser is BowserInvincibility))
            {
                if (side.Equals(Side.Top))
                {
                    Context.Hits--;
                    if (Context.Hits == 0)
                    {
                        Killer = (Mario)gameObject;
                        EventManager.Instance.TriggerEnemyDefeatedEvent(Context, (Mario)gameObject);
                        Context.PowerupStateBowser.BecomeDead();
                        Context.PowerupStateBowser.Killer = Killer;
                        return true;
                    }
					Context.PowerupStateBowser = new BowserInvincibility(Context);
                }
            
        }
            return false;
        }

        public abstract void BecomeDead();
		public abstract void BecomeAlive();
		public abstract void BecomeInvincible();
        public abstract bool Update(GameTime gameTime, float percent);
    }
}