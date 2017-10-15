using MarioClone.Collision;
using MarioClone.Factories;
using MarioClone.GameObjects.Enemies;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static MarioClone.Collision.GameGrid;

namespace MarioClone.GameObjects
{
    public class GreenKoopaObject : AbstractEnemy
    {
        public GreenKoopaObject(ISprite sprite, Vector2 position) : base(sprite, position)
		{
			BoundingBox.UpdateOffSets(-4, -4, -4, -4);
            BoundingBox.UpdateHitBox(Position, Sprite);
        }

        public override void CollisionResponse(AbstractGameObject gameObject, GameGrid.Side side)
        {
            if (gameObject is Mario)
            {
                if(side == Side.Top || side == Side.Bottom)
                {
                    PowerupState.BecomeDead();
                }
            }

        }
   
        public override bool Update(GameTime gameTime, float percent)
        {
            return base.Update(gameTime, percent);
        }
    }
}
