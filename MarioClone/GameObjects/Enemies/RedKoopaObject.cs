using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class RedKoopaObject : AbstractEnemy

    {
        public RedKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }

        /*public override bool CollisionResponse(AbstractGameObject gameObject, GameGrid.Side side, GameTime gameTime)
        {
            if (gameObject is Mario)
            {
                if (side.Equals(Side.Top))
                {
                    PowerupState.BecomeDead();
                }
            }

        }*/

        public override bool Update(GameTime gameTime, float percent)
        {
            //bool retVal = PowerupState.Update(gameTime, percent);
            return base.Update(gameTime, percent);
            //return retVal;
        }
    }
}
