using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using MarioClone.Collision;
using MarioClone.GameObjects.Other;

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
                        TakeDamage(gameObject);
                    }
					else if(gameObject is BowserObject)
					{
						if(Context.Velocity.X > 0)
						{
							Context.Velocity = new Vector2(Context.Velocity.X + 2, -7);
						}
						else if (Context.Velocity.X < 0)
						{
							Context.Velocity = new Vector2(Context.Velocity.X - 2, -7);
						}
						else
						{
							Context.Velocity = new Vector2(-5, -7);
						}
					}
					else
                    {
                        Context.Velocity = new Vector2(Context.Velocity.X, -7);
                    }
                }
                else
                {
                    TakeDamage(gameObject);
                }
                return true;
            }
            else if(gameObject is AbstractBlock)
            {
                if(side == Side.Bottom && gameObject.Velocity.Y < 0)
                {
                    if(!ReferenceEquals(((AbstractBlock)gameObject).Bumper, Context))
                    {
                        TakeDamage(((AbstractBlock)gameObject).Bumper);
                    }
                }
            }
            else if(gameObject is Mario)
            {
                Mario temp = (Mario)gameObject;
                if (side == Side.Top)
                {
                    if(temp.ActionState is MarioFall2)
                    {
                        TakeDamage(gameObject);
                    }
                }
                else if(side == Side.Bottom && !((temp.PowerupState is MarioInvincibility2)
                    || temp.PowerupState is MarioDead2))
                {
                    if(!(Context.ActionState is MarioJump2))
                    {
                        Context.Velocity = new Vector2(Context.Velocity.X, -7);
                    }
                }
            }
            else if((gameObject is FireBall && !ReferenceEquals(((FireBall)gameObject).Owner, Context)))
            {
                TakeDamage(((FireBall)gameObject).Owner);
            }
			else if(gameObject is BigFireBall)
			{
				TakeDamage(((BigFireBall)gameObject).Owner);
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

            return false;
        }

        public abstract void BecomeDead();
		public abstract void Update(GameTime gameTime);
		public abstract void BecomeNormal();
        public abstract void BecomeSuper();
        public abstract void BecomeFire();
        public abstract void TakeDamage(AbstractGameObject damager);
        public abstract void BecomeInvincible();
    }
}
