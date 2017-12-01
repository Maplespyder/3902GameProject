using MarioClone.Collision;
using MarioClone.GameObjects;
using MarioClone.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioClone.States.EnemyStates.Powerup
{
    public enum BowserAction
    {
        Idle,
        Walk,
        BreatheFire
    }
    public abstract class BowserActionState
    {
 
        protected BowserObject Context { get; set; }

        protected BowserAction PreviousActionState { get; set; }

        protected byte[] random { get; set; }
        protected int randomResult { get; set; }

        protected BowserActionState(BowserObject context)
        {
            random = new Byte[1];
            Context = context;
        }

        public BowserAction Action { get; set; }


        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractBlock)
            {
                if (side == Side.Left)
                {
                    Context.Velocity = new Vector2(BowserObject.BowserMovementSpeed, Context.Velocity.Y);
                    Context.Orientation = Facing.Right;
                }
                else if (side == Side.Right)
                {
                    Context.Velocity = new Vector2(-(BowserObject.BowserMovementSpeed), Context.Velocity.Y);
                    Context.Orientation = Facing.Left;
                }
				else if(side == Side.Bottom)
				{
					Context.Gravity = false;
					Context.Velocity = new Vector2(Context.Velocity.X, 0);
					return true;
				}
            }

            return false;

        }

        public abstract void BecomeWalk(Facing orientation);
        public abstract void BreatheFire();
        public abstract void BecomeIdle();

        public abstract bool Update(GameTime gameTime, float percent);
    }
}
