using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
	public class BrickPieceObject : AbstractBlock
	{
		public BrickPieceObject(ISprite sprite, Vector2 velocity, Vector2 position, int drawOrder) : base(sprite, velocity, position, drawOrder)
		{
			Sprite = sprite;
			Velocity = velocity;
			Position = position;
			Visible = true;
		}

        public override void Bounce()
        {
            //do nothing
        }

        public override void Break()
        {
            //do nothing
        }

        public override void BecomeVisible()
        {
            //do nothing
        }

        public override bool Update(GameTime gameTime)
		{
            bool disposeMe = false;
			Move();

			//Nugget off screen?
			if(Position.Y < MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight || Position.Y > MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight)
			{
                disposeMe = true;
			}

            return disposeMe;
		}

		public void ChangeVelocity(Vector2 velocity)
		{
			Velocity = velocity;
		}

		public override void Move()
		{
			//Movement will also need to be tested and likely refactored late
			Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (Visible)
			{
				Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
			}
		}
	}
}
