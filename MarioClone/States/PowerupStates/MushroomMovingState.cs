using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MushroomMovingState : PowerupState
    {
        public MushroomMovingState(AbstractPowerup mushroom) : base(mushroom)
        {
            Context.DrawOrder = 0.51f;
            if(Context.Releaser != null)
            {
                if (Context.Releaser.Position.X <= Context.Position.X)
                {
                    Context.Velocity = new Vector2(-2, 0);
                }
                else
                {
                    Context.Velocity = new Vector2(2, 0);
                }
            }
            else
            {
                Context.Velocity = new Vector2(-2, 0);
            }
            
        }
    }
}
