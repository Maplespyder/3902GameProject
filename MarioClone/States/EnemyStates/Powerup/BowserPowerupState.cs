using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;

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
                float whereOnHitBox = Math.Abs(gameObject.Position.X - Context.Position.X);
                if (side.Equals(Side.Top) && (whereOnHitBox < 250 && Context.Orientation is Facing.Left || whereOnHitBox > 600 && Context.Orientation is Facing.Right)) 
                {
                    Context.Hits--;
                    EventManager.Instance.TriggerEnemyDefeatedEvent(Context, (Mario)gameObject);
                    if (Context.Hits == 0)
                    {
                        Killer = (Mario)gameObject;
                        Context.PowerupStateBowser.BecomeDead();
                        Context.PowerupStateBowser.Killer = Killer;
                        return true;
                    }
                    int shift = (gameObject.Position.X > Context.Position.X+Context.Sprite.SourceRectangle.Width/2) ? -10 : 10;
                    Context.Velocity = new Vector2(Context.Velocity.Y + shift, Context.Velocity.X);
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