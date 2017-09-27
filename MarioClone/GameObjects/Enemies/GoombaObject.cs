using System;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Factories;
using MarioClone.States;

namespace MarioClone.GameObjects
{
    public class GoombaObject : IGameObject, IMoveable, ICollidable
    {

        public Vector2 Position { get; protected set; }

        public Vector2 Velocity { get; }

        public int DrawOrder { get; }

        public bool Visible { get; protected set; }

        public ISprite Sprite { get; set; }

        public EnemySpriteFactory SpriteFactory { get; protected set; }

		public Rectangle BoundingBox { get; protected set; }

		public int offSet { get; protected set; } //determines offSet to shrink bounding box by
		public int xCoordinate { get; protected set; }
		public int yCoordinate { get; protected set; }

		public GoombaObject(Vector2 velocity, Vector2 position)
        {
            SpriteFactory = MovingEnemySpriteFactory.Instance;
            Sprite = SpriteFactory.Create(EnemyType.Goomba);
            Velocity = velocity;
            Position = position;
            Visible = true;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
			}
        }

		public void UpdateBoundingBox()
		{
			xCoordinate = (int)Position.X + offSet;
			yCoordinate = (int)Position.Y + offSet;
			BoundingBox = new Rectangle(xCoordinate, yCoordinate, Sprite.SourceRectangle.Width - 2 * offSet, Sprite.SourceRectangle.Height - 2 * offSet);
		}
   
        public bool Update(GameTime gameTime)
        {
			UpdateBoundingBox();
			return false;
        }
    }
}
