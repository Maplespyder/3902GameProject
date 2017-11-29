using MarioClone.Collision;
using MarioClone.GameObjects;
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
        BreathFire
    }
    public abstract class BowserActionState
    {
 
        protected BowserObject Context { get; set; }

        protected MarioAction LastState { get; set; }

        protected BowserActionState(BowserObject context)
        {
            Context = context;
        }

        public BowserAction Action { get; set; }


        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractBlock)
            {
                if (side == Side.Left)
                {
                    Context.Velocity = new Vector2(BowserObject.EnemyHorizontalMovementSpeed, Context.Velocity.Y);
                    Context.Orientation = Facing.Right;
                }
                else if (side == Side.Right)
                {
                    Context.Velocity = new Vector2(-(BowserObject.EnemyHorizontalMovementSpeed), Context.Velocity.Y);
                    Context.Orientation = Facing.Left;
                }
            }

            return false;

        }

        public abstract void Walk(Facing orientation);
        public abstract void BreathFire();
        public abstract void Idle();

        public abstract bool Update(GameTime gameTime, float percent);
    }
}
