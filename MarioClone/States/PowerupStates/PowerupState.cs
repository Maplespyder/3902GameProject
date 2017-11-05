using MarioClone.EventCenter;
using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public abstract class PowerupState
    {
        protected AbstractPowerup Context { get; set; }

        protected PowerupState(AbstractPowerup context)
        {
            Context = context;
            Context.DrawOrder = 1;
            Context.Velocity = new Vector2(0, 0);
        }
        
        public virtual bool Update(GameTime gameTime, float percent)
        {
            return false;
        }

        public virtual bool CollisionResponse(AbstractGameObject obj)
        {
            if(obj is Mario)
            {
                EventManager.Instance.TriggerPowerupCollectedEvent(Context, (Mario)obj);
                Context.PointValue = 0;
                return true;
            }
            return false;
        }
    }
}
