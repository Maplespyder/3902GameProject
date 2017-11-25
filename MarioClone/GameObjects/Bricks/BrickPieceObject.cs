using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
	public class BrickPieceObject : AbstractBlock
	{

        public BrickPieceObject(ISprite sprite, Vector2 position) : base(sprite, position) { }

        public override bool Update(GameTime gameTime, float percent)
		{
			Move(percent);

			//Nugget off screen?
			if(Position.Y >= MarioCloneGame.Player1Camera.Limits.Value.Bottom)
			{
                return true;
			}

            return false;
		}

		public void ChangeVelocity(Vector2 velocity)
		{
			Velocity = velocity;
		}

		private void Move(float percent)
		{
			//Movement will also need to be tested and likely refactored late
			Position = new Vector2(Position.X + Velocity.X * percent, Position.Y + Velocity.Y * percent);
			Velocity = new Vector2(Velocity.X, Velocity.Y + .2f * percent);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, Position, DrawOrder, gameTime, Orientation);
        }
    }
}
