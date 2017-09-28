using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioClone.Collision;

namespace MarioClone.GameObjects
{
    class CoinObject : IGameObject, IMoveable
    {
        public ISprite Sprite { get; set; }

        public Vector2 Position { get; protected set; }

        public int DrawOrder { get; protected set; }

        public bool Visible { get; protected set; }

        public Vector2 Velocity { get; protected set; }
		public HitBox BoundingBox { get; protected set; }

		public CoinObject(ISprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Velocity = new Vector2(0, 0);
            Position = position;
            Visible = true;
            DrawOrder = 1;
			BoundingBox = new HitBox(0, 0);
			BoundingBox.UpdateHitBox(Position, Sprite);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                Sprite.Draw(spriteBatch, Position, this.DrawOrder, gameTime, Facing.Left);
            }
        }

		public bool Update(GameTime gameTime)
        {
            return false;
        }
    }
}
