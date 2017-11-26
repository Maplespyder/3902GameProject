using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using MarioClone.Collision;

namespace MarioClone.States
{
    public enum MarioPowerup
    {
        Dead,
        Normal,
        Super,
        Fire,
        Invincible
    }

    public abstract class MarioPowerupState
    {
        public MarioPowerup Powerup { get; set; }

        protected Mario Context { get; set; }

        protected MarioPowerupState(Mario context)
        {
            Context = context;
        }

        // Behavior/actions

        public virtual void Enter() { }
        public virtual void Leave() { }

        public virtual bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is AbstractEnemy && !((AbstractEnemy)gameObject).IsDead)
            {
                if (side == Side.Bottom)
                {
                    if (gameObject is PiranhaObject)
                    {
                        TakeDamage();
                    }
                    else
                    {
                        Context.Velocity = new Vector2(Context.Velocity.X, -7);
                    }
                }
                else
                {
                    TakeDamage();
                }
                return true;
            }
            else if (gameObject is RedMushroomObject)
            {
                BecomeSuper();
                return true;
            }
            else if (gameObject is FireFlowerObject)
            {
                BecomeFire();
                return true;
            }
            else if(gameObject is FireBall)
            {
                var fireball = (FireBall)gameObject;
                if (fireball.Owner is AbstractEnemy)
                {
                    TakeDamage();
                }
            }

            return false;
        }

        public abstract void BecomeDead();
		public abstract void Update(GameTime gameTime);
		public abstract void BecomeNormal();
        public abstract void BecomeSuper();
        public abstract void BecomeFire();
        public abstract void TakeDamage();
        public abstract void BecomeInvincible();
    }
}
