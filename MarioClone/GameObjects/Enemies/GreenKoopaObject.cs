using MarioClone.Sprites;
using MarioClone.States.EnemyStates;
using Microsoft.Xna.Framework;

namespace MarioClone.GameObjects
{
    public class GreenKoopaObject : AbstractEnemy
    {
        public GreenKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
		{
			BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
            PowerupState = new KoopaAlive(this);
        }

        /*public override bool CollisionResponse(AbstractGameObject gameObject, GameGrid.Side side, GameTime gameTime)
        {
            if (gameObject is Mario)
            {
                if(side.Equals(Side.Top))
                {
                    PowerupState.BecomeDead();
                    TimeDead = 0;

                }
            }

        }*/

        public override bool Update(GameTime gameTime, float percent)
        {
            bool retVal = PowerupState.Update(gameTime, percent);
            return base.Update(gameTime, percent) || retVal;
        }
    }
}
