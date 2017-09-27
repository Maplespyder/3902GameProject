using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
	public class BrickPieceObject : AbstractBlock
	{

        public BrickPieceObject(ISprite sprite, Vector2 position, int drawOrder) : base(sprite, position, drawOrder)
        {
 
        }

        public override bool Update(GameTime gameTime)
		{
            bool disposeMe = false;
			Move();

			//Nugget off screen?
			if(Position.Y > MarioCloneGame.ReturnGraphicsDevice.PreferredBackBufferHeight)
			{
                disposeMe = true;
			}

            return disposeMe;
		}

		public void ChangeVelocity(Vector2 velocity)
		{
			Velocity = velocity;
		}

		private void Move()
		{
			//Movement will also need to be tested and likely refactored late
				Position = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);
				Velocity = new Vector2(Velocity.X, Velocity.Y + .2f);
			
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (Visible)
			{
				Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
			}
		}

        public override void Bump()
        {
            // do nothing
        }
    }
}
