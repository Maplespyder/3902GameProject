using MarioClone.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioClone.States
{
    public class MushroomMovingState : PowerupState
    {
        public MushroomMovingState(AbstractPowerup mushroom) : base(mushroom)
        {
            if(Mario.Instance.Position.X <= Context.Position.X)
            {
                Context.Velocity = new Vector2(-2, 0);
            }
            else
            {
                Context.Velocity = new Vector2(2, 0);
            }
        }
    }
}
