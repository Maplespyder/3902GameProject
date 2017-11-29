using MarioClone.Collision;
using MarioClone.EventCenter;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (gameObject is Mario && !(((Mario)gameObject).PowerupState is MarioInvincibility2))
            {
                if (side.Equals(Side.Top))
                {
                    Context.Hits -= 1;
                    if (Context.Hits == 0)
                    {
                        Killer = (Mario)gameObject;
                        EventManager.Instance.TriggerEnemyDefeatedEvent(Context, (Mario)gameObject);
                        Context.PowerupStateBowser.BecomeDead();
                        return true;
                    }
                }
            }
            else if (gameObject is FireBall)
            {
                var fireball = (FireBall)gameObject;
                if (fireball.Owner is Mario)
                {
                    Context.Hits -= 1;
                    if (Context.Hits == 0)
                    {
                        Killer = (Mario)gameObject;
                        EventManager.Instance.TriggerEnemyDefeatedEvent(Context, (Mario)fireball.Owner);
                        Context.PowerupStateBowser.BecomeDead();
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract void BecomeDead();
        public abstract void BecomeInvincible();
        public abstract bool Update(GameTime gameTime, float percent);
    }
}
