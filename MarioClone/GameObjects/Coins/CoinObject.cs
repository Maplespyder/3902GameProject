using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.GameObjects
{
    class CoinObject : IGameObject, IMoveable
    {
        public ISprite Sprite { get; set; }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }
		public Rectangle BoundingBox { get; protected set; }

		private int offSet = 0;

		public CoinObject(ISprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Velocity = new Vector2(0, 0);
            Position = position;
            Visible = true;
            DrawOrder = 1;
			UpdateBoundingBox();
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
			BoundingBox = new Rectangle((int)Position.X - offSet, (int)Position.Y - offSet, Sprite.SourceRectangle.Width + (2 * offSet),
				Sprite.SourceRectangle.Height + (2 * offSet));
		}

		public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
