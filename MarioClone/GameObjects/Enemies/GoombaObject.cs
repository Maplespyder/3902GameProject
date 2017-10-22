using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;
using MarioClone.Collision;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class GoombaObject : AbstractEnemy 
    {
        public GoombaObject(ISprite sprite, Vector2 position) : base(sprite, position)
        {
            PowerupState = new GoombaAlive(this);
            BoundingBox.UpdateOffSets(-8, -8, -8, -8);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario)
            {
                if (side.Equals(Side.Top))
                {
                    PowerupState.BecomeDead();
                    TimeDead = 0;
                    return true;
                }
            }
            return false;
        }

        public override bool Update(GameTime gameTime, float percent)
        {
            bool retVal = PowerupState.Update(gameTime, percent);
            base.Update(gameTime, percent);
            return retVal;
        }
    }
}
