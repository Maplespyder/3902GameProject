using MarioClone.Collision;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace MarioClone.GameObjects
{
	public class RedMushroomObject : AbstractPowerup
	{
        public RedMushroomObject(ISprite sprite, Vector2 position) : base(sprite, position, Color.Green) { }

        public override bool CollisionResponse(AbstractGameObject gameObject, Side side, GameTime gameTime)
        {
            if (gameObject is Mario && (side == Side.Top || side == Side.Bottom || side == Side.Right || side == Side.Left))
            {
                isCollided = true;
            }
            return true;

        }

        public override bool Update(GameTime gameTime, float percent)
        {
            if (isCollided)
            {
                return true;
            }
            return base.Update(gameTime, percent);


        }
    }
}
